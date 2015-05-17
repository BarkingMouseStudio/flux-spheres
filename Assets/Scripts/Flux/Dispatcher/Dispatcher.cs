using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Flux {

  public abstract class Dispatcher<T> where T : Dispatcher<T>, new() {

    private static T instance;

    public static T Instance {
      get {
        if (instance == null) {
          instance = new T();
        }
        return instance;
      }
    }

    private int lastId;
    private IDictionary<int, Action<Payload>> callbacks;
    private IDictionary<int, bool> isPending;
    private IDictionary<int, bool> isHandled;
    private bool isDispatching;
    private Payload pendingPayload;

    public Dispatcher() {
      this.lastId = 1;
      this.callbacks = new Dictionary<int, Action<Payload>>();
      this.isPending = new Dictionary<int, bool>();
      this.isHandled = new Dictionary<int, bool>();
      this.isDispatching = false;
    }

    private static void Invariant(bool condition, string message) {
      if (!condition) {
        throw new Exception("Invariant Violation: " + message);
      }
    }

    public int Register(Action<Payload> callback) {
      var id = this.lastId;
      this.lastId++;
      this.callbacks[id] = callback;
      return id;
    }

    public void Unregister(int id) {
      Invariant(this.callbacks.ContainsKey(id),
        String.Format("Dispatcher#unregister(...): `{0}` does not map to a registered callback.", id));
      this.callbacks.Remove(id);
    }

    public void WaitFor(int[] ids) {
      Invariant(this.isDispatching,
        "Dispatcher#waitFor(...): Must be invoked while dispatching.");

      foreach (int id in ids) {
        if (this.isPending[id]) {
          Invariant(this.isHandled[id],
            String.Format("Dispatcher#waitFor(...): Circular dependency detected while waiting for `{0}`.", id));
          continue;
        }

        this.Invoke(id);
      }
    }

    public virtual void Dispatch(Payload payload) {
      Invariant(!this.isDispatching,
        "Dispatcher#dispatch(...): Cannot dispatch in the middle of a dispatch.");

      this.StartDispatching(payload);

      try {
        foreach (int id in this.callbacks.Keys) {
          if (this.isPending[id]) {
            continue;
          }

          this.Invoke(id);
        }
      } finally {
        this.StopDispatching();
      }
    }

    public bool IsDispatching() {
      return this.isDispatching;
    }

    private void Invoke(int id) {
      this.isPending[id] = true;
      this.callbacks[id](this.pendingPayload);
      this.isHandled[id] = false;
    }

    private void StartDispatching(Payload payload) {
      foreach (int id in this.callbacks.Keys) {
        this.isPending[id] = false;
        this.isHandled[id] = false;
      }

      this.pendingPayload = payload;
      this.isDispatching = true;
    }

    private void StopDispatching() {
      this.isDispatching = false;
    }
  }
}
