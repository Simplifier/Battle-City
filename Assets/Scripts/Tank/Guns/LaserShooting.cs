using UnityEngine;

public class LaserShooting : TimedGun {
	public int damage = 20;
	public float range = 100f;
	public LayerMask obstacleMask;
	public AudioSource shootingAudio;
	public AudioClip fireClip;

	protected RaycastHit shootHit;
	private Ray _shootRay;
	private LineRenderer _gunLine;

	void Awake() {
		_gunLine = GetComponent<LineRenderer>();
	}

	protected virtual void Update() {
		Loop();
	}

	public override void DisableEffects() {
		_gunLine.enabled = false;
	}

	protected override void Shoot() {
		timer = 0;

		shootingAudio.clip = fireClip;
		shootingAudio.Play();

		_gunLine.enabled = true;
		_gunLine.SetPosition(0, transform.position);

		_shootRay.origin = transform.position;
		_shootRay.direction = transform.forward;

		if (Physics.Raycast(_shootRay, out shootHit, range, obstacleMask)) {
			HandleHit();
		} else {
			_gunLine.SetPosition(1, _shootRay.origin + _shootRay.direction * range);
		}
	}

	protected virtual void HandleHit() {
		EnemyLifecycle enemyLifecycle = shootHit.collider.GetComponent<EnemyLifecycle>();
		if (enemyLifecycle != null) {
			enemyLifecycle.TakeDamage(damage);
		}

		_gunLine.SetPosition(1, shootHit.point);
	}
}