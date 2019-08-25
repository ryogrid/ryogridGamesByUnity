using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 lastVelocity;
    private Vector3 lastPosition;
    private int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
         rb = this.GetComponent<Rigidbody>();       
    }

    //一定秒数ごとに呼び出される. デフォルトは20ms間隔らしい
    void FixedUpdate()
    {
        lastVelocity = rb.velocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < -3f)
        {
            Destroy(gameObject);
        }

        counter += 1;

        if((lastPosition == null || counter % 11 == 0))
        {
            lastPosition = gameObject.transform.position;
        }

        if (counter % 17 == 0 && lastPosition.x == gameObject.transform.position.x &&  lastPosition.y == gameObject.transform.position.y && lastPosition.z == gameObject.transform.position.z
            && rb.velocity.x == 0 && rb.velocity.x == 0 && rb.velocity.z == 0){
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        Vector3 refrectVec = Vector3.Reflect(this.lastVelocity, coll.contacts[0].normal);
        //Debug.Log(refrectVec);
        rb.velocity = refrectVec;
    }

}
