using UnityEngine;

public class BallController : MonoBehaviour
{

    private Rigidbody2D rb;

    const float MIN_ANG = 25.0f;
    const float MAX_ANG = 40.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        ThrowBall();
       
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void ThrowBall(){
        transform.position = new Vector3(0,0,0);

        float angulo = Random.Range(MIN_ANG,MAX_ANG) * Mathf.Deg2Rad;

        float x = Mathf.Cos(angulo);
        float y = Mathf.Sin(angulo);

        int directionX = Random.Range(0,-2)== 0 ? 1 : -1; //el dos no cuenta
        x = x*directionX;

        int directionY = Random.Range(0,-2)== 0 ? 1 : -1; //el dos no cuenta
        y = y*directionY;

        rb.AddForce (new Vector2(x,y), ForceMode2D.Impulse);
    }

    
    private void OnCollisionEnter2D(Collision2D collision){

        string tag = collision.gameObject.tag;

        if(tag.Equals("PaddleLeft")){
            Debug.Log ("collision Pala Izq");
        }else if(tag.Equals("PaddleRight")){
            Debug.Log ("collision Pala derecha");
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collider){
        ThrowBall();
        Debug.Log ("Puntooooo");
    }
}
