using System.Collections;
using TMPro;
using UnityEngine;

public class TextColor : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] float duration; //Tiempo que transcurre entre cambio de color 
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine("ChangeColor");
    }

    IEnumerator ChangeColor(){
        float t =0; 
        while(t < duration){
            t+=Time.deltaTime;
            text.color = Color.Lerp(Color.black, Color.white, t/duration); 
            yield return null; 
        }

        StartCoroutine("ChangeColor");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
