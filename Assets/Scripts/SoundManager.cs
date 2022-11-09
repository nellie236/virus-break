using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "ThreePatternGame") 
        {
            GameObject.Find("MysteriousDungeonSound").GetComponent<AudioSource>().Play();
            GameObject.Find("MysteriousDungeonSound").GetComponent<AudioSource>().loop = true;
        }
        
        if (SceneManager.GetActiveScene().name == "IntroScene")
        {
            GameObject.Find("MysteriousDungeonSound").GetComponent<AudioSource>().Play();
            GameObject.Find("MysteriousDungeonSound").GetComponent<AudioSource>().loop = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            // If the left mouse button is pressed down...
            if (Input.GetMouseButtonDown(0) == true)
            {
                GameObject.Find("OnClickSound").GetComponent<AudioSource>().Play();

            }
            else
            {
                GameObject.Find("KeyboardSound").GetComponent<AudioSource>().Play();
            }
        }
    }
}
