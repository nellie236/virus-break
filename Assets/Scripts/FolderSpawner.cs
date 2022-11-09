using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FolderSpawner : MonoBehaviour
{
    public int amountOfFolders; //use this int to run instantiate X amount of times
    public int spawnPointNumber;
    public GameObject spawnLocation;
    public GameObject[] spawnPoints;
    public GameObject folderIconPrefab;
    public Image folderTabPrefab;

    public GameObject[] folderTabs;
    

    // Start is called before the first frame update
    void Awake()
    {
        amountOfFolders = Random.Range(2, 5);
        for (int i = 0; i < amountOfFolders; i++)
        {
            spawnPoints = GameObject.FindGameObjectsWithTag("Folder Spawns");
            spawnPointNumber = Random.Range(0, spawnPoints.Length);
            spawnLocation = spawnPoints[spawnPointNumber];
            spawnLocation.gameObject.tag = "OccupiedSpawnSpot";
            GameObject folder = Instantiate(folderIconPrefab, spawnLocation.transform.position, Quaternion.identity) as GameObject;
            folder.transform.SetParent(this.gameObject.transform);
            folder.transform.SetParent(this.gameObject.transform, false);
            folder.transform.localScale = new Vector3(1, 1, 1);
            Image folderTab = Instantiate(folderTabPrefab) as Image;
            folderTab.transform.SetParent(GameObject.Find("DragableTabs").transform, false); //used to be FolderTabGRP
            folderTab.transform.position = GameObject.Find("FolderManager").transform.position + new Vector3(i * -.2f, i * -.2f, 0);
            folder.GetComponent<ClickIcon>().TabToOpen = folderTab;
            folderTab.GetComponent<FolderEmptyOverride>().parentFolderIcon = folder;
        }

        //for amount of folders, each has a certain number of range of "clues" it can have, which will be patterns due to time
    }

    void Start()
    {
        folderTabs = GameObject.FindGameObjectsWithTag("FolderTab");
    }
}
