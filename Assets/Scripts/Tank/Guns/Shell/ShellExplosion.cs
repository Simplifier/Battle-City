using System.Collections;
using UnityEngine;

public class ShellExplosion : MonoBehaviour {
	public LayerMask obstacleMask;
	public ParticleSystem explosionParticles;
	public AudioSource explosionAudio;
	public float maxDamage = 100f;
	public float explosionForce = 1000f;
	public float explosionRadius = 5f;

	private Renderer _renderer;
	private Rigidbody _rb;

	void Start() {
		_renderer = GetComponent<Renderer>();
		_rb = GetComponent<Rigidbody>();
	}

	private void OnTriggerEnter(Collider other) {
		var colliders = Physics.OverlapSphere(transform.position, explosionRadius, obstacleMask);

		foreach (Collider c in colliders) {
			Rigidbody targetRigidbody = c.GetComponent<Rigidbody>();
			if (!targetRigidbody) {
				continue;
			}

			targetRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);

			EnemyLifecycle enemy = targetRigidbody.GetComponent<EnemyLifecycle>();
			if (!enemy) {
				continue;
			}

			float damage = CalculateDamage(targetRigidbody.transform.position);
			enemy.TakeDamage(damage);
		}

		explosionParticles.transform.parent = null;
		explosionParticles.Play();
		explosionAudio.Play();

		_renderer.enabled = false;
		_rb.detectCollisions = false;
		_rb.velocity = Vector3.zero;
		StartCoroutine(ResetParticles(explosionParticles.main.duration));
	}

	private IEnumerator ResetParticles(float delay) {
		yield return new WaitForSeconds(delay);

		explosionParticles.transform.parent = transform;
		explosionParticles.transform.position = transform.position;
		_renderer.enabled = true;
		_rb.detectCollisions = true;

		Pool.Put(gameObject.name, gameObject);
	}


	private float CalculateDamage(Vector3 targetPosition) {
		Vector3 explosionToTarget = targetPosition - transform.position;
		float distance = explosionToTarget.magnitude;
		float relativeDistance = (explosionRadius - distance) / explosionRadius;

		float damage = relativeDistance * maxDamage;
		return Mathf.Max(0f, damage);
	}
}