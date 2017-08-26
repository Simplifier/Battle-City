using UnityEngine;

public class TankMovement : MonoBehaviour {
	public AudioSource movementAudio;
	public AudioClip engineDriving;
	public AudioClip engineIdling;
	public float pitchRange = 0.2f;
	public float speed = 12f;
	public float turnSpeed = 180f;
	public Transform weapon;

	private float _movementInputValue;
	private float _originalPitch;
	private Rigidbody _rigidbody;
	private float _turnInputValue;

	private void Awake() {
		_rigidbody = GetComponent<Rigidbody>();
	}

	private void OnEnable() {
		_rigidbody.isKinematic = false;
		_movementInputValue = 0;
		_turnInputValue = 0;
	}

	private void OnDisable() {
		_rigidbody.isKinematic = true;
	}

	private void Start() {
		_originalPitch = movementAudio.pitch;
	}

	private void Update() {
		_movementInputValue = Input.GetAxis("Move");
		_turnInputValue = Input.GetAxis("Turn");

		EngineAudio();
	}

	private void EngineAudio() {
		var isIdling = Mathf.Abs(_movementInputValue) < .1f && Mathf.Abs(_turnInputValue) < .1f;
		if (isIdling) {
			if (movementAudio.clip == engineDriving) {
				PlayMovementAudio(engineIdling);
			}
		}
		else {
			if (movementAudio.clip == engineIdling) {
				PlayMovementAudio(engineDriving);
			}
		}
	}

	private void PlayMovementAudio(AudioClip audio) {
		movementAudio.clip = audio;
		movementAudio.pitch = Random.Range(_originalPitch - pitchRange, _originalPitch + pitchRange);
		movementAudio.Play();
	}

	private void FixedUpdate() {
		Move();
		Turn();
	}

	private void Move() {
		Vector3 movement = transform.forward * _movementInputValue * speed * Time.deltaTime;
		_rigidbody.MovePosition(_rigidbody.position + movement);
	}

	private void Turn() {
		float turnAngle = _turnInputValue * turnSpeed * Time.deltaTime;
		Quaternion turn = Quaternion.Euler(0f, turnAngle, 0f);
		_rigidbody.MoveRotation(_rigidbody.rotation * turn);
	}

	private void TurnWeapon() {
		// ray from the mouse to the camera
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit floorHit;
		if (Physics.Raycast(camRay, out floorHit)) {
			Vector3 direction = floorHit.point - transform.position;
			direction.y = 0;

			weapon.rotation = Quaternion.LookRotation(direction);
		}
	}
}