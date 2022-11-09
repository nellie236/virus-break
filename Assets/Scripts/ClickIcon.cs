using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickIcon : MonoBehaviour
{
    public Image TabToOpen;
    public bool TabOpen;

    private void Start()
    {
        TabOpen = false;
        TabToOpen.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (TabToOpen.gameObject.activeInHierarchy)
        {
            TabOpen = true;
        }
        else
        {
            TabOpen = false;
        }
    }

    public void OpenTab()
    {
        if (TabOpen == false)
        {
            TabToOpen.gameObject.SetActive(true);
            TabToOpen.transform.SetAsLastSibling();
            TabOpen = true;
        }
    }

    public void XOut()
    {
        if (TabOpen == true)
        {
            GameObject.Find("OnClickSound").GetComponent<AudioSource>().Stop();
            GameObject.Find("CloseOutSound").GetComponent<AudioSource>().Play();
            TabToOpen.gameObject.SetActive(false);
            TabOpen = false;
        }
        
    }

}
