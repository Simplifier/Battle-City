using UnityEngine;
using UnityEngine.UI;

public class ScoreCounting : MonoBehaviour {
	[SerializeField]
	private static int _score;
	private Text _tf;

	void Awake() {
		_tf = GetComponent<Text>();
		_score = 0;
	}

	void Update() {
		_tf.text = _score.ToString();
	}

	public static void AccountScore(int scoreValue) {
		_score += scoreValue;
	}
}