using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClueSpawner : MonoBehaviour
{
    [Header("Pattern Spawning")]
    public List<GameObject> patternSpawnPoints;
    public GameObject spawnLocation;
    public int spawnPointNumber;

    [Header("The Different Hints")]
    public GameObject oneCorrectNumberAndPlace;
    public GameObject oneCorrectNumberWrongPlace; //spawn TWO of these, different values
    public GameObject twoCorrectNumberWrongPlace;
    public GameObject wrongNumbers;
    public int hintType;
    int amountOfHints = 5;

    [Header("Pattern Organization")]
    public GameObject[] correctInputs;
    public List<GameObject> otherInputs;
    public CorrectPatternManager correctPatternManagerScript;
    GameObject input1;
    GameObject input2;
    GameObject input3;

    int correct1;
    int correct2;
    int correct3;

    GameObject[] folderTabs;
    

    void Start()
    {
        correctPatternManagerScript = GameObject.Find("Main Camera").GetComponent<CorrectPatternManager>();
        otherInputs = new List<GameObject> { correctPatternManagerScript.Patterns[0], correctPatternManagerScript.Patterns[1],
            correctPatternManagerScript.Patterns[2], correctPatternManagerScript.Patterns[3], correctPatternManagerScript.Patterns[4],
            correctPatternManagerScript.Patterns[5], correctPatternManagerScript.Patterns[6], correctPatternManagerScript.Patterns[7] };
        patternSpawnPoints = new List<GameObject>();
        patternSpawnPoints.AddRange(GameObject.FindGameObjectsWithTag("Pattern Spawns"));

        correct1 = Random.Range(0, 3);

        do correct2 = Random.Range(0, 3);
        while (correct2 == correct1);

        do correct3 = Random.Range(0, 3);
        while ((correct3 == correct2) || (correct3 == correct1));

        folderTabs = GameObject.FindGameObjectsWithTag("FolderTab");

        StartCoroutine(CheckForCorrectInput());

    }

    public IEnumerator CheckForCorrectInput()
    {
        //need to wait to check for input, or else it will return as null since they run at the same time
        yield return new WaitForSeconds(2);
        input1 = correctPatternManagerScript.input1Object;
        input2 = correctPatternManagerScript.input2Object;
        input3 = correctPatternManagerScript.input3Object;
        correctInputs = new[] { input1, input2, input3 };

        otherInputs.Remove(input1);
        otherInputs.Remove(input2);
        otherInputs.Remove(input3);

        for (hintType = 0; hintType < amountOfHints; hintType++)
        {
            HintTypeSpawner();
        }
        Debug.Log("Done spawning hints.");

        
        foreach (GameObject folder in folderTabs)
        {
            folder.GetComponent<FolderEmptyOverride>().CheckTheFolder();
            Debug.Log("checking the folder");
        }
    }

    public void HintTypeSpawner()
    {

        switch (hintType)
        {
            case 0: //one correct tile, well placed

                #region case0 
                //spawn the clue itself, choose location, write location as occupied
                spawnPointNumber = Random.Range(0, patternSpawnPoints.Count);
                spawnLocation = patternSpawnPoints[spawnPointNumber];
                GameObject clue0 = Instantiate(oneCorrectNumberAndPlace) as GameObject;
                clue0.transform.SetParent(spawnLocation.gameObject.transform);
                clue0.transform.position = spawnLocation.transform.position;
                clue0.transform.localScale = new Vector3(1, 1, 1);
                spawnLocation.gameObject.tag = "OccupiedSpawnSpot";
                patternSpawnPoints.Remove(spawnLocation);

                //correct input and placement
                Image correctplacement0 = clue0.transform.GetChild(correct1).GetComponent<Image>(); //first get the correct placement of the tile
                Image correctimage0 = correctInputs[correct1].gameObject.GetComponent<Image>(); //get the image of the randomly selected correct tile
                correctplacement0.GetComponent<Image>().sprite = correctimage0.GetComponent<Image>().sprite; //sets randomly selected one to it's correct placement

                //pick other two random, incorrect tiles

                //first random tile
                int randomtile0;
                do randomtile0 = Random.Range(0, 3); //make sure it isn't same placement as correctimage0
                while (randomtile0 == correct1);

                Image randomplacement0 = clue0.transform.GetChild(randomtile0).GetComponent<Image>(); //get the placement
                int randomimage0 = Random.Range(0, otherInputs.Count);  //makes sure to pick one that is not correct
                Image random0 = otherInputs[randomimage0].gameObject.GetComponent<Image>(); //get the random image
                randomplacement0.GetComponent<Image>().sprite = random0.GetComponent<Image>().sprite; //combine placement and image

                //second random tile
                int randomtile1;
                do randomtile1 = Random.Range(0, 3);
                while ((randomtile1 == randomtile0) || (randomtile1 == correct1)); //make sure it isn't the same placement as random0 and correctimage0 

                Image randomplacement1 = clue0.transform.GetChild(randomtile1).GetComponent<Image>();
                int randomimage1;
                do randomimage1 = Random.Range(0, otherInputs.Count);
                while (randomimage1 == randomimage0); //make sure the two random ones are different 
                Image random1 = otherInputs[randomimage1].gameObject.GetComponent<Image>();
                randomplacement1.GetComponent<Image>().sprite = random1.GetComponent<Image>().sprite;
                #endregion case0

                break;
            case 1: //one correct tile, wrongly placed

                #region case1
                //spawn the clue, choose location, write location as occupied
                spawnPointNumber = Random.Range(0, patternSpawnPoints.Count);
                spawnLocation = patternSpawnPoints[spawnPointNumber];
                GameObject clue1 = Instantiate(oneCorrectNumberWrongPlace) as GameObject;
                clue1.transform.SetParent(spawnLocation.gameObject.transform);
                clue1.transform.position = spawnLocation.transform.position;
                clue1.transform.localScale = new Vector3(1, 1, 1);
                spawnLocation.gameObject.tag = "OccupiedSpawnSpot";
                patternSpawnPoints.Remove(spawnLocation);

                //pick correct number, put in wrong placement
                Image correctimage1 = correctInputs[correct2].gameObject.GetComponent<Image>();
                int wrongplace1;
                do wrongplace1 = Random.Range(0, 3);
                while (wrongplace1 == correct2);
                Image wrongplacement1 = clue1.transform.GetChild(wrongplace1).GetComponent<Image>();
                wrongplacement1.GetComponent<Image>().sprite = correctimage1.GetComponent<Image>().sprite;

                //first random
                int randomtile2;
                do randomtile2 = Random.Range(0, 3);
                while (randomtile2 == wrongplace1);

                Image randomplacement2 = clue1.transform.GetChild(randomtile2).GetComponent<Image>();
                int randomimage2 = Random.Range(0, otherInputs.Count);
                Image random2 = otherInputs[randomimage2].gameObject.GetComponent<Image>();
                randomplacement2.GetComponent<Image>().sprite = random2.GetComponent<Image>().sprite;

                //second random
                int randomtile3;
                do randomtile3 = Random.Range(0, 3);
                while ((randomtile3 == randomtile2) || (randomtile3 == wrongplace1));

                Image randomplacement3 = clue1.transform.GetChild(randomtile3).GetComponent<Image>();
                int randomimage3;
                do randomimage3 = Random.Range(0, otherInputs.Count);
                while (randomimage3 == randomimage2);
                Image random3 = otherInputs[randomimage3].gameObject.GetComponent<Image>();
                randomplacement3.GetComponent<Image>().sprite = random3.GetComponent<Image>().sprite;
                #endregion case1

                break;
            case 2: //one correct tile, wrongly placed

                #region case2
                //spawn the clue, choose location, write location as occupied
                spawnPointNumber = Random.Range(0, patternSpawnPoints.Count);
                spawnLocation = patternSpawnPoints[spawnPointNumber];
                GameObject clue2 = Instantiate(oneCorrectNumberWrongPlace) as GameObject;
                clue2.transform.SetParent(spawnLocation.gameObject.transform);
                clue2.transform.position = spawnLocation.transform.position;
                clue2.transform.localScale = new Vector3(1, 1, 1);
                spawnLocation.gameObject.tag = "OccupiedSpawnSpot";
                patternSpawnPoints.Remove(spawnLocation);

                //pick correct number, put in wrong placement
                Image correctimage2 = correctInputs[correct3].gameObject.GetComponent<Image>();
                int wrongplace2;
                do wrongplace2 = Random.Range(0, 3);
                while (wrongplace2 == correct3);
                Image wrongplacement2 = clue2.transform.GetChild(wrongplace2).GetComponent<Image>();
                wrongplacement2.GetComponent<Image>().sprite = correctimage2.GetComponent<Image>().sprite;

                //first random
                int randomtile4;
                do randomtile4 = Random.Range(0, 3);
                while (randomtile4 == wrongplace2);

                Image randomplacement4 = clue2.transform.GetChild(randomtile4).GetComponent<Image>();
                int randomimage4 = Random.Range(0, otherInputs.Count);
                Image random4 = otherInputs[randomimage4].gameObject.GetComponent<Image>();
                randomplacement4.GetComponent<Image>().sprite = random4.GetComponent<Image>().sprite;

                //second random
                int randomtile5;
                do randomtile5 = Random.Range(0, 3);
                while ((randomtile5 == randomtile4) || (randomtile5 == wrongplace2));

                Image randomplacement5 = clue2.transform.GetChild(randomtile5).GetComponent<Image>();
                int randomimage5;
                do randomimage5 = Random.Range(0, otherInputs.Count);
                while (randomimage5 == randomimage4);
                Image random5 = otherInputs[randomimage5].gameObject.GetComponent<Image>();
                randomplacement5.GetComponent<Image>().sprite = random5.GetComponent<Image>().sprite;
                #endregion case2

                break;
            case 3: //two correct tiles, wrongly placed

                #region case3
                //spawn the clue, choose location, write location as occupied
                spawnPointNumber = Random.Range(0, patternSpawnPoints.Count);
                spawnLocation = patternSpawnPoints[spawnPointNumber];
                GameObject clue3 = Instantiate(twoCorrectNumberWrongPlace) as GameObject;
                clue3.transform.SetParent(spawnLocation.gameObject.transform);
                clue3.transform.position = spawnLocation.transform.position;
                clue3.transform.localScale = new Vector3(1, 1, 1);
                spawnLocation.gameObject.tag = "OccupiedSpawnSpot";
                patternSpawnPoints.Remove(spawnLocation);

                //pick first correct, shuffle placement
                int correct4 = Random.Range(0, 3); //will be a dupe (same true tile) of one of the first three
                Image correctimage3 = correctInputs[correct4].gameObject.GetComponent<Image>();
                int wrongplace3;
                do wrongplace3 = Random.Range(0, 3);
                while (wrongplace3 == correct4);
                Image wrongplacement3 = clue3.transform.GetChild(wrongplace3).GetComponent<Image>();
                wrongplacement3.GetComponent<Image>().sprite = correctimage3.GetComponent<Image>().sprite;

                //pick second correct, shuffle placement
                int correct5;
                do correct5 = Random.Range(0, 3);
                while (correct5 == correct4);
                Image correctimage4 = correctInputs[correct5].gameObject.GetComponent<Image>();
                int wrongplace4;
                do wrongplace4 = Random.Range(0, 3);
                while ((wrongplace4 == wrongplace3) || (wrongplace4 == correct5));
                Image wrongplacement4 = clue3.transform.GetChild(wrongplace4).GetComponent<Image>();
                wrongplacement4.GetComponent<Image>().sprite = correctimage4.GetComponent<Image>().sprite;

                //pick random
                int randomtile6;
                do randomtile6 = Random.Range(0, 3);
                while ((randomtile6 == wrongplace3) || (randomtile6 == wrongplace4));

                Image randomplacement6 = clue3.transform.GetChild(randomtile6).GetComponent<Image>();
                int randomimage6 = Random.Range(0, otherInputs.Count);
                Image random6 = otherInputs[randomimage6].gameObject.GetComponent<Image>();
                randomplacement6.GetComponent<Image>().sprite = random6.GetComponent<Image>().sprite;
                #endregion case3

                break;
            case 4: //all tiles are wrong

                #region case4
                spawnPointNumber = Random.Range(0, patternSpawnPoints.Count);
                spawnLocation = patternSpawnPoints[spawnPointNumber];
                GameObject clue4 = Instantiate(wrongNumbers) as GameObject;
                clue4.transform.SetParent(spawnLocation.gameObject.transform);
                clue4.transform.position = spawnLocation.transform.position;
                clue4.transform.localScale = new Vector3(1, 1, 1);
                spawnLocation.gameObject.tag = "OccupiedSpawnSpot";
                patternSpawnPoints.Remove(spawnLocation);

                //all random, incorrect tiles

                //random 1
                int randomtile7 = Random.Range(0, 3);

                Image randomplacement7 = clue4.transform.GetChild(randomtile7).GetComponent<Image>();
                int randomimage7 = Random.Range(0, otherInputs.Count);
                Image random7 = otherInputs[randomimage7].gameObject.GetComponent<Image>();
                randomplacement7.GetComponent<Image>().sprite = random7.GetComponent<Image>().sprite;

                //random 2
                int randomtile8;
                do randomtile8 = Random.Range(0, 3);
                while (randomtile8 == randomtile7);

                Image randomplacement8 = clue4.transform.GetChild(randomtile8).GetComponent<Image>();
                int randomimage8;
                do randomimage8 = Random.Range(0, otherInputs.Count);
                while (randomimage8 == randomimage7);
                Image random8 = otherInputs[randomimage8].gameObject.GetComponent<Image>();
                randomplacement8.GetComponent<Image>().sprite = random8.GetComponent<Image>().sprite;

                //random 3
                int randomtile9;
                do randomtile9 = Random.Range(0, 3);
                while ((randomtile9 == randomtile7) || (randomtile9 == randomtile8));

                Image randomplacement9 = clue4.transform.GetChild(randomtile9).GetComponent<Image>();
                int randomimage9;
                do randomimage9 = Random.Range(0, otherInputs.Count);
                while ((randomimage9 == randomimage7) || (randomimage9 == randomimage8));
                Image random9 = otherInputs[randomimage9].gameObject.GetComponent<Image>();
                randomplacement9.GetComponent<Image>().sprite = random9.GetComponent<Image>().sprite;
                #endregion case4

                break;
        }
    }
}
