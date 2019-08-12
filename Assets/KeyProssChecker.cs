using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyProssChecker : MonoBehaviour
{

    public GameObject CannonballPrefab;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("checkKeyPress", 0.01f, 0.01f);
    }

    
    void checkKeyPress()
    {
 		if (Input.GetKeyDown (KeyCode.Space)) {
			Instantiate (CannonballPrefab, new Vector3 (8.5f, -4.5f, 0), Quaternion.identity);
		}       
    }
}
