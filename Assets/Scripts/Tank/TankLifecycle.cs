using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TankLifecycle : MonoBehaviour {
	[HideInInspector]
	public int health;
	public int startingHealth = 100;
	public AudioClip deathClip;
	public Image healthIcon;
	public float flashSpeed = 5f;
	public Color flashColour = new Color(0, 0, 0, 1);
	public Slider healthSlider;


	private Color _startingHealthIconColour;
	private AudioSource _playerAudio;
	private TankMovement _movement;
	private TimedGun _gun;
	private bool _isDead;
	private bool _isDamaged;


	void Awake() {
		_playerAudio = GetComponent<AudioSource>();
		_movement = GetComponent<TankMovement>();
		_gun = GetComponentInChildren<TimedGun>();

		health = startingHealth;
		_startingHealthIconColour = healthIcon.color;
	}


	void Update() {
		if (_isDamaged) {
			healthIcon.color = flashColour;
		} else {
			healthIcon.color = Color.Lerp(healthIcon.color, _startingHealthIconColour, flashSpeed * Time.deltaTime);
		}

		_isDamaged = false;
	}


	public void TakeDamage(int amount) {
		_isDamaged = true;

		_playerAudio.Play();

		health -= amount;
		healthSlider.value = health;
		if (health <= 0 && !_isDead) {
			Die();
		}
	}


	void Die() {
		_isDead = true;

		_gun.DisableEffects();

		_playerAudio.clip = deathClip;
		_playerAudio.Play();

		_movement.enabled = false;
		_gun.enabled = false;
	}
}