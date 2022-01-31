using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Audio;

public class LeaderboardController : MonoBehaviour
{
    Score scores;
    int score;

    public AudioMixer musicMixer;
    public AudioMixer soundMixer;

    public Text leaderboard;
    public InputField input;
    public Button enterName;

    public Slider musicSlider;
    public Slider soundSlider;

    GameController gameController;

    float timeSinceCheck;

    void AddScore()
    {
        enterName.enabled = false;
        timeSinceCheck = 0;
        StartCoroutine(ScoreCall());
    }

    IEnumerator ScoreCall()
    {
        var name = input.text;
        if (name.Length > 0)
        {
            yield return StartCoroutine(getRequest("https://www.dreamlo.com/lb/F1ATo-ZqvEaDj13eDDCqbAy255hqSxLkmPoj1DeI1M5g/add/"+name+"/"+ score.ToString()));

        }
        StartCoroutine(GetTopScores());
    }


    IEnumerator GetTopScores()
    {
        yield return StartCoroutine(getRequest("https://www.dreamlo.com/lb/61f09cb3778d3c8d80ab0d6c/json"));

        leaderboard.text = "";

        var entries = scores.dreamlo.leaderboard.entry;
        for (int ii = 0; ii < entries.Length; ii++)
        {
            if (ii >=10)
            {
                break;
            }

            var e = entries[ii];
            string line = (ii+1).ToString() + ": "+  e.name + " - " + e.score;

            leaderboard.text += line +"\n\n";
        }
    }

    void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        StartCoroutine(GetTopScores());
        enterName.onClick.AddListener(AddScore);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        timeSinceCheck += Time.unscaledDeltaTime;
        if (timeSinceCheck > 10)
        {
            enterName.enabled = true;
        }
        score = gameController.score;
        musicMixer.SetFloat("MusicVolume", musicSlider.value);
        soundMixer.SetFloat("SoundVolume", soundSlider.value);
    }

    IEnumerator getRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    var response = (System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data));
                    if (response != "OK")
                    {
                        Debug.Log(System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data));
                        scores = JsonUtility.FromJson<Score>(System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data));
                    }
                    break;
            }
        }
    }
}


[System.Serializable]
public class Score
{
    public Dreamlo dreamlo;
}

[System.Serializable]
public class Dreamlo
{
    public Leaderboard leaderboard;
}

[System.Serializable]
public class Leaderboard
{
    public Entry[] entry;
}

[System.Serializable]
public class Entry
{
    public string name;
    public string score;
    public string seconds;
    public string text;
    public string date;
}
