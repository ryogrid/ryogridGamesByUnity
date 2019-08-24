using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 lastPosition;
    private int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
         this.rb = this.GetComponent<Rigidbody>();       
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
            && this.rb.velocity.x == 0 && this.rb.velocity.x == 0 && this.rb.velocity.z == 0){
            Destroy(gameObject);
        }
    }
}
