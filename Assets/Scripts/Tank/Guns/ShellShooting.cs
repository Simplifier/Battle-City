using System.Collections;
using UnityEngine;

public class ShellShooting : TimedGun {
	public GameObject shellPrefab;
	public AudioSource shootingAudio;
	public AudioClip fireClip;
	public float launchForce = 50f;

	private void Update() {
		Loop();
	}

	protected override void Shoot() {
		timer = 0;

		GameObject shell = Pool.Get(shellPrefab, transform.position, transform.rotation);
		if (!shell) {
			return;
		}

		shell.GetComponent<Rigidbody>().velocity = launchForce * transform.forward;
		shootingAudio.clip = fireClip;
		shootingAudio.Play();
	}
}