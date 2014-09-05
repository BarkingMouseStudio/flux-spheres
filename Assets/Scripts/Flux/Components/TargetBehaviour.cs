using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Flux;

public class TargetBehaviour : FluxBehaviour {

  public bool isOnFire = false;

  public Material baseMaterial;
  public Material fireMaterial;

  void OnEnable() {
    TargetStore.Instance.Changed += OnChanged;
  }

  void OnDisable() {
    TargetStore.Instance.Changed -= OnChanged;
  }

  void OnChanged() {
    isOnFire = TargetStore.Instance.IsOnFire(GetInstanceID());

    if (isOnFire) {
      GetComponent<Renderer>().material = fireMaterial;
    } else {
      GetComponent<Renderer>().material = baseMaterial;
    }
  }
}
