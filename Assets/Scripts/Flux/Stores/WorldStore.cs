using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Flux;

public class WorldStore : Store<WorldStore> {

  private Vector3 target;

  public WorldStore() {
    WorldStore.DispatchId = AppDispatcher.Instance.Register(dispatchReceive);
  }

  public Vector3 Target {
    get {
      return target;
    }
  }

  void dispatchReceive(Payload payload) {
    var action = payload.action;

    switch (action.actionType) {
      case "UPDATE_TARGET":
        target = (Vector3)action.data;
        break;
      default:
        return;
    }

    Change();
  }
}
