using UnityEngine;
using System;
using System.Collections;

namespace Flux {

  public struct Payload {
    public string source;
    public FluxAction action;

    public Payload(string source, FluxAction action) {
      this.source = source;
      this.action = action;
    }

    public override string ToString() {
      return String.Format("{0}: {1}", this.source, this.action);
    }
  }
}
