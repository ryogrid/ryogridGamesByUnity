﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballController : MonoBehaviour
{

    private float screenWidthUnits = -1;
    private float screenHeightUnits = -1;
    private const int PIXCELS_OF_UNIT = 100;
    private Vector2 lastVelocity;
    private Rigidbody2D rb;
    private Vector3 lastPosition;

    void Start()
    {
        //lastPositioin = gameObject.transform.position;
    }

    private void updateScreenSizeInfo()
    {
        screenWidthUnits = Screen.width / (float) PIXCELS_OF_UNIT;
        screenHeightUnits = Screen.height / (float) PIXCELS_OF_UNIT;
    }

    // Update is called once per frame
    void Update()
    {
        updateScreenSizeInfo();
        //if (transform.position.x < -8 || transform.position.x > 8.5 || transform.position.y < -5.5) {
        if (transform.position.x < -0.5f * screenWidthUnits || transform.position.x > 0.5f * screenWidthUnits || transform.position.y < -0.5f * screenHeightUnits) {
            Destroy(gameObject);
        }

        if (lastPosition != null && lastPosition.Equals(gameObject.transform.position)){
            Destroy(gameObject);
        }

        lastPosition = gameObject.transform.position;
    }
}
