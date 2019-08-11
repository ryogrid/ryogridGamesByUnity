using UnityEngine;
using System.Collections;

public class RocketController : MonoBehaviour {

	public GameObject bulletPrefab;

	void Update () {
		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.Translate (-0.1f, 0, 0);
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Translate ( 0.1f, 0, 0);
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			Instantiate (bulletPrefab, transform.position, Quaternion.identity);
		}
	}
}