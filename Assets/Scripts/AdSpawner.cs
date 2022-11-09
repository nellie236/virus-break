using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdSpawner : MonoBehaviour
{
    public float minWait;
    public float maxWait;
    public float timer;
    public int adToSpawn;
    public int spawnPoint;
    public GameObject spawnLocation;
    public GameObject[] ads;
    public GameObject[] spawnPoints;
    GameObject readMeCheck;

    //private bool isSpawning;
    // Start is called before the first frame update
    void Start()
    {
        //isSpawning = false;

        readMeCheck = GameObject.Find("READMEIcon");
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartSpawningAds()
    {
        if ((readMeCheck.GetComponent<BlockTillReadMe>().hasReadMe == true))
        {
            //Debug.Log("Gonna Spawning Ads");
            StartCoroutine(WaitToSpawn());
        }
    }

    IEnumerator WaitToSpawn()
    {
        //Debug.Log("Spawning Ad");
        timer = Random.Range(minWait, maxWait);
        adToSpawn = Random.Range(0, ads.Length);
        spawnPoint = Random.Range(0, spawnPoints.Length);
        spawnLocation = spawnPoints[spawnPoint];
        yield return new WaitForSeconds(timer);
        GameObject myAd = Instantiate(ads[adToSpawn], spawnLocation.transform.position, Quaternion.identity) as GameObject;
        myAd.transform.SetParent(this.gameObject.transform);
        myAd.transform.localScale = new Vector3(1, 1, 1);
        //GameObject.Find("ShakeEverything").GetComponent<ShakeBehavior>().TriggerShake();
        GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
        StartCoroutine(WaitToSpawnMore());
    }

    IEnumerator WaitToSpawnMore()
    {
        timer = Random.Range(minWait, maxWait);
        yield return new WaitForSeconds(timer);
        StartCoroutine(WaitToSpawn());
    }
}
