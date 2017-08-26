using UnityEngine;

public class IndicatorAligning : MonoBehaviour {
	private static readonly Quaternion _rot = Quaternion.Euler(48, 0, 0);
	void Update() {
		transform.rotation = _rot;
	}
}