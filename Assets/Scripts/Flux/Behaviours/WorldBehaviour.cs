using System;
using UnityEngine;
using UniRx;
using Flux;

public class WorldBehaviour : ObservableMonoBehaviour {

  public override void Awake() {
    UpdateAsObservable()
      .Where(_ => Input.GetMouseButtonDown((int)MouseButton.Left))
      .Select(_ => Camera.main.ScreenPointToRay(Input.mousePosition))
      .SelectWhere(ray => {
        RaycastHit hit;
        return Tuple.Create(collider.Raycast(ray, out hit, 100.0f), hit);
      })
      .Subscribe(hit => {
        ComponentActionCreator.UpdateTarget(hit.point);
      });
  }
}
