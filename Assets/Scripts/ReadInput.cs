using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadInput : MonoBehaviour
{
    private string input;
    static string playerName;

    static bool nameBoxPopUp = true;
    public Image introNameBox;

    public Text HelloPlayer;

    // Start is called before the first frame update
    void Start()
    {
        if (nameBoxPopUp == true)
        {
            introNameBox.gameObject.SetActive(true); 
        }
    }

    void Update()
    {
        if (nameBoxPopUp == true)
        {
            if (Input.GetKey(KeyCode.Return))
            {
                CheckPlayerName();
            }
        }
    }
    public void ReadStringInput(string s)
    {
        input = s;
    }

    public void CheckPlayerName()
    {
        if (input != null)
        {
            playerName = input;
            Debug.Log(playerName);
            introNameBox.gameObject.SetActive(false);
            nameBoxPopUp = false;
        }
    }

    public void TextPlayerName()
    {
        HelloPlayer.text = ("Hello " + playerName);
    }
}
