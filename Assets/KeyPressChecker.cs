using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class KeyPressChecker : MonoBehaviour
{

    public GameObject CannonballPrefab;
    public GameObject MatoPrefab;
    public GameObject BlockPrefab;
    public GameObject CannonPolePrefab;
    public GameObject WallPrefab;

    public AudioClip audioShot;

    private int shotPower1P = 400;
    private int shotPower2P = 400;
    private int shotAngle1P = 45;
    private int shotAngle2P = 45;

    private GameObject createdMato1P = null;
    private GameObject createdMato2P = null;
    private GameObject createdCannonball = null;

    private GameObject createdPole1P = null;
    private GameObject createdPole2P = null;

    private const float CHECK_INTERVAL = 0.01f;
    private float msgTimeCounter = 0;

    GameObject ShotParamText1P;
    GameObject ShotParamText2P;
    GameObject MessageText;

    private bool isTurnOfP1 = true;
    private float screenWidthUnits = -1;
    private float screenHeightUnits = -1;
    private const int PIXCELS_OF_UNIT = 100;
    private const int BLOCKS_NUM = 60;

    private const bool IS_MOUNTAIN = false;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();

        ShotParamText1P = GameObject.Find("ShotParamText1P");
        ShotParamText2P = GameObject.Find("ShotParamText2P");
        MessageText = GameObject.Find("MessageText");
        updateScreenSizeInfo();

        GameObject createdWall = Instantiate(WallPrefab, new Vector3(0, -0.25f * screenHeightUnits, 0), Quaternion.identity);
        createdWall.transform.localScale = new Vector3(0.1f, screenHeightUnits * 0.6f, 1);

        InvokeRepeating("checkKeyPress", CHECK_INTERVAL, CHECK_INTERVAL);
        placeMato();
    }

    public void placeMato()
    {
        if (createdMato1P != null)
        {
            Destroy(createdMato1P);
            createdMato1P = null;
        }
        if (createdMato2P != null)
        {
            Destroy(createdMato2P);
            createdMato2P = null;
        }
        if (createdPole1P != null)
        {
            Destroy(createdPole1P);
            createdPole1P = null;
        }
        if (createdPole2P != null)
        {
            Destroy(createdPole2P);
            createdPole2P = null;
        }

        createdMato1P = Instantiate(MatoPrefab, new Vector3(-0.499f * screenWidthUnits, -0.35f * screenHeightUnits, 0), Quaternion.identity);
        createdMato2P = Instantiate(MatoPrefab, new Vector3(0.499f * screenWidthUnits, -0.35f * screenHeightUnits, 0), Quaternion.identity);
        createdPole1P = Instantiate(CannonPolePrefab, new Vector3(-0.499f * screenWidthUnits, -0.499f * screenHeightUnits, 0), Quaternion.identity);
        createdPole2P = Instantiate(CannonPolePrefab, new Vector3(0.499f * screenWidthUnits, -0.499f * screenHeightUnits, 0), Quaternion.identity);
        //createdPole2P.transform.Rotate(new Vector3(0f, 0f, 180f));
        iTween.RotateTo(createdPole2P, iTween.Hash("z", 135f));
        iTween.RotateTo(createdPole1P, iTween.Hash("z", 45f));
        shotAngle1P = 45;
        shotAngle2P = 45;

        //画面上の障害物を全て消す
        var clones = GameObject.FindGameObjectsWithTag("block");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }

        //障害物を配置
        placeBlocksRandom();
    }

    private void placeBlocksRandom()
    {
        if (IS_MOUNTAIN)
        {
            for (int ii = 0; ii < 150; ii++)
            {
                float gauss_rand_x = 0;
                for(int jj=0; jj < 2; jj++)
                {
                    gauss_rand_x += Random.Range(-0.4f, 0.4f);
                }
                gauss_rand_x /= 2f;
                float y_cood = ((1f - Mathf.Abs(gauss_rand_x) / 0.4f) * 0.6f * screenHeightUnits * Random.Range(0f, 1f)) - (0.5f * screenHeightUnits);
                Instantiate(BlockPrefab, new Vector3(gauss_rand_x * screenWidthUnits, y_cood, 0), Quaternion.identity);
            }
        }
        else
        {
            for (int ii = 0; ii < BLOCKS_NUM; ii++)
            {
                Instantiate(BlockPrefab, new Vector3(Random.Range(-0.4f * screenWidthUnits, 0.4f * screenWidthUnits), Random.Range(-0.4f * screenHeightUnits, 0.5f * screenHeightUnits), 0), Quaternion.identity);
            }
        }
    }

    private void updateScreenSizeInfo()
    {
        screenWidthUnits = Screen.width / (float) PIXCELS_OF_UNIT;
        screenHeightUnits = Screen.height / (float) PIXCELS_OF_UNIT;
    }

    void checkKeyPress()
    {
        updateScreenSizeInfo();
 		if (Input.GetKeyDown (KeyCode.UpArrow)) {
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
 		if (Input.GetKeyDown (KeyCode.DownArrow)) {
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
 		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
            if (isTurnOfP1)
            {
                if (shotPower1P < 1000)  shotPower1P += 10;
            }
            else
            {
                if (shotPower2P < 1000)  shotPower2P += 10;
            }

		}
 		if (Input.GetKeyDown (KeyCode.RightArrow)) {
            if (isTurnOfP1)
            {
                if (shotPower1P > 0) shotPower1P -= 10;
            }
            else
            {
                if (shotPower2P > 0) shotPower2P -= 10;
            }

		}
/*
        if (Input.GetKeyDown(KeyCode.M))
        {
            placeMato();
            MessageText.GetComponent<Text>().text = "ギブアップか・・・";
        }
*/

        //一定間隔で定期的にクリアする
        msgTimeCounter += CHECK_INTERVAL;
        if(msgTimeCounter > 3f)
        {
            MessageText.GetComponent<Text>().text = "";
            msgTimeCounter = 0;
        }

        //的がユーザのプレイで消滅していたら再生成する
        if(createdMato1P == null)
        {
            placeMato();
        }
        if(createdMato2P == null)
        {
            placeMato();
        }
        ShotParamText1P.GetComponent<Text>().text = (isTurnOfP1? "@ ":"") + "Power: " + shotPower1P.ToString() + " Angle: " + shotAngle1P.ToString();
        ShotParamText2P.GetComponent<Text>().text = (!isTurnOfP1 ? "@ ":"") + "Power: " + shotPower2P.ToString() + " Angle: " + shotAngle2P.ToString();

 		if (Input.GetKeyDown(KeyCode.Space)) {
            shotNewCannonbool();
		}
    }

    void shotNewCannonbool()
    {
        if (createdCannonball != null)
        {
            return;
        }
        if (isTurnOfP1)
        {
            createdCannonball = Instantiate(CannonballPrefab, new Vector3(-0.499f * screenWidthUnits, -0.499f * screenHeightUnits, 0), Quaternion.identity);
            //createdCannonball = Instantiate(CannonballPrefab, new Vector3(-8f, -4.5f, 0), Quaternion.identity);
            float x_power = 1 * 2 * Mathf.Cos((Mathf.PI / 2f) * (shotAngle1P / 90f)) * shotPower1P;
            float y_power = 2f * Mathf.Sin((Mathf.PI / 2f) * (shotAngle1P / 90f)) * shotPower1P;
            createdCannonball.GetComponent<Rigidbody2D>().AddForce(new Vector2(x_power, y_power));
        }
        else // Turn of P2
        {
            createdCannonball = Instantiate(CannonballPrefab, new Vector3(0.499f * screenWidthUnits, -0.499f * screenHeightUnits, 0), Quaternion.identity);
            //createdCannonball = Instantiate(CannonballPrefab, new Vector3(8f, -4.5f, 0), Quaternion.identity);
            float x_power = -1 * 2 * Mathf.Cos((Mathf.PI / 2f) * (shotAngle2P / 90f)) * shotPower2P;
            float y_power = 2f * Mathf.Sin((Mathf.PI / 2f) * (shotAngle2P / 90f)) * shotPower2P;
            createdCannonball.GetComponent<Rigidbody2D>().AddForce(new Vector2(x_power, y_power));
        }
        isTurnOfP1 = !isTurnOfP1;

        audioSource.PlayOneShot(audioShot);
    }
}
