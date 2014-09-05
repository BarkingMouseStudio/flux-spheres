using UnityEngine;
using System.Collections;

namespace Flux {

  public abstract class Store<T> where T : Store<T>, new() {

    private static T instance;

    public static T Instance {
      get {
        if (instance == null) {
          instance = new T();
        }
        return instance;
      }
    }

    private static int dispatchId = -1;

    public static int DispatchId {
      get {
        return dispatchId;
      }
      protected set {
        dispatchId = value;
      }
    }

    public delegate void ChangedHandler();
    public event ChangedHandler Changed;

    protected void Change() {
      if (Changed != null) {
        Changed();
      }
    }
  }
}
