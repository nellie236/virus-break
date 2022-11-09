using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public float timeValue = 90;
    public bool StartTimer = false;
    GameObject gameOverScreen;
    public Text timeText;

    void Start()
    {
        gameOverScreen = GameObject.Find("GameOver");
        gameOverScreen.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (StartTimer == true)
        {
            if (timeValue > 0)
            {
                timeValue -= Time.deltaTime;
            }
            else
            {
                timeValue = 0;
                if (timeValue == 0)
                {

                    GameOver();
                }
            }

            DisplayTime(timeValue);
        }
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
        GameObject.Find("DecisiveBattleSound").GetComponent<AudioSource>().Stop();
        GameObject.Find("DecisiveBattleSound").GetComponent<AudioSource>().loop = false;
        GameObject.Find("TitleThemeSound").GetComponent<AudioSource>().Play();
        GameObject.Find("TitleThemeSound").GetComponent<AudioSource>().loop = true;
    }

    public void StartTheTimer()
    {
        if (StartTimer == false)
        {
            StartTimer = true;
            GameObject.Find("MysteriousDungeonSound").GetComponent<AudioSource>().Stop();
            GameObject.Find("MysteriousDungeonSound").GetComponent<AudioSource>().loop = false;
            GameObject.Find("DecisiveBattleSound").GetComponent<AudioSource>().Play();
            GameObject.Find("DecisiveBattleSound").GetComponent<AudioSource>().loop = true;
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        else if (timeToDisplay > 0)
        {
            timeToDisplay += 1;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void PauseTimer()
    {
        if (StartTimer == true)
        {
            StartTimer = false;
        }
    }

    public void UnpauseTimer()
    {
        if (GameObject.Find("READMEIcon").GetComponent<BlockTillReadMe>().PauseScreen == true)
        {
            StartTimer = true;
        }
    }

}
