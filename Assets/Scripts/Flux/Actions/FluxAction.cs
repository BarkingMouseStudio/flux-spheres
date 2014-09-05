using UnityEngine;
using System.Collections;

namespace Flux {

  public struct FluxAction {
    public string actionType;
    public object data;

    public FluxAction(string actionType, object data = null) {
      this.actionType = actionType;
      this.data = data;
    }

    public override string ToString() {
      return this.actionType;
    }
  }
}
