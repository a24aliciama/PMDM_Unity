using System.Collections.Generic;
using UnityEngine;

public class BallController0 : MonoBehaviour
{
    Rigidbody2D rb; 
    [SerializeField] float delay;
    [SerializeField] float force;
    [SerializeField] AudioClip sfxPaddle;
    [SerializeField] AudioClip sfxBrick;
    [SerializeField] AudioClip sfxWall;

    //[SerializeField] GameObject paddle;
    AudioSource audioSource;


    Dictionary<string, int> bricks = new Dictionary<string, int>(){
        {"BrickYellow", 10}, 
        {"BrickGreen", 15}, 
        {"BrickOrange", 20}, 
        {"BrickRed", 25}, 

    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        Invoke("ThrowBall", delay);
    }

    private void OnCollisionEnter2D(Collision2D other){
        string tag = other.gameObject.tag;
        if(bricks.ContainsKey(tag)){
            audioSource.clip = sfxBrick;
            audioSource.Play();
            Destroy(other.gameObject);
        }else if(tag == "Wall-T" || tag == "Wall-L" || tag == "Wall-R"){
            audioSource.clip = sfxWall;
            audioSource.Play();
        }
    }

    private void ThrowBall(){
        transform.position = Vector3.zero;
        rb.linearVelocity = Vector2.zero;
        float dirX, dirY = -1; 
        dirX = Random.Range(0,2) ==0? -1:1;
        Vector2 dir = new Vector2(dirX, dirY);
        dir.Normalize();
        rb.AddForce(dir*force, ForceMode2D.Impulse);
    }
        // Update is called once per frame
    void Update()
    {
        
    }
}
