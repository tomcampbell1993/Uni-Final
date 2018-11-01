using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScreen : MonoBehaviour {

    public int germanBombers;
    public int germanFighters;
    public int britishFighters;

    public Text scoreText;

	void Start () {
        scoreText = gameObject.GetComponent<Text>();
        germanBombers = 0;
        germanFighters = 0;
        britishFighters = 0;
    }
	

	void Update () {
        setText();
	}

    public void setText()
    {
        scoreText.text = (
            "Planes destroyed," +
            "\n" + "German bombers: " + germanBombers.ToString() +
            "\n" + "German fighters: " + germanFighters.ToString() +
            "\n" + "British fighters: " + britishFighters.ToString());
    }

    public void addBomber(int x)
    {
        germanBombers = germanBombers + x;
    }

    public void addGermanFighter(int x)
    {
        germanFighters = germanFighters + x;
    }

    public void addBritishFighter(int x)
    {
        britishFighters = britishFighters + x;
    }

    public int GetGermanFighters()
    {
        return germanFighters;
    }
    public int GetGermanBombers()
    {
        return germanBombers;
    }
    public int GetBritishFighters()
    {
        return britishFighters;
    }
}
