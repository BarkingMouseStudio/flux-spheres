using UnityEngine;
using System;
using System.Collections;
using Flux;

public static class ComponentActionCreator {

  public static void GetTargets() {
    AppDispatcher.Instance.HandleComponentAction(new FluxAction("GET_TARGETS"));
  }

  public static void SetFire(int id) {
    AppDispatcher.Instance.HandleComponentAction(new FluxAction("SET_FIRE", id));
  }
}
