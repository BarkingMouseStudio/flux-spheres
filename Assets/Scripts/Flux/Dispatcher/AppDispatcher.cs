using UnityEngine;
using System;
using System.Collections;
using Flux;

public class AppDispatcher : Dispatcher<AppDispatcher> {

  public void HandleComponentAction(FluxAction action) {
    Dispatch(new Payload("COMPONENT_ACTION", action));
  }
}
