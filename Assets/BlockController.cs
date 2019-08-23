using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    private const float TAIKYUDO_MAX = 6f;
    private const float CANONBALL_MASS = 1f;
    private float taikyudo = TAIKYUDO_MAX;

    private AudioSource audioSource;
    public AudioClip audioBreak;

    // Start is called before the first frame update
    void Start()
    {
        //audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        float vx = other.relativeVelocity.x;
        float vy = other.relativeVelocity.y;
        float recv_force = Mathf.Sqrt(vx * vx + vy * vy);

        Rigidbody2D ball = other.gameObject.GetComponent<Rigidbody2D>();
        float slowDownRatio = (recv_force - taikyudo) / recv_force;
        if(slowDownRatio < 0) slowDownRatio = 0;
        if(slowDownRatio > 1) slowDownRatio = 1;
        ball.velocity = new Vector2(vx * slowDownRatio, vy * slowDownRatio);

        taikyudo -= recv_force;

        Color color = gameObject.GetComponent<Renderer>().material.color;
        color.a = (taikyudo / TAIKYUDO_MAX);
        if(color.a < 0.2)
        {
            color.a = 0.2f;
        }
        gameObject.GetComponent<Renderer>().material.color = color;

        if (taikyudo <= 0)
        {
            audioSource = other.gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioBreak);
            //StartCoroutine("DelayedDestroy");
            Destroy(gameObject);
        }
    }

    IEnumerator DelayedDestroy(){
 
        yield return new WaitForSeconds(1f);  //wait 1sec
        Destroy(gameObject);
    }
}
