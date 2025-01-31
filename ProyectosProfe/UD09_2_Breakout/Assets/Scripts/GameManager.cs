using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static int score {get; private set;} = 0; 
    public static int lives {get; private set;}= 3;  

    public static List<int> totalBricks = new List<int> {0, 27, 19};
    [SerializeField] TextMeshProUGUI txtLives;
    [SerializeField] TextMeshProUGUI txtScore; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI(){
        txtScore.text = string.Format("{0,3:D3}",score); //Formateo a tres digitos 
        txtLives.text = lives.ToString();
    }

    public static void UpdateScore(int points){
        score += points;
    }

    public static void UpdateLives(){
        lives--;
    }
}
