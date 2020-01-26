using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCheckerControler : MonoBehaviour
{
    public GameObject BallPrefab;
    public GameObject RacketPrefab;
    private GameObject ScoreText;

    private GameObject createdBall = null;
    private GameObject createdRacket = null;
    private GameObject RacketZLineHorizontal = null;
    private GameObject RacketZLineVertical = null;

    private const float CHECK_INTERVAL = 0.01f;
    //private const float MOVE_DISTANCE = 2f;
    private const float MOVE_DISTANCE = 0.05f;
    private const float PIXCELS_OF_UNIT = 100f;

    // Start is called before the first frame update
    void Start()
    {
        createdRacket = Instantiate(RacketPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        ScoreText = GameObject.Find("ScoreMeshText");
        ScoreText.GetComponent<TextMesh>().text = "0 回";

        RacketZLineHorizontal = GameObject.Find("RacketZLineHorizontal");
        RacketZLineVertical = GameObject.Find("RacketZLineVertical");

        //createdRacket.GetComponent<Renderer>().material.color = Color.blue;
        InvokeRepeating("checkKeyPress", CHECK_INTERVAL, CHECK_INTERVAL);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float getConvertedMouseCoodX()
    {
        return ((Input.mousePosition.x / Screen.width) * 5f - 2.5f) * 1.3f;
    }

    float getConvertedMouseCoodY()
    {
        return (Input.mousePosition.y / Screen.height) * 5f - 2.5f;
    }

    void checkKeyPress()
    {
        /*
                //updateScreenSizeInfo();
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    createdRacket.transform.position = new Vector3 (createdRacket.transform.position.x, createdRacket.transform.position.y + MOVE_DISTANCE, createdRacket.transform.position.z);
                    //iTween.MoveBy(createdRacket, iTween.Hash("y", MOVE_DISTANCE));
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    createdRacket.transform.position = new Vector3 (createdRacket.transform.position.x, createdRacket.transform.position.y - MOVE_DISTANCE, createdRacket.transform.position.z);
                    //iTween.MoveBy(createdRacket, iTween.Hash("y", -1 * MOVE_DISTANCE));
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    createdRacket.transform.position = new Vector3 (createdRacket.transform.position.x - MOVE_DISTANCE, createdRacket.transform.position.y, createdRacket.transform.position.z);
                    //iTween.MoveBy(createdRacket, iTween.Hash("x", -1 * MOVE_DISTANCE));
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    createdRacket.transform.position = new Vector3 (createdRacket.transform.position.x + MOVE_DISTANCE, createdRacket.transform.position.y, createdRacket.transform.position.z);
                    //iTween.MoveBy(createdRacket, iTween.Hash("x", MOVE_DISTANCE));
                }
        */

        Vector2 stickVal2D = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        //createdRacket.transform.position = new Vector3 (getConvertedMouseCoodX(), getConvertedMouseCoodY(), createdRacket.transform.position.z);
        createdRacket.transform.position = new Vector2(createdRacket.transform.position.x + 0.1f * stickVal2D.x, createdRacket.transform.position.y + 0.1f * stickVal2D.y);

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

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    shotNewBall();
        //}
        //if (Input.anyKeyDown) {
        //    shotNewBall();
        //}
        if (OVRInput.Get(OVRInput.Button.One)) {
            shotNewBall();
        }

        RacketZLineHorizontal.transform.position = new Vector3(0, createdRacket.transform.position.y, createdRacket.transform.position.z);
        RacketZLineVertical.transform.position = new Vector3(createdRacket.transform.position.x, 0, createdRacket.transform.position.z);
    }

    //private void reCreateRacket()
    //{
    //    if(createdRacket != null)
    //    {
    //        Destroy(createdRacket);
    //        createdRacket = null;            
    //    }
    //    createdRacket = Instantiate(RacketPrefab, new Vector3(0, 0, -2f), Quaternion.identity);
    //}

    void shotNewBall()
    {
        if (createdBall != null)
        {
            return;
        }
        createdBall = Instantiate(BallPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        //createdCannonball = Instantiate(CannonballPrefab, new Vector3(-8f, -4.5f, 0), Quaternion.identity);
        //float shotPowerXY = 60f;
        float shotVerocityFixedZ = BallController.Z_CONSTANT_VELOCITY;
        //float shotPowerFixedZ = 1000f;

        float shotAngle = Random.Range(0f, 360f);
        /*
                float x_power = Mathf.Cos(2 * Mathf.PI * (shotAngle / 360f)) * shotPowerXY;
                float y_power = Mathf.Sin(2 * Mathf.PI  * (shotAngle / 360f)) * shotPowerXY;
        */
        float x_speed = Mathf.Cos(2 * Mathf.PI * (shotAngle / 360f)) * shotVerocityFixedZ;
        float y_speed = Mathf.Sin(2 * Mathf.PI * (shotAngle / 360f)) * shotVerocityFixedZ;

        createdBall.GetComponent<Renderer>().material.color = Color.black;
        //createdBall.GetComponent<Rigidbody>().AddForce(new Vector3(x_power, y_power, shotVerocityFixedZ));
        createdBall.GetComponent<Rigidbody>().velocity = new Vector3(x_speed, y_speed, shotVerocityFixedZ);

        //createdBall.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, shotPowerFixedZ));

        ScoreText.GetComponent<TextMesh>().text = "0 回";
        //内部状態を初期化するために作り直す
        //reCreateRacket();
    }
}
