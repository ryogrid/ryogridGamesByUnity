﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatoController : MonoBehaviour
{

    //public GameObject MatoPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

/*
	void OnTriggerEnter2D(Collider2D coll) {
		//Destroy (coll.gameObject);
		Destroy (gameObject);
        //Instantiate(MatoPrefab, new Vector3(Random.Range(-7f, 7f), Random.Range(-4f, 4f), 0), Quaternion.identity);
        //Instantiate(gameObject, new Vector3(Random.Range(-7f, 7f), Random.Range(-4f, 4f), 0), Quaternion.identity);

        GameObject MessageText = GameObject.Find("MessageText");
        MessageText.GetComponent<Text>().text = "君、意外とやるじゃん";
    }
*/

    void OnCollisionEnter2D(Collision2D other)
    {
        GameObject ShotParamText1P = GameObject.Find("ShotParamText1P");
        bool isTurn1P = true;
        if(ShotParamText1P.GetComponent<Text>().text.IndexOf("@") != -1)
        {
            isTurn1P = false;
        }

        GameObject MessageText = GameObject.Find("MessageText");
        if (WorldController.is1PlayerMode)
        {
            MessageText.GetComponent<Text>().text = "お見事!";
            Destroy(gameObject);
        }
        else
        {
            if (gameObject.transform.position.x < 0f && !isTurn1P)
            {
                MessageText.GetComponent<Text>().text = "2Pの勝利!!!";
                Destroy(gameObject);
            }
            else if(gameObject.transform.position.x > 0f && isTurn1P)
            {
                MessageText.GetComponent<Text>().text = "1Pの勝利!!!";
                Destroy(gameObject);
            }
        }


        //Instantiate(MatoPrefab, new Vector3(Random.Range(-7f, 7f), Random.Range(-4f, 4f), 0), Quaternion.identity);
        //Instantiate(gameObject, new Vector3(Random.Range(-7f, 7f), Random.Range(-4f, 4f), 0), Quaternion.identity);
/*
        GameObject MessageText = GameObject.Find("MessageText");
        MessageText.GetComponent<Text>().text = "君、意外とやるじゃん";
*/
    }
}
