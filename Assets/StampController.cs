using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampController : MonoBehaviour
{

    private int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(counter > 50)
        if(counter > 15)
        {
            Destroy(gameObject);
            return;
        }
        counter++;
    }
}
