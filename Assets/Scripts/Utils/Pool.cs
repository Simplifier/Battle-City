using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour {
	public static Dictionary<string, Stack<GameObject>> pool = new Dictionary<string, Stack<GameObject>>();

	void OnDestroy() {
		pool.Clear();
	}

	public static void Put(string key, GameObject obj) {
		if (!pool.ContainsKey(key)) {
			pool[key] = new Stack<GameObject>();
		}
		obj.SetActive(false);
		pool[key].Push(obj);
	}

	public static GameObject Get(GameObject prefab, Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion)) {
		string key = prefab.name;
		GameObject obj;
		if (!pool.ContainsKey(key)) {
			obj = Object.Instantiate(prefab, position, rotation);
			obj.name = prefab.name;
			return obj;
		}

		if (pool[key].Count == 0) {
			obj = Object.Instantiate(prefab, position, rotation);
			obj.name = prefab.name;
		}
		else {
			obj = pool[key].Pop();
		}

		obj.transform.position = position;
		obj.transform.rotation = rotation;
		obj.SetActive(true);

		return obj;
	}
}