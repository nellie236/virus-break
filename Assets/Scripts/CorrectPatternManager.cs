using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorrectPatternManager : MonoBehaviour
{
    public GameObject[] Patterns;
    public int Input1;
    public int Input2;
    public int Input3;
    public Sprite iconBox;

    public GameObject input1Object;
    public GameObject input2Object;
    public GameObject input3Object;

    public Pattern slot1;
    public Pattern slot2;
    public Pattern slot3;

    public Text TimeLeftText;

    GameObject winScreen;

    public GameObject green1;
    public GameObject green2;
    public GameObject green3;
    public GameObject red1;

    public int guessCount;
    public GameObject guess1;
    public GameObject guess2;
    public GameObject guess3;
    public GameObject guess4;

    int wrongGuessTimer;
    bool wrongGuess;


    public void Start()
    {
        //spawnPoints = GameObject.FindGameObjectsWithTag("Pattern Spawns");
        Time.timeScale = 1;
        winScreen = GameObject.Find("WinScreen");
        winScreen.SetActive(false);

        green1.SetActive(false);
        green2.SetActive(false);
        green3.SetActive(false);

        Input1 = Random.Range(0, Patterns.Length);
        Input2 = Random.Range(0, Patterns.Length);
        do Input2 = Random.Range(0, Patterns.Length);
        while (Input2 == Input1);
        Input3 = Random.Range(0, Patterns.Length);
        do Input3 = Random.Range(0, Patterns.Length);
        while ((Input3 == Input2) || (Input3 == Input1));

        input1Object = Patterns[Input1];
        input2Object = Patterns[Input2];
        input3Object = Patterns[Input3];

        guessCount = 0;
        guess1.SetActive(false);
        guess2.SetActive(false);
        guess3.SetActive(false);
        guess4.SetActive(false);

        red1.SetActive(false);
        wrongGuessTimer = 0;

        //spawnPointNumber = Random.Range(0, spawnPoints.Length);
        //spawnLocation = spawnPoints[spawnPointNumber];
        //Image input1 = Instantiate(Patterns[Input1].GetComponent<Image>(), spawnLocation.transform.position, Quaternion.identity) as Image;

        
        
    }

    private void Update()
    {
        if (wrongGuess == true)
        {
            red1.SetActive(true);
            wrongGuessTimer += 1;
               
                if (wrongGuessTimer >= 150)
                {
                    red1.SetActive(false);
                    wrongGuess = false;
                }
        }
    }


    public void ClearAllSlots()
    {
        GameObject.Find("WrongGuessSound").GetComponent<AudioSource>().Play();
        GameObject[] slots = GameObject.FindGameObjectsWithTag("Slot");

        foreach (GameObject slot in slots)
        {
            slot.GetComponent<Image>().sprite = iconBox;
        }

        guessCount += 1;

        if (guessCount == 1)
        {
            guess1.SetActive(true);
            GameObject.Find("TimerText").GetComponent<Timer>().timeValue -= 5;
        }
        else if (guessCount == 2)
        {
            guess2.SetActive(true);
            GameObject.Find("TimerText").GetComponent<Timer>().timeValue -= 10;
        }
        else if (guessCount == 3)
        {
            guess3.SetActive(true);
            GameObject.Find("TimerText").GetComponent<Timer>().timeValue -= 15;
        }
        else if (guessCount == 4)
        {
            guess4.SetActive(true);
            GameObject.Find("TimerText").GetComponent<Timer>().timeValue = 5;
        }
        else if (guessCount >= 5)
        {
            GameObject.Find("TimerText").GetComponent<Timer>().GameOver();
        }
    }

    public void CheckIfCorrect()
    {
        if (slot1.patternID == Input1 && slot2.patternID == Input2 && slot3.patternID == Input3)
        {
            guessCount += 1;
            Debug.Log("You win!");
            Time.timeScale = 0;
            GameObject.Find("SuccessSound").GetComponent<AudioSource>().Play();
            GameObject.Find("TimerText").GetComponent<Timer>().PauseTimer();
            winScreen.SetActive(true);
            TimeLeftText.text = GameObject.Find("TimerText").GetComponent<Timer>().timeText.text;
            GameObject.Find("DecisiveBattleSound").GetComponent<AudioSource>().Stop();
            GameObject.Find("DecisiveBattleSound").GetComponent<AudioSource>().loop = false;
            GameObject.Find("AndTheJourneyBeginsSound").GetComponent<AudioSource>().Play();
            GameObject.Find("AndTheJourneyBeginsSound").GetComponent<AudioSource>().loop = true;
            //need win example here, thank you (NAME) for defeating the virus! you completed the task in (TIME). The hacker surely won't return 
        }
        else
        {
            if (slot1.patternID == Input1)
            {
                //enable green square, keep visual there, make it so that slot is filled and can't be changed - change tag of slots so that they don't get cleared with X
                slot1.gameObject.tag = "Correct Slot";
                green1.SetActive(true);
            }

            if (slot2.patternID == Input2)
            {
                //
                slot2.gameObject.tag = "Correct Slot";
                green2.SetActive(true);
            }

            if (slot3.patternID == Input3)
            {
                slot3.gameObject.tag = "Correct Slot";
                green3.SetActive(true);
            }

            wrongGuessTimer = 0;
            wrongGuess = true;

            Debug.Log("Try again!");
            GameObject.Find("WrongGuessSound").GetComponent<AudioSource>().Play();
            ClearAllSlots();
        }

        
    }

    //track certain amount of guesses, 3-5? need visuals for this too
}
