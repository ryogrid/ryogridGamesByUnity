using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject BallZLine;
    private GameObject BallZLineHorizontal;
    private GameObject BallZLineVertical;
    private Vector3 lastVelocity;
    private Vector3 lastPosition;
    private int counter = 0;

    private const float Z_CONSTANT_VELOCITY = 2.5f;
    private const float Z_STRANGE_VELOCITY_THRESHOLD = 0.1f;
    private const float XY_VELOCITY_LATIO_UPPER = 1f;
    private const float XY_VELOCITY_LATIO_BOTTOM = 0.2f;

    private AudioSource audioSource;
    public AudioClip audioBound;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        BallZLine = GameObject.Find("BallZLine");
        BallZLineHorizontal = GameObject.Find("BallZLineHorizontal");
        BallZLineVertical = GameObject.Find("BallZLineVertical");
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    //一定秒数ごとに呼び出される. デフォルトは20ms間隔らしい
    void FixedUpdate()
    {
        //ボールのスピードをいい感じに調整する
        adjustBallSpeed();

        lastVelocity = rb.velocity;
        //iTween.MoveTo(BallZLine, iTween.Hash("x", this.transform.position.x, "y", this.transform.position.y));
        BallZLine.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        BallZLineHorizontal.transform.position = new Vector3(0, this.transform.position.y, this.transform.position.z);
        BallZLineVertical.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
    }

    private void resetBallZLine()
    {
        BallZLine.transform.position = new Vector3(0, 0, 0);
        BallZLineHorizontal.transform.position = new Vector3(0, 0, 0);
        BallZLineVertical.transform.position = new Vector3(0, 0, 0);
    }

    private void adjustBallSpeed()
    {
        float vx = rb.velocity.x;
        float vy = rb.velocity.y;
        float vz = rb.velocity.z;
        float vz_abs = Mathf.Abs(vz);
        /*
                if(Mathf.Abs(vx) >= XY_VELOCITY_LATIO_UPPER * Z_CONSTANT_VELOCITY)
                {
                    vx = vx >= 0 ? XY_VELOCITY_LATIO_UPPER * Z_CONSTANT_VELOCITY : -1 * XY_VELOCITY_LATIO_UPPER * Z_CONSTANT_VELOCITY;
                }
                if(Mathf.Abs(vx) <= XY_VELOCITY_LATIO_BOTTOM * Z_CONSTANT_VELOCITY)
                {
                    vx = vx >= 0 ? XY_VELOCITY_LATIO_BOTTOM * Z_CONSTANT_VELOCITY : -1 * XY_VELOCITY_LATIO_BOTTOM * Z_CONSTANT_VELOCITY;
                }

                if(Mathf.Abs(vy) >= XY_VELOCITY_LATIO_UPPER * Z_CONSTANT_VELOCITY)
                {
                    vy = vy >= 0 ? XY_VELOCITY_LATIO_UPPER * Z_CONSTANT_VELOCITY : -1 * XY_VELOCITY_LATIO_UPPER * Z_CONSTANT_VELOCITY;
                }
                if(Mathf.Abs(vy) <= XY_VELOCITY_LATIO_BOTTOM * Z_CONSTANT_VELOCITY)
                {
                    vy = vy >= 0 ? XY_VELOCITY_LATIO_BOTTOM * Z_CONSTANT_VELOCITY : -1 * XY_VELOCITY_LATIO_BOTTOM * Z_CONSTANT_VELOCITY;
                }
        */
        if (vz_abs <= Z_CONSTANT_VELOCITY) {
            // 突き抜け等の可能性があるため必ず手前方向に移動させる
            vz_abs = Z_CONSTANT_VELOCITY;
            vz = -1 * Z_CONSTANT_VELOCITY;
        }
        else if (vz_abs <= Z_CONSTANT_VELOCITY){
            vz_abs = Z_CONSTANT_VELOCITY;
            vz = vz >= 0 ? Z_CONSTANT_VELOCITY : -1 * Z_CONSTANT_VELOCITY;
        }

        if(Mathf.Abs(vx) >= XY_VELOCITY_LATIO_UPPER * vz_abs)
        {
            vx = vx >= 0 ? XY_VELOCITY_LATIO_UPPER * vz_abs : -1 * XY_VELOCITY_LATIO_UPPER * vz_abs;
        }
        if(Mathf.Abs(vx) <= XY_VELOCITY_LATIO_BOTTOM * vz_abs)
        {
            vx = vx >= 0 ? XY_VELOCITY_LATIO_BOTTOM * vz_abs : -1 * XY_VELOCITY_LATIO_BOTTOM * vz_abs;
        }

        if(Mathf.Abs(vy) >= XY_VELOCITY_LATIO_UPPER * vz_abs)
        {
            vy = vy >= 0 ? XY_VELOCITY_LATIO_UPPER * vz_abs : -1 * XY_VELOCITY_LATIO_UPPER * vz_abs;
        }
        if(Mathf.Abs(vy) <= XY_VELOCITY_LATIO_BOTTOM * vz_abs)
        {
            vy = vy >= 0 ? XY_VELOCITY_LATIO_BOTTOM * vz_abs : -1 * XY_VELOCITY_LATIO_BOTTOM * vz_abs;
        }

        rb.velocity = new Vector3(vx, vy, vz);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < -3f)
        {
            resetBallZLine();
            Destroy(gameObject);
        }

        counter += 1;

        if((lastPosition == null || counter % 11 == 0))
        {
            lastPosition = gameObject.transform.position;
        }

        if (counter % 17 == 0 && lastPosition.x == gameObject.transform.position.x &&  lastPosition.y == gameObject.transform.position.y && lastPosition.z == gameObject.transform.position.z
            && rb.velocity.x == 0 && rb.velocity.x == 0 && rb.velocity.z == 0){
            resetBallZLine();
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        Vector3 refrectVec = Vector3.Reflect(this.lastVelocity, coll.contacts[0].normal);
        //Debug.Log(refrectVec);
        refrectVec.x *= 1.01f;
        refrectVec.y *= 1.01f;
        refrectVec.z *= 1.01f;
        rb.velocity = refrectVec;
        audioSource.PlayOneShot(audioBound);
    }

}
