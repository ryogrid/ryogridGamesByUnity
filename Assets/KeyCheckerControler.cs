using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCheckerControler : MonoBehaviour
{

    public GameObject BallPrefab;
    public GameObject RacketPrefab;

    private GameObject createdBall = null;
    private GameObject createdRacket = null;
    private const float CHECK_INTERVAL = 0.01f;
    private const float MOVE_DISTANCE = 2f;

    // Start is called before the first frame update
    void Start()
    {
        createdRacket = Instantiate(RacketPrefab, new Vector3(0, 0, -2f), Quaternion.identity);
        //createdRacket.GetComponent<Renderer>().material.color = Color.blue;
        InvokeRepeating("checkKeyPress", CHECK_INTERVAL, CHECK_INTERVAL);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void checkKeyPress()
    {

        //updateScreenSizeInfo();
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //createdRacket.transform.position += new Vector3 (0, MOVE_DISTANCE, 0);
            iTween.MoveBy(createdRacket, iTween.Hash("y", MOVE_DISTANCE));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            //createdRacket.transform.position += new Vector3 (0, -1 * MOVE_DISTANCE, 0);
            iTween.MoveBy(createdRacket, iTween.Hash("y", -1 * MOVE_DISTANCE));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //createdRacket.transform.position += new Vector3 (-1f * MOVE_DISTANCE, 0, 0);
            iTween.MoveBy(createdRacket, iTween.Hash("x", -1 * MOVE_DISTANCE));
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //createdRacket.transform.position += new Vector3 (MOVE_DISTANCE, 0, 0);
            iTween.MoveBy(createdRacket, iTween.Hash("x", MOVE_DISTANCE));
        }

/*
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
        if (createdBall != null)
        {
            return;
        }
        createdBall = Instantiate(BallPrefab, new Vector3(0, 0, -1.5f), Quaternion.identity);
        //createdCannonball = Instantiate(CannonballPrefab, new Vector3(-8f, -4.5f, 0), Quaternion.identity);
        float shotPowerXY = 90f;
        float shotPowerFixedZ = 80;
        float shotAngle = Random.Range(0f, 360f);
        float x_power = Mathf.Cos(2 * Mathf.PI * (shotAngle / 360f)) * shotPowerXY;
        float y_power = Mathf.Sin(2 * Mathf.PI  * (shotAngle / 360f)) * shotPowerXY;

        createdBall.GetComponent<Renderer>().material.color = Color.black;
        createdBall.GetComponent<Rigidbody>().AddForce(new Vector3(x_power, y_power, shotPowerFixedZ));
        createdBall.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, shotPowerFixedZ));
    }
}
