using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public GameObject explosionPrefab;   //爆発エフェクトのPrefab

	void Update () {
		transform.Translate (0, 0.2f, 0);

		if (transform.position.y > 5) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
	    // 爆発エフェクトを生成する	
		Instantiate (explosionPrefab, transform.position, Quaternion.identity);
		Destroy (coll.gameObject);
		Destroy (gameObject);
	}
}
