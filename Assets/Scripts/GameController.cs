using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public StationController station;
    public PlayerController player;
    public ArenaController arena;

    public Text scoreText;

    int score = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: "+score.ToString();
    }

    void Scale()
    {

    }

    void CheckLoseCondition()
    {

    }

    public void AddToScore(int points)
    {
        score += points;
    }
}
