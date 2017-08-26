using UnityEngine;
using UnityEngine.AI;

public class FreezeData : MonoBehaviour {
	[HideInInspector]
	public float originalSpeed;
	[HideInInspector]
	public NavMeshAgent nav;
	[HideInInspector]
	public EnemyLifecycle lifecycle;
	[HideInInspector]
	public Material mat;

	private void Awake() {
		lifecycle = GetComponent<EnemyLifecycle>();
		nav = GetComponent<NavMeshAgent>();
		originalSpeed = nav.speed;

		mat = GetMaterial();
		mat.EnableKeyword("_EMISSION");
	}

	private Material GetMaterial() {
		foreach (Transform child in transform) {
			return child.GetComponent<Renderer>().material;
		}
		return null;
	}
}
