using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FreezingLaserShooting : LaserShooting {
	public float freezeTime = 3;

	private readonly Dictionary<FreezeData, float> _freezeTimers = new Dictionary<FreezeData, float>();
	private static readonly Color _freezeColor = new Color(0, .5f, .9f);

	protected override void Update() {
		base.Update();
		UnfreezeByTime(_freezeTimers.Keys.ToList());
	}

	private void UnfreezeByTime(List<FreezeData> enemies) {
		foreach (FreezeData enemy in enemies) {
			float time = _freezeTimers[enemy] + Time.deltaTime;
			_freezeTimers[enemy] = time;

			if (time >= freezeTime || enemy.lifecycle.health <= 0) {
				enemy.nav.speed = enemy.originalSpeed;
				_freezeTimers.Remove(enemy);

				enemy.mat.SetColor("_EmissionColor", Color.black);
			}
		}
	}

	protected override void HandleHit() {
		base.HandleHit();

		Freeze(shootHit.collider.GetComponent<FreezeData>());
	}

	private void Freeze(FreezeData enemy) {
		if (enemy) {
			_freezeTimers[enemy] = 0;
			enemy.nav.speed = enemy.originalSpeed / 2;

			enemy.mat.SetColor("_EmissionColor", _freezeColor);
		}
	}
}