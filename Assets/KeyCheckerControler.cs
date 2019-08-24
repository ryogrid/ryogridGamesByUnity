using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCheckerControler : MonoBehaviour
{

    public GameObject BallPrefab;
    private const float CHECK_INTERVAL = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("checkKeyPress", CHECK_INTERVAL, CHECK_INTERVAL);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void checkKeyPress()
    {
/*
        updateScreenSizeInfo();
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isTurnOfP1)
            {
                if (shotAngle1P < 90)
                {
                    shotAngle1P += 1;
                    //createdPole1P.transform.Rotate(new Vector3(0f, 0f, 1f));
                    iTween.RotateTo(createdPole1P, iTween.Hash("z", shotAngle1P));
                }
            }
            else
            {
                if (shotAngle2P < 90)
                {
                    shotAngle2P += 1;
                    //createdPole2P.transform.Rotate(new Vector3(0f, 0f, -1f));
                    iTween.RotateTo(createdPole2P, iTween.Hash("z", 180f - shotAngle2P));
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (isTurnOfP1)
            {
                if (shotAngle1P > 0)
                {
                    shotAngle1P -= 1;
                    //createdPole1P.transform.Rotate(new Vector3(0f, 0f, -1f));
                    iTween.RotateTo(createdPole1P, iTween.Hash("z", shotAngle1P));
                }
            }
            else
            {
                if (shotAngle2P > 0)
                {
                    shotAngle2P -= 1;
                    //createdPole1P.transform.Rotate(new Vector3(0f, 0f, 1f));
                    iTween.RotateTo(createdPole2P, iTween.Hash("z", 180f - shotAngle2P));
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (isTurnOfP1)
            {
                if (shotPower1P < 1000) shotPower1P += 10;
            }
            else
            {
                if (shotPower2P < 1000) shotPower2P += 10;
            }

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (isTurnOfP1)
            {
                if (shotPower1P > 0) shotPower1P -= 10;
            }
            else
            {
                if (shotPower2P > 0) shotPower2P -= 10;
            }

        }

        //一定間隔で定期的にクリアする
        msgTimeCounter += CHECK_INTERVAL;
        if (msgTimeCounter > 3f)
        {
            MessageText.GetComponent<Text>().text = "";
            msgTimeCounter = 0;
        }

        //的がユーザのプレイで消滅していたら再生成する
        if (createdMato1P == null)
        {
            placeMato();
        }
        if (createdMato2P == null)
        {
            placeMato();
        }
        ShotParamText1P.GetComponent<Text>().text = (isTurnOfP1 ? "@ " : "") + "Power: " + shotPower1P.ToString() + " Angle: " + shotAngle1P.ToString();
        ShotParamText2P.GetComponent<Text>().text = (!isTurnOfP1 ? "@ " : "") + "Power: " + shotPower2P.ToString() + " Angle: " + shotAngle2P.ToString();
*/
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shotNewBall();
        }
    }

    void shotNewBall()
    {
        GameObject createdBall = Instantiate(BallPrefab, new Vector3(0, 0, -2), Quaternion.identity);
        //createdCannonball = Instantiate(CannonballPrefab, new Vector3(-8f, -4.5f, 0), Quaternion.identity);
        float shotPowerXY = 100f;
        float shotPowerFixedZ = 20;
        float shotAngle = Random.Range(0f, 150f);
        float x_power = 1 * 2 * Mathf.Cos(Mathf.PI * ((shotAngle + 20) / 180f)) * shotPowerXY;
        float y_power = 2f * Mathf.Sin(Mathf.PI  * ((shotAngle + 20) / 180f)) * shotPowerXY;

        createdBall.GetComponent<Rigidbody2D>().AddForce(new Vector3(x_power, y_power, shotPowerFixedZ));
    }
}
