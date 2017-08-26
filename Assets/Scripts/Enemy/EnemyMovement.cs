using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {
	private NavMeshAgent _nav;
	private EnemyLifecycle _lifecycle;
	private Transform _player;
	private TankLifecycle _playerLifecycle;

	private void Awake() {
		_player = GameObject.FindGameObjectWithTag("Player").transform;
		_playerLifecycle = _player.GetComponent<TankLifecycle>();
		_lifecycle = GetComponent<EnemyLifecycle>();
		_nav = GetComponent<NavMeshAgent>();
	}

	private void Update() {
		if (_lifecycle.health > 0 && _playerLifecycle.health > 0) {
			_nav.SetDestination(_player.position);
		} else {
			_nav.enabled = false;
		}
	}
}