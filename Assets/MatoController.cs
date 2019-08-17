using System.Collections;
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
		//Destroy (coll.gameObject);
        GameObject MessageText = GameObject.Find("MessageText");
        if(gameObject.transform.position.x < 0f)
        {
            MessageText.GetComponent<Text>().text = "2Pの勝利!!!";
        }
        else
        {
            MessageText.GetComponent<Text>().text = "1Pの勝利!!!";
        }

		Destroy (gameObject);
        //Instantiate(MatoPrefab, new Vector3(Random.Range(-7f, 7f), Random.Range(-4f, 4f), 0), Quaternion.identity);
        //Instantiate(gameObject, new Vector3(Random.Range(-7f, 7f), Random.Range(-4f, 4f), 0), Quaternion.identity);
/*
        GameObject MessageText = GameObject.Find("MessageText");
        MessageText.GetComponent<Text>().text = "君、意外とやるじゃん";
*/
    }
}
