using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyLifecycle : MonoBehaviour {
	//[HideInInspector]
	public float health;
	public int startingHealth = 100;
	public Slider healthSlider;
	[Tooltip("the enemy sinks through the ground when dead")]
	public float sinkSpeed = 2.5f;              
	public float sinkDelay = 1;
	public int scoreValue = 10;
	[Tooltip("protection reduces taken damage by specified part")]
	public float protection = .5f;
	public Slider protectionSlider;

	public static int enemyCount;


	private NavMeshAgent _nav;
	private Animator _anim;
	private CapsuleCollider _capsuleCollider;
	private bool _isDead;
	private bool _isSinking;

	void Awake() {
		_anim = GetComponent<Animator>();
		_capsuleCollider = GetComponent<CapsuleCollider>();
		_nav = GetComponent<NavMeshAgent>();

		protectionSlider.value = protection;
	}

	void OnEnable() {
		_isDead = false;
		_isSinking = false;
		_nav.enabled = true;
		health = startingHealth;
		healthSlider.value = health;
		enemyCount++;
	}

	void Update() {
		if (_isSinking) {
			transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
		}
	}


	public void TakeDamage(float amount) {
		if (_isDead) {
			return;
		}

		health -= amount * (1 - protection);
		healthSlider.value = health;

		if (health <= 0) {
			Die();
		}
	}


	private void Die() {
		_isDead = true;
		enemyCount--;

		// turn off collisions
		_capsuleCollider.isTrigger = true;

		_anim.SetTrigger("Dead");

		StartCoroutine(StartSinking());
	}


	private IEnumerator StartSinking() {
		yield return new WaitForSeconds(sinkDelay);

		GetComponent<NavMeshAgent>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = true;

		_isSinking = true;

		ScoreCounting.AccountScore(scoreValue);

		StartCoroutine(Remove(gameObject, 2));
	}

	private IEnumerator Remove(GameObject enemy, int delay) {
		yield return new WaitForSeconds(delay);

		Pool.Put(enemy.name, enemy);
	}
}