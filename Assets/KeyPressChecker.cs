using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class KeyPressChecker : MonoBehaviour
{

    public GameObject CannonballPrefab;
    public GameObject MatoPrefab;

    private int shotPowerP1 = 400;
    private int shotPowerP2 = 400;
    private int shotAngleP1 = 45;
    private int shotAngleP2 = 45;

    private GameObject createdMato = null;
    private GameObject createdCannonball = null;

    private float CHECK_INTERVAL = 0.01f;
    private float msgTimeCounter = 0;

    GameObject ShotParamText1P;
    GameObject ShotParamText2P;
    GameObject MessageText;

    private bool isTurnOfP1 = true;

    // Start is called before the first frame update
    void Start()
    {
        ShotParamText1P = GameObject.Find("ShotParamText1P");
        ShotParamText2P = GameObject.Find("ShotParamText2P");
        MessageText = GameObject.Find("MessageText");
        InvokeRepeating("checkKeyPress", CHECK_INTERVAL, CHECK_INTERVAL);
        placeMato();
    }

    public void placeMato()
    {
        if(createdMato != null)
        {
            Destroy(createdMato);
            createdMato = null;
        }
        //他にもインスタンスが存在する場合があるので、それもケア (普通に的を消した場合)
        GameObject tmpMato = GameObject.Find("MatoPrefab");
        if(tmpMato != null)
        {
            Destroy(tmpMato);
        }

        createdMato = Instantiate(MatoPrefab, new Vector3(Random.Range(-7f, 7f), Random.Range(-4f, 4f), 0), Quaternion.identity);
    }


    void checkKeyPress()
    {
 		if (Input.GetKeyDown (KeyCode.Space)) {
            shotNewCannonbool();
		}
 		if (Input.GetKeyDown (KeyCode.UpArrow)) {
            if (isTurnOfP1)
            {
                if (shotAngleP1 < 90) shotAngleP1 += 1;
            }
            else
            {
                if (shotAngleP2 < 90) shotAngleP2 += 1;
            }

		}
 		if (Input.GetKeyDown (KeyCode.DownArrow)) {
            if (isTurnOfP1)
            {
                if (shotAngleP1 > 0) shotAngleP1 -= 1;
            }
            else
            {
                if (shotAngleP2 > 0) shotAngleP2 -= 1;
            }
		}
 		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
            if (isTurnOfP1)
            {
                if (shotPowerP1 < 700) shotPowerP1 += 10;
            }
            else
            {
                if (shotPowerP2 < 700) shotPowerP2 += 10;
            }

		}
 		if (Input.GetKeyDown (KeyCode.RightArrow)) {
            if (isTurnOfP1)
            {
                if (shotPowerP1 > 0) shotPowerP1 -= 10;
            }
            else
            {
                if (shotPowerP2 > 0) shotPowerP2 -= 10;
            }

		}
        if (Input.GetKeyDown(KeyCode.M))
        {
            placeMato();
            MessageText.GetComponent<Text>().text = "ギブアップか・・・";
        }

        //一定間隔で定期的にクリアする
        msgTimeCounter += CHECK_INTERVAL;
        if(msgTimeCounter > 3f)
        {
            MessageText.GetComponent<Text>().text = "";
            msgTimeCounter = 0;
        }

        //的がユーザのプレイで消滅していたら再生成する
        if(createdMato == null)
        {
            placeMato();
        }
        ShotParamText1P.GetComponent<Text>().text = (isTurnOfP1? "@ ":"") + "Power: " + shotPowerP1.ToString() + " Angle: " + shotAngleP1.ToString();
        ShotParamText2P.GetComponent<Text>().text = (!isTurnOfP1 ? "@ ":"") + "Power: " + shotPowerP2.ToString() + " Angle: " + shotAngleP2.ToString();
    }

    void shotNewCannonbool()
    {
        if (createdCannonball != null)
        {
            return;
        }
        if (isTurnOfP1)
        {
            createdCannonball = Instantiate(CannonballPrefab, new Vector3(8.5f, -4.5f, 0), Quaternion.identity);
            //createdCannonball = Instantiate(CannonballPrefab, new Vector3(10f, -4.5f, 0), Quaternion.identity);
            float x_power = 1 * 2 * Mathf.Cos((Mathf.PI / 2f) * (shotAngleP1 / 90f)) * shotPowerP1;
            float y_power = 2f * Mathf.Sin((Mathf.PI / 2f) * (shotAngleP1 / 90f)) * shotPowerP1;
        //cannon.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500, 700));
            createdCannonball.GetComponent<Rigidbody2D>().AddForce(new Vector2(x_power, y_power));
            isTurnOfP1 = false;
        }
        else // Turn of P2
        {
            createdCannonball = Instantiate(CannonballPrefab, new Vector3(8.5f, -4.5f, 0), Quaternion.identity);
            //createdCannonball = Instantiate(CannonballPrefab, new Vector3(10f, -4.5f, 0), Quaternion.identity);
            float x_power = -1 * 2 * Mathf.Cos((Mathf.PI / 2f) * (shotAngleP2 / 90f)) * shotPowerP2;
            float y_power = 2f * Mathf.Sin((Mathf.PI / 2f) * (shotAngleP2 / 90f)) * shotPowerP2;
            //cannon.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500, 700));
            createdCannonball.GetComponent<Rigidbody2D>().AddForce(new Vector2(x_power, y_power));
            isTurnOfP1 = true;
        }
        //isTurnOfP1 = !isTurnOfP1;
    }
}
