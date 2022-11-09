using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolderEmptyOverride : MonoBehaviour
{

    public bool empty;
    public GameObject parentFolderIcon;


    private void Update()
    {
        if (empty == true)
        {
            Debug.Log(this + "is empty");
        }
    }


    public string searchTag;
    public List<GameObject> children = new List<GameObject>();
    public int emptyAmount;

     void Start()
    {
        searchTag = "Pattern Spawns";
    }

    public void CheckTheFolder()
    {
        /*if (searchTag != null)
        {
            FindObjectWithTag(searchTag);
        }*/

        FindObjectWithTag(searchTag);
    }

    public void FindObjectWithTag(string _tag)
    {
        children.Clear();
        Transform parent = this.transform;
        
        GetChildObject(parent, _tag);
        
    }

    public void GetChildObject(Transform parent, string _tag)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            //Debug.Log("this is happening");
            //Debug.Log(parent.childCount);
            Transform child = parent.GetChild(i); //only getting top 2 
            //Debug.Log(child);
            if (child.tag == _tag)
            {
                //Debug.Log("child is empty");
                emptyAmount += 1;
                children.Add(child.gameObject);
            }
        }

        if (emptyAmount == 4)
        {
            empty = true;
            Destroy(this.gameObject);
            Destroy(parentFolderIcon);
            Debug.Log(parent + "is empty. Destroying it.");
        }

        else if (emptyAmount < 4)
        {
            empty = false;
        }
    }

   
}
