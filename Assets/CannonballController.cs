using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<Rigidbody>().AddForce(bulletInstance.transform.forward * bulletPower);
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500, 700));
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -8) {
            Destroy(gameObject);
        }
    }
}
