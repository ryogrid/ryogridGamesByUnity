using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballController : MonoBehaviour
{

    private float screenWidthUnits = -1;
    private float screenHeightUnits = -1;
    private const int PIXCELS_OF_UNIT = 100;

    private void updateScreenSizeInfo()
    {
        screenWidthUnits = Screen.width / (float) PIXCELS_OF_UNIT;
        screenHeightUnits = Screen.height / (float) PIXCELS_OF_UNIT;
    }

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500, 700));
    }

    // Update is called once per frame
    void Update()
    {
        updateScreenSizeInfo();
        //if (transform.position.x < -8 || transform.position.x > 8.5 || transform.position.y < -5.5) {
        if (transform.position.x < -0.5f * screenWidthUnits || transform.position.x > 0.5f * screenWidthUnits || transform.position.y < -0.5f * screenHeightUnits) {
            Destroy(gameObject);
        }
    }
}
