using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public bool paused = false;
    public void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (!paused) 
            { 
                this.gameObject.GetComponent<ClickIcon>().OpenTab();
                Pause();
            }
        } 
    }

    public void Pause()
    {
        Time.timeScale = 0;
        paused = true;
    }
    public void Resume()
    {
        Time.timeScale = 1;
        if (SceneManager.GetActiveScene().name == "ThreePatternGame") 
        {
            GameObject.Find("TimerText").GetComponent<Timer>().UnpauseTimer();
        }
        paused = false;
    }
}
