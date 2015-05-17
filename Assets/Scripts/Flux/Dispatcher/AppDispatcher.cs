using UnityEngine;
using System;
using System.Collections;
using Flux;

public class AppDispatcher : Dispatcher<AppDispatcher> {

  public override void Dispatch(Payload payload) {
    Debug.Log(payload);
    base.Dispatch(payload);
  }

  public void HandleComponentAction(FluxAction action) {
    Dispatch(new Payload("COMPONENT_ACTION", action));
  }
}
