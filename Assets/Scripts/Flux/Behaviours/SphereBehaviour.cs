using UnityEngine;
using UniRx;
using Flux;

[RequireComponent(typeof(NavMeshAgent))]
public class SphereBehaviour : MonoBehaviour {

  NavMeshAgent agent;

  void Awake() {
    agent = GetComponent<NavMeshAgent>();
    WorldStore.Instance.Changed += OnChange;
  }

  void OnChange() {
    agent.SetDestination(WorldStore.Instance.Target);
  }
}
