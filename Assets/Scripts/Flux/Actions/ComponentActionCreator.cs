using UnityEngine;
using System;
using System.Collections;
using Flux;

public static class ComponentActionCreator {

  public static void UpdateTarget(Vector3 target) {
    AppDispatcher.Instance.HandleComponentAction(new FluxAction("UPDATE_TARGET", target));
  }
}
