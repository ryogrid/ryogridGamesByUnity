using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
	void Update () {
		transform.Translate (0, 0.2f, 0);

		if (transform.position.y > 5) {
			Destroy (gameObject);
		}
	}
}