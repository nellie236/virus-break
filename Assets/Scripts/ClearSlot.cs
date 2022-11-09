using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearSlot : MonoBehaviour
{
    public Sprite iconBox;
    
    public void EmptySlot()
    {
        GameObject slot = this.gameObject;
        slot.GetComponent<Image>().sprite = iconBox;
        
    }
}
