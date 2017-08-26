using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
	public float restartDelay = 5f;

	private TankLifecycle _playerLifecycle;
	private Animator anim;
	private float restartTimer;


	private void Awake() {
		_playerLifecycle = GameObject.FindGameObjectWithTag("Player").GetComponent<TankLifecycle>();
		anim = GetComponent<Animator>();
	}


	void Update() {
		if (_playerLifecycle.health <= 0) {
			EnemyLifecycle.enemyCount = 0;
			anim.SetTrigger("GameOver");

			restartTimer += Time.deltaTime;

			if (restartTimer >= restartDelay) {
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
		}
	}
}