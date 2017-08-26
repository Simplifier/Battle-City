using UnityEngine;

public abstract class TimedGun : MonoBehaviour {
	public float timeBetweenShots = 0.15f;

	protected float timer;
	private float _effectsDisplayTime = 0.2f;

	protected void Loop() {
		timer += Time.deltaTime;

		if (Input.GetButton("Fire") && timer >= timeBetweenShots) {
			Shoot();
		}

		if (timer >= timeBetweenShots * _effectsDisplayTime) {
			DisableEffects();
		}
	}

	public virtual void DisableEffects() {
	}

	protected abstract void Shoot();
}