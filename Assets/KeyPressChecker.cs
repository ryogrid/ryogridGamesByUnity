using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class KeyPressChecker : MonoBehaviour
{

    public GameObject CannonballPrefab;
    public GameObject MatoPrefab;

    private int shotPower = 400;
    private int shotAngle = 45;

    private GameObject createdMato = null;
    private GameObject createdCannonball = null;

    private float CHECK_INTERVAL = 0.01f;
    private float msgTimeCounter = 0;

    GameObject ShotParamText;
    GameObject MessageText;

    // Start is called before the first frame update
    void Start()
    {
        ShotParamText = GameObject.Find("ShotParamText");
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
            if(shotAngle < 90)
            {
                shotAngle += 1;
            }
		}
 		if (Input.GetKeyDown (KeyCode.DownArrow)) {
            if(shotAngle > 0)
            {
                shotAngle -= 1;
            }
		}
 		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
            if(shotPower < 700)
            {
                shotPower += 10;
            }
		}
 		if (Input.GetKeyDown (KeyCode.RightArrow)) {
            if(shotPower > 0)
            {
                shotPower -= 10;
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

        ShotParamText.GetComponent<Text>().text = "Power: " + shotPower.ToString() + " Angle: " + shotAngle.ToString();
    }

    void shotNewCannonbool()
    {
        if (createdCannonball != null)
        {
            return;
        }
        createdCannonball = Instantiate(CannonballPrefab, new Vector3(8.5f, -4.5f, 0), Quaternion.identity);
        float x_power = -1 * 2 * Mathf.Cos((Mathf.PI / 2f) * (shotAngle / 90f)) * shotPower;
        float y_power = 2f * Mathf.Sin((Mathf.PI / 2f) * (shotAngle / 90f)) * shotPower;

        //cannon.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500, 700));
        createdCannonball.GetComponent<Rigidbody2D>().AddForce(new Vector2(x_power, y_power));
    }
}
