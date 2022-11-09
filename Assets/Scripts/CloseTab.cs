using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTab : MonoBehaviour
{

    public void CloseFolder()
    {
        this.transform.parent.gameObject.SetActive(false);
    }

    public void CloseAd()
    {
        Destroy(this.transform.parent.gameObject);
    }
}
