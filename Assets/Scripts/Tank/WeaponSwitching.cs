using UnityEngine;

public class WeaponSwitching : MonoBehaviour {
	public int selectedWeaponIndex;

	[HideInInspector]
	public TimedGun selectedWeapon;

	private int _prevSelectedWeapon = -1;
	private bool _switchPressed;

	private void Start() {
		SelectWeapon();
	}

	private void Update() {
		float switchValue = Input.GetAxis("Switch Weapon");
		if (Mathf.Abs(switchValue) > .1 && !_switchPressed) {
			if (switchValue > 0) { // next weapon
				if (selectedWeaponIndex >= transform.childCount - 1) {
					selectedWeaponIndex = 0;
				}
				else {
					selectedWeaponIndex++;
				}
			}
			else { // previous weapon
				if (selectedWeaponIndex <= 0) {
					selectedWeaponIndex = transform.childCount - 1;
				}
				else {
					selectedWeaponIndex--;
				}
			}

			_switchPressed = true;
		}
		if (Mathf.Abs(switchValue) < .1) {
			_switchPressed = false;
		}


		for (int i = 1; i < 10; i++) {
			if (transform.childCount < i) {
				break;
			}
			if (Input.GetKeyDown(i.ToString())) {
				selectedWeaponIndex = i - 1;
				break;
			}
		}

		SelectWeapon();
	}

	private void SelectWeapon() {
		if (_prevSelectedWeapon == selectedWeaponIndex) {
			return;
		}

		int i = 0;
		foreach (Transform weapon in transform) {
			if (i == selectedWeaponIndex) {
				weapon.gameObject.SetActive(true);
				selectedWeapon = weapon.GetComponent<TimedGun>();
			}
			else {
				weapon.gameObject.SetActive(false);
			}

			i++;
		}
	}
}