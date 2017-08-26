using UnityEngine;

public class BorderRestriction : MonoBehaviour {
	private Transform _player;
	private Rigidbody _body;
	private Vector3 _prevPos;

	private void Awake() {
		_player = GameObject.FindGameObjectWithTag("Player").transform;
		_body = _player.GetComponent<Rigidbody>();
	}

	void OnTriggerEnter(Collider other) {
		if (other.transform == _player) {
			_body.MovePosition(_body.position - Vector3.Normalize(_body.position - _prevPos) * 10);
		}
		_prevPos = _body.position;
	}
}