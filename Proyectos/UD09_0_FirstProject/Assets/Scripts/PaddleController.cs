using UnityEngine;

public class PaddleController : MonoBehaviour
{
const float MAX_Y=4.00f ;
const float MIN_Y=-4.00f ;

[SerializeField] float speed = 9f;
// Start is called once before the first execution of Update after the MonoBehaviour is created
void Start()
{

}

// Update is called once per frame
void Update()
{
if(Input.GetKey("up") && transform.position.y < MAX_Y){
transform.Translate(Vector3.up * speed * Time.deltaTime);
}
if(Input.GetKey("down") && transform.position.y > MIN_Y){
transform.Translate(Vector3.down * speed * Time.deltaTime);
}
}
}
