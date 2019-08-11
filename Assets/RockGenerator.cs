using UnityEngine;
using System.Collections;

public class RockGenerator : MonoBehaviour {

	public GameObject rockPrefab;

	void Start () {
		InvokeRepeating ("GenRock", 1, 1);
	}
	
	void GenRock () {
		Instantiate (rockPrefab, new Vector3 (-2.5f + 5 * Random.value, 6, 0), Quaternion.identity);
	}
}
