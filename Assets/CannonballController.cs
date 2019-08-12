using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500, 700));
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -8 || transform.position.y < -5.5) {
            Destroy(gameObject);
        }
    }
}
