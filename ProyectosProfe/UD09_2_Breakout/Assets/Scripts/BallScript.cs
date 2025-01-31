using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallScript : MonoBehaviour
{
    Rigidbody2D rb; 
    [SerializeField] float delay;
    [SerializeField] float force;
    [SerializeField] float forceIncrease;
   // [SerializeField] GameManager gameManager;

    int brickCount;
    [SerializeField] AudioClip sfxPaddle;
    [SerializeField] AudioClip sfxBrick;
    [SerializeField] AudioClip sfxWall;
    [SerializeField] AudioClip sfxFail;

    //[SerializeField] GameObject paddle;
    GameObject paddle;
    AudioSource audioSource;

    private bool reduced = false;

    Dictionary<string, int> bricks = new Dictionary<string, int>(){
        {"BrickYellow", 10}, 
        {"BrickGreen", 15}, 
        {"BrickOrange", 20}, 
        {"BrickRed", 25}, 
        {"BrickPass", -1}, 

    };

    int hitCounts = 0;
    int sceneId;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        Invoke("throwBall", delay);
        paddle = GameObject.FindWithTag("Paddle");
        sceneId = SceneManager.GetActiveScene().buildIndex;
    }

    public void TamPaddle(bool reduce){
        reduced = reduce;
        Vector3 tamPaddleActual  = paddle.transform.localScale; //1.5, 0.2
        paddle.transform.localScale = reduce ? 
            new Vector3(tamPaddleActual.x*0.5f, tamPaddleActual.y, tamPaddleActual.z) : 
            new Vector3(tamPaddleActual.x*2f, tamPaddleActual.y, tamPaddleActual.z) ;
    }

    private void OnCollisionEnter2D(Collision2D other){
        string tag = other.gameObject.tag;
        if(bricks.ContainsKey(tag)){
            audioSource.clip = sfxBrick;
            audioSource.Play();
            GameManager.UpdateScore(bricks[tag]);
            Destroy(other.gameObject);
            brickCount++; 
            if(brickCount == GameManager.totalBricks[sceneId]){
               SceneManager.LoadScene(sceneId+1); 
            }
        }
        if(tag == "Paddle"){
            hitCounts++;
            if(hitCounts % 4 == 0){
                rb.AddForce(rb.linearVelocity*forceIncrease, ForceMode2D.Impulse);
            }
            Vector3 positionPaddle = other.gameObject.transform.position;
            Vector2 contact = other.GetContact(0).point;
            if(rb.linearVelocity.x < 0 && contact.x > positionPaddle.x ||
                rb.linearVelocity.x > 0 && contact.x < positionPaddle.x){
                rb.linearVelocity = new Vector2(-rb.linearVelocityX, rb.linearVelocityY);
            }

            audioSource.clip = sfxPaddle;
            audioSource.Play();
            
        }else if(tag == "Wall-T" || tag == "Wall-L" || tag == "Wall-R" || tag == "Brick-Rock" ){
            audioSource.clip = sfxWall;
            audioSource.Play();
        }
        if(!reduced && tag == "Wall-T"){
            TamPaddle(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void throwBall(){
        transform.position = Vector3.zero;
        rb.linearVelocity = Vector2.zero;
        float dirX, dirY = -1; 
        dirX = Random.Range(0,2) ==0? -1:1;
        Vector2 dir = new Vector2(dirX, dirY);
        dir.Normalize();
        rb.AddForce(dir*force, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag=="Wall-B"){
            GameManager.UpdateLives();
            Invoke("throwBall", delay);
            if(reduced){
               TamPaddle(false);
            }
        }
        if(other.tag=="BrickPass"){
            if(bricks.ContainsKey(other.tag)){
                audioSource.clip = sfxBrick;
                audioSource.Play();
                GameManager.UpdateScore(bricks[other.tag]);
                Destroy(other.gameObject);
            }
        }
    }
    
}
