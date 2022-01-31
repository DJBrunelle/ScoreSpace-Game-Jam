using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    AudioSource[] audioSources;
    AudioSource gameTheme;
    AudioSource creditsTheme;

    public GameObject gameUI;
    public GameObject creditsUI;
    public Button start;

    public StationController station;
    public PlayerController player;
    public ArenaController arena;

    public Text scoreText;
    public Text waveText;
    public Text gameOverText;

    public int scaleInterval;

    int score = 0;
    int waveScore = 0;
    int wave = 0;

    public bool gameOver = false;

    void Awake()
    {
        audioSources = GetComponents<AudioSource>();

        gameTheme = audioSources[0];
        creditsTheme = audioSources[1];
    }


    // Start is called before the first frame update
    void Start()
    {
        gameTheme.Play();
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

    void Restart()
    {
        Time.timeScale = 1;
        player.Reset();
        arena.Reset();
        station.Reset();
        creditsUI.SetActive(false);
        gameUI.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        creditsTheme.Stop();
        gameTheme.Play();
        gameOver = false;
        wave = 0;
        score = 0;
        waveScore = 0;
    }

    void CheckLoseCondition()
    {
        if ((player.isDead() || station.isDead()) && !gameOver)
        {
            gameOver = true;
            Time.timeScale = 0.01f;
            gameOverText.gameObject.SetActive(true);
            gameTheme.Stop();
            creditsTheme.Play();
            gameUI.SetActive(false);
            creditsUI.SetActive(true);
            start.onClick.AddListener(Restart);
        }
    }

    public void AddToScore(int points)
    {
        score += points;
        waveScore += points;
    }
}
