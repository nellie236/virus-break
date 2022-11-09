using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System.Linq;



public class HintSpawner : MonoBehaviour
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

    void Start()
    {
        correctPatternManagerScript = GameObject.Find("Main Camera").GetComponent<CorrectPatternManager>();
        otherInputs = new List<GameObject> { correctPatternManagerScript.Patterns[0], correctPatternManagerScript.Patterns[1],
            correctPatternManagerScript.Patterns[2], correctPatternManagerScript.Patterns[3], correctPatternManagerScript.Patterns[4],
            correctPatternManagerScript.Patterns[5], correctPatternManagerScript.Patterns[6], correctPatternManagerScript.Patterns[7] };
        patternSpawnPoints = new List<GameObject>();
        patternSpawnPoints.AddRange(GameObject.FindGameObjectsWithTag("Pattern Spawns"));

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

        GameObject[] folderTabs;
        folderTabs = GameObject.FindGameObjectsWithTag("FolderTab");
        foreach (GameObject folder in folderTabs)
        {
            folder.GetComponent<FolderEmptyOverride>().CheckTheFolder();
            Debug.Log("checking the folder");
        }
    }
    
    public void HintTypeSpawner()
    {
        int otherInputsCount = otherInputs.Count;
        int correctPlacement = Random.Range(0, 3);
        //hintType = 3;
        switch (hintType)
        {
            case 0: //oneCorrectNumberAndPlace

                #region case0
                //spawn the clue
                spawnPointNumber = Random.Range(0, patternSpawnPoints.Count);
                spawnLocation = patternSpawnPoints[spawnPointNumber];
                GameObject oneCorrectNumberAndPlaceClue = Instantiate(oneCorrectNumberAndPlace) as GameObject;
                oneCorrectNumberAndPlaceClue.transform.SetParent(spawnLocation.gameObject.transform);
                oneCorrectNumberAndPlaceClue.transform.position = spawnLocation.transform.position;
                oneCorrectNumberAndPlaceClue.transform.localScale = new Vector3(1, 1, 1);
                spawnLocation.gameObject.tag = "OccupiedSpawnSpot";
                patternSpawnPoints.Remove(spawnLocation);

                //pick correct number and placement
                int correctImagePlacement = Random.Range(0, 3);
                Image correctNumberAndPlace = oneCorrectNumberAndPlaceClue.transform.GetChild(correctImagePlacement).GetComponent<Image>();
                Image correctOneToSetTo = correctInputs[correctImagePlacement].gameObject.GetComponent<Image>();
                correctNumberAndPlace.GetComponent<Image>().sprite = correctOneToSetTo.GetComponent<Image>().sprite;

                //pick other two random, incorrect patterns

                //first random
                int random1; 
                do random1 = Random.Range(0, 3);
                while (random1 == correctImagePlacement);

                Image Random1 = oneCorrectNumberAndPlaceClue.transform.GetChild(random1).GetComponent<Image>();
                int randomImageNumber1 = Random.Range(0, otherInputsCount);
                Image randomImage1 = otherInputs[randomImageNumber1].gameObject.GetComponent<Image>();
                Random1.GetComponent<Image>().sprite = randomImage1.GetComponent<Image>().sprite;

                //second random
                int random2;
                do random2 = Random.Range(0, 3);
                while ((random2 == random1) || (random2 == correctImagePlacement));

                Image Random2 = oneCorrectNumberAndPlaceClue.transform.GetChild(random2).GetComponent<Image>();
                int randomImageNumber2 = Random.Range(0, otherInputsCount); //could put in do while to make sure they aren't the same image
                do randomImageNumber2 = Random.Range(0, otherInputsCount);
                while (randomImageNumber2 == randomImageNumber1);
                Image randomImage2 = otherInputs[randomImageNumber2].gameObject.GetComponent<Image>();
                Random2.GetComponent<Image>().sprite = randomImage2.GetComponent<Image>().sprite;

                //make sure that the folder will fit to this asset
                //++++oneCorrectNumberAndPlaceClue.GetComponentInParent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                //oneCorrectNumberAndPlaceClue.GetComponentInParent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;

                #endregion case0

                break;
            case 1: //correctNumberWrongPlace - two of these are spawned

                #region case1
                //spawn clue
                spawnPointNumber = Random.Range(0, patternSpawnPoints.Count);
                spawnLocation = patternSpawnPoints[spawnPointNumber];
                GameObject correctNumberWrongPlaceClue = Instantiate(oneCorrectNumberWrongPlace) as GameObject;
                correctNumberWrongPlaceClue.transform.SetParent(spawnLocation.gameObject.transform);
                correctNumberWrongPlaceClue.transform.position = spawnLocation.transform.position;
                correctNumberWrongPlaceClue.transform.localScale = new Vector3(1, 1, 1);
                spawnLocation.gameObject.tag = "OccupiedSpawnSpot";
                patternSpawnPoints.Remove(spawnLocation);

                //pick correct number, put in wrong placement
                //int correctPlacement = Random.Range(0, 3); //pick a correct placement first, then jumble it up 
                Image correctImage = correctInputs[correctPlacement].gameObject.GetComponent<Image>();
                int wrongPlacement;
                do wrongPlacement = Random.Range(0, 3);
                while (wrongPlacement == correctPlacement);
                Image wrongPlace = correctNumberWrongPlaceClue.transform.GetChild(wrongPlacement).GetComponent<Image>();
                wrongPlace.GetComponent<Image>().sprite = correctImage.GetComponent<Image>().sprite;

                //first random
                int random3;
                do random3 = Random.Range(0, 3);
                while (random3 == wrongPlacement);

                Image Random3 = correctNumberWrongPlaceClue.transform.GetChild(random3).GetComponent<Image>();
                int randomImageNumber3 = Random.Range(0, otherInputsCount);
                Image randomImage3 = otherInputs[randomImageNumber3].gameObject.GetComponent<Image>();
                Random3.GetComponent<Image>().sprite = randomImage3.GetComponent<Image>().sprite;

                //second random
                int random4;
                do random4 = Random.Range(0, 3);
                while ((random4 == wrongPlacement) || (random4 == random3));

                Image Random4 = correctNumberWrongPlaceClue.transform.GetChild(random4).GetComponent<Image>();
                int randomImageNumber4 = Random.Range(0, otherInputsCount);
                do randomImageNumber4 = Random.Range(0, otherInputsCount);
                while (randomImageNumber4 == randomImageNumber3);
                Image randomImage4 = otherInputs[randomImageNumber4].gameObject.GetComponent<Image>();
                Random4.GetComponent<Image>().sprite = randomImage4.GetComponent<Image>().sprite;
                #endregion case1

                break;
            case 2: //TwoCorrectNumberWrongPlace

                #region case2
                //spawn clue
                spawnPointNumber = Random.Range(0, patternSpawnPoints.Count);
                spawnLocation = patternSpawnPoints[spawnPointNumber];
                GameObject twoCorrectNumberWrongPlaceClue = Instantiate(twoCorrectNumberWrongPlace) as GameObject;
                twoCorrectNumberWrongPlaceClue.transform.SetParent(spawnLocation.gameObject.transform);
                twoCorrectNumberWrongPlaceClue.transform.position = spawnLocation.transform.position;
                twoCorrectNumberWrongPlaceClue.transform.localScale = new Vector3(1, 1, 1);
                spawnLocation.gameObject.tag = "OccupiedSpawnSpot";
                patternSpawnPoints.Remove(spawnLocation);

                //pick correct number, put in wrong placement
                int correctPlacement2 = Random.Range(0, 3); //pick a correct placement first, then jumble it up 
                do correctPlacement2 = Random.Range(0, 3);
                while (correctPlacement2 == correctPlacement);
                Image correctImage2 = correctInputs[correctPlacement2].gameObject.GetComponent<Image>();
                int wrongPlacement2;
                do wrongPlacement2 = Random.Range(0, 3);
                while (wrongPlacement2 == correctPlacement2);
                Image wrongPlace2 = twoCorrectNumberWrongPlaceClue.transform.GetChild(wrongPlacement2).GetComponent<Image>();
                wrongPlace2.GetComponent<Image>().sprite = correctImage2.GetComponent<Image>().sprite;

                //pick second correct, put in wrong place
                int correctPlacement3 = Random.Range(0, 3); //pick a correct placement first, then jumble it up 
                do correctPlacement3 = Random.Range(0, 3);
                while (correctPlacement3 == correctPlacement2);
                Image correctImage3 = correctInputs[correctPlacement3].gameObject.GetComponent<Image>();
                int wrongPlacement3;
                do wrongPlacement3 = Random.Range(0, 3);
                while ((wrongPlacement3 == correctPlacement3) || (wrongPlacement3 == wrongPlacement2));
                Image wrongPlace3 = twoCorrectNumberWrongPlaceClue.transform.GetChild(wrongPlacement3).GetComponent<Image>();
                wrongPlace3.GetComponent<Image>().sprite = correctImage3.GetComponent<Image>().sprite;

                //random image
                int random5;
                do random5 = Random.Range(0, 3);
                while ((random5 == wrongPlacement2) || (random5 == wrongPlacement3));

                Image Random5 = twoCorrectNumberWrongPlaceClue.transform.GetChild(random5).GetComponent<Image>();
                int randomImageNumber5 = Random.Range(0, otherInputsCount);
                Image randomImage5 = otherInputs[randomImageNumber5].gameObject.GetComponent<Image>();
                Random5.GetComponent<Image>().sprite = randomImage5.GetComponent<Image>().sprite;
                #endregion case2

                break;
            case 3: //WrongNumbers

                #region case3
                //spawn clue
                spawnPointNumber = Random.Range(0, patternSpawnPoints.Count);
                spawnLocation = patternSpawnPoints[spawnPointNumber];
                GameObject wrongNumbersClue = Instantiate(wrongNumbers) as GameObject;
                wrongNumbersClue.transform.SetParent(spawnLocation.gameObject.transform);
                wrongNumbersClue.transform.position = spawnLocation.transform.position;
                wrongNumbersClue.transform.localScale = new Vector3(1, 1, 1);
                spawnLocation.gameObject.tag = "OccupiedSpawnSpot";
                patternSpawnPoints.Remove(spawnLocation);

                //random1
                int random6;
                random6 = Random.Range(0, 3);
                
                Image Random6 = wrongNumbersClue.transform.GetChild(random6).GetComponent<Image>();
                int randomImageNumber6 = Random.Range(0, otherInputsCount);
                Image randomImage6 = otherInputs[randomImageNumber6].gameObject.GetComponent<Image>();
                Random6.GetComponent<Image>().sprite = randomImage6.GetComponent<Image>().sprite;

                //random2
                int random7;
                do random7 = Random.Range(0, 3);
                while (random7 == random6);

                Image Random7 = wrongNumbersClue.transform.GetChild(random7).GetComponent<Image>();
                int randomImageNumber7 = Random.Range(0, otherInputsCount);
                do randomImageNumber7 = Random.Range(0, otherInputsCount);
                while (randomImageNumber7 == randomImageNumber6);
                Image randomImage7 = otherInputs[randomImageNumber7].gameObject.GetComponent<Image>();
                Random7.GetComponent<Image>().sprite = randomImage7.GetComponent<Image>().sprite;

                //random3
                int random8;
                do random8 = Random.Range(0, 3);
                while ((random8 == random6) || (random8 == random7));

                Image Random8 = wrongNumbersClue.transform.GetChild(random8).GetComponent<Image>();
                int randomImageNumber8 = Random.Range(0, otherInputsCount);
                do randomImageNumber8 = Random.Range(0, otherInputsCount);
                while ((randomImageNumber8 == randomImageNumber7) || (randomImageNumber8 == randomImageNumber6));
                Image randomImage8 = otherInputs[randomImageNumber8].gameObject.GetComponent<Image>();
                Random8.GetComponent<Image>().sprite = randomImage8.GetComponent<Image>().sprite;
                #endregion case3

                break;
            case 4: //correctNumberWrongPlace #2

                #region case4
                //spawn clue
                spawnPointNumber = Random.Range(0, patternSpawnPoints.Count);
                spawnLocation = patternSpawnPoints[spawnPointNumber];
                GameObject correctNumberWrongPlaceClue2 = Instantiate(oneCorrectNumberWrongPlace) as GameObject;
                correctNumberWrongPlaceClue2.transform.SetParent(spawnLocation.gameObject.transform);
                correctNumberWrongPlaceClue2.transform.position = spawnLocation.transform.position;
                correctNumberWrongPlaceClue2.transform.localScale = new Vector3(1, 1, 1);
                spawnLocation.gameObject.tag = "OccupiedSpawnSpot";
                patternSpawnPoints.Remove(spawnLocation);

                //pick correct number, put in wrong placement
                int correctPlacement4 = Random.Range(0, 3); //pick a correct placement first, then jumble it up 
                do correctPlacement4 = Random.Range(0, 3);
                while (correctPlacement4 == correctPlacement);
                Image correctImage4 = correctInputs[correctPlacement4].gameObject.GetComponent<Image>();
                int wrongPlacement4;
                do wrongPlacement4 = Random.Range(0, 3);
                while (wrongPlacement4 == correctPlacement4);
                Image wrongPlace4 = correctNumberWrongPlaceClue2.transform.GetChild(wrongPlacement4).GetComponent<Image>();
                wrongPlace4.GetComponent<Image>().sprite = correctImage4.GetComponent<Image>().sprite;

                //first random
                int random9;
                do random9 = Random.Range(0, 3);
                while (random9 == wrongPlacement4);

                Image Random9 = correctNumberWrongPlaceClue2.transform.GetChild(random9).GetComponent<Image>();
                int randomImageNumber9 = Random.Range(0, otherInputsCount);
                Image randomImage9 = otherInputs[randomImageNumber9].gameObject.GetComponent<Image>();
                Random9.GetComponent<Image>().sprite = randomImage9.GetComponent<Image>().sprite;

                //second random
                int random10;
                do random10 = Random.Range(0, 3);
                while ((random10 == wrongPlacement4) || (random10 == random9));

                Image Random10 = correctNumberWrongPlaceClue2.transform.GetChild(random10).GetComponent<Image>();
                int randomImageNumber10 = Random.Range(0, otherInputsCount);
                do randomImageNumber10 = Random.Range(0, otherInputsCount);
                while (randomImageNumber10 == randomImageNumber9);
                Image randomImage10 = otherInputs[randomImageNumber10].gameObject.GetComponent<Image>();
                Random10.GetComponent<Image>().sprite = randomImage10.GetComponent<Image>().sprite;
                #endregion case4
                break;
            default:
                break;
        }
    }

    
}
