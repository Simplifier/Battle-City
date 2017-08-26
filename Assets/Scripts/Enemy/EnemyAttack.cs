using UnityEngine;


public class EnemyAttack : MonoBehaviour {
	public float timeBetweenHits = 0.5f;
	public int attackDamage = 10;

	private Animator _anim;
	private GameObject _player;
	private TankLifecycle _playerLifecycle;
	private EnemyLifecycle _enemyLifecycle;
	private bool _playerInRange;
	private float _timer; // provides delay between attacks


	void Awake() {
		_player = GameObject.FindGameObjectWithTag("Player");
		_playerLifecycle = _player.GetComponent<TankLifecycle>();
		_enemyLifecycle = GetComponent<EnemyLifecycle>();
		_anim = GetComponent<Animator>();
	}


	void OnTriggerEnter(Collider other) {
		if (other.gameObject == _player) {
			_playerInRange = true;
			_anim.SetBool("Attack", true);
		}
	}


	void OnTriggerExit(Collider other) {
		if (other.gameObject == _player) {
			_playerInRange = false;
			_anim.SetBool("Attack", false);
		}
	}


	void Update() {
		_timer += Time.deltaTime;

		if (_timer >= timeBetweenHits && _playerInRange && _enemyLifecycle.health > 0) {
			Attack();
		}

		if (_playerLifecycle.health <= 0) {
			_anim.SetTrigger("PlayerDead");
		}
	}


	private void Attack() {
		_timer = 0;

		if (_playerLifecycle.health > 0) {
			_playerLifecycle.TakeDamage(attackDamage);
		}
	}
}