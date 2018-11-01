using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {

    public GameObject endButtonObject;
    public GameObject endResultsObjct;
    public GameObject endGameObject;
    public GameObject britishWinPicture;
    public GameObject germanWinPicture;

    Button endButton;
    Text endResultText;

    Vector3 OOB;
    Vector3 endGameOriginal;

	void Start () {

        endButtonObject = GameObject.Find("EndQuit");
        endResultsObjct = GameObject.Find("EndResult");
        endGameObject = GameObject.Find("EndGame");
        britishWinPicture = GameObject.Find("BritishVictory");
        germanWinPicture = GameObject.Find("GermanVictory");

        endButton = endButtonObject.GetComponent<Button>();
        endResultText = endResultsObjct.GetComponent<Text>();

        endGameOriginal = endGameObject.transform.position;
        OOB = new Vector3(9999.0f, 9999.0f, 9999.0f);

        endButtonObject.transform.position = OOB;

        britishWinPicture.SetActive(false);
        germanWinPicture.SetActive(false);

        SetButtons();

    }
	
	void Update () {
		
	}

    public void EndGameResults()
    {
        ScoreScreen scores = GameObject.Find("ScoreScreen").GetComponent<ScoreScreen>();

        int germanFighters = scores.GetGermanFighters();
        int germanBombers = scores.GetGermanBombers();
        int britishFighters = scores.GetBritishFighters();

        string victory = "";

        if(((germanBombers*4)+germanFighters) > (britishFighters))
        {
            victory = "The British have Won!";
            britishWinPicture.SetActive(true);
        }
        else
        {
            victory = "The Germans have Won!";
            germanWinPicture.SetActive(true);
        }

        endButtonObject.transform.position = endGameOriginal;

        endResultText.text = ("It is October 31st, 1940. The Luftwaffe has been ordered to pull back to Russia, where the Fuhrer will begin Operation barbarossa. The Battle of Britain is over!" +
            "\n" + "Over the course of this battle " + germanFighters.ToString() + " german fighters," +
            "\n" + germanBombers.ToString() + " german bombers, and " +
            "\n" + britishFighters.ToString() + " british fighters have been shot down!" +
            "\n" + victory);
    }

    void SetButtons()
    {
        endButton.onClick.AddListener(ExitGame);
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
