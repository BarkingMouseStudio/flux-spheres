using UnityEngine;
using System.Collections;
using Flux;

[RequireComponent(typeof(NavMeshAgent))]
public class MinionBehaviour : FluxBehaviour {

  TargetBehaviour[] targets;

  TargetBehaviour currentTarget;
  NavMeshAgent agent;

  void Awake() {
    agent = GetComponent<NavMeshAgent>();
    ComponentActionCreator.GetTargets();
  }

  void OnEnable() {
    TargetStore.Instance.Changed += OnChanged;
  }

  void OnDisable() {
    TargetStore.Instance.Changed -= OnChanged;
  }

  void OnChanged() {
    targets = Helpers.Shuffle(TargetStore.Instance.GetAll());

    if (currentTarget == null || currentTarget.isOnFire) {
      PickTarget();
    }
  }

  void PickTarget() {
    if (targets.Length == 0) {
      return;
    }

    for (int i = 0; i < targets.Length; i++) {
      currentTarget = targets[i];

      if (currentTarget.isOnFire) {
        currentTarget = null;
        continue;
      } else {
        break;
      }
    }

    if (currentTarget != null) {
      agent.SetDestination(currentTarget.transform.position);
    } else {
      agent.Stop();
    }
  }

	void Update() {
    if (currentTarget) {
      if (currentTarget.isOnFire) {
        // Pick a new target
        PickTarget();
      } else {
        float dist = Vector3.Distance(transform.position, currentTarget.transform.position);
        if (dist < 1.5f) {
          // Set nearby currentTarget on fire
          ComponentActionCreator.SetFire(currentTarget.GetInstanceID());
        }
      }
    }
	}
}
