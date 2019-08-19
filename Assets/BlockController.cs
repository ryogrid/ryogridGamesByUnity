using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    private const float TAIKYUDO_MAX = 20f;
    private const float CANONBALL_MASS = 1f;
    private float taikyudo = TAIKYUDO_MAX;

    // Start is called before the first frame update
    void Start()
    {
        //taikyudo = TAIKYUDO_MAX;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log(other.relativeVelocity);
        float vx = other.relativeVelocity.x;
        float vy = other.relativeVelocity.y;
        float recv_force = Mathf.Sqrt(vx * vx + vy * vy);

        Rigidbody2D ball = other.gameObject.GetComponent<Rigidbody2D>();
        float slowDownRatio = (recv_force - taikyudo) / recv_force;
        ball.velocity = new Vector2(vx * slowDownRatio, vy * slowDownRatio);

        taikyudo -= recv_force;

        Color color = gameObject.GetComponent<Renderer>().material.color;
        color.a = (taikyudo / TAIKYUDO_MAX);
        gameObject.GetComponent<Renderer>().material.color = color;

        //Debug.Log(recv_force);
        //Destroy(gameObject);

        if (taikyudo <= 0)
        {
            Destroy(gameObject);
        }
    }
}
