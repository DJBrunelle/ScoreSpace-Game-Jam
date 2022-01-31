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
    public Text waveText;

    public int scaleInterval;

    int score = 0;
    int waveScore = 0;
    int wave = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: "+score.ToString();
        waveText.text = "Wave: "+wave.ToString();

        Scale();
        CheckLoseCondition();
    }

    void Scale()
    {
        if (waveScore / arena.spawnRate > scaleInterval)
        {
            wave += 1;
            waveScore = 0;
            arena.spawnRate += 0.25f;
        }
    }

    void CheckLoseCondition()
    {

    }

    public void AddToScore(int points)
    {
        score += points;
        waveScore += points;
    }
}
