using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTillReadMe : MonoBehaviour
{
    public bool hasReadMe;
    public GameObject blocker1;
    public GameObject blocker2;
    public bool PauseScreen;
    public void Start()
    {
        hasReadMe = false;
        PauseScreen = false;
    }

    public void Update()
    {
        if (hasReadMe == false)
        {
            blocker1.SetActive(true);
            blocker2.SetActive(true);
        }

        if (hasReadMe == true && gameObject.GetComponent<ClickIcon>().TabOpen == false)
        {
            Destroy(blocker1);
            Destroy(blocker2);
            PauseScreen = true;
        }
    }

    public void ClickedOn()
    {
        hasReadMe = true;
    }

}
