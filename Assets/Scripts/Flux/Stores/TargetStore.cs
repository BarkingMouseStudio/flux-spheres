using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Flux;

public class TargetStore : Store<TargetStore> {

  private IDictionary<int, TargetBehaviour> targets = new Dictionary<int, TargetBehaviour>();
  private IDictionary<int, bool> targetsOnFire = new Dictionary<int, bool>();

  public TargetBehaviour[] GetAll() {
    ICollection<TargetBehaviour> vals = targets.Values;
    TargetBehaviour[] arr = new TargetBehaviour[vals.Count];
    vals.CopyTo(arr, 0);
    return arr;
  }

  public bool IsOnFire(int id) {
    bool isOnFire = false;
    targetsOnFire.TryGetValue(id, out isOnFire);
    return isOnFire;
  }

  protected void dispatchReceive(Payload payload) {
    var action = payload.action;

    switch (action.actionType) {
      case "GET_TARGETS":
        var _targets = (TargetBehaviour[])Object.FindObjectsOfType(typeof(TargetBehaviour));
        foreach (TargetBehaviour target in _targets) {
          targets[target.GetInstanceID()] = target;
        }
        break;
      case "SET_FIRE":
        targetsOnFire[(int)action.data] = true;
        break;
      default:
        return;
    }

    Change();
  }

  public TargetStore() {
    TargetStore.DispatchId = AppDispatcher.Instance.Register(dispatchReceive);
  }
}
