using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketController : MonoBehaviour
{
    private int BoundCount;
    // Start is called before the first frame update
    void Start()
    {
        BoundCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        BoundCount++;
        GameObject.Find("ScoreMeshText").GetComponent<TextMesh>().text =  BoundCount.ToString() +  " 回";
    }
}
