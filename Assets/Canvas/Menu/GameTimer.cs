using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script controls the game clock, the random weather generator and the time at which the game ends

public class GameTimer : MonoBehaviour {

    public GameObject gameTimerObject;
    public GameObject lightObject;

    public Text gameTimerText;
    Light lightComp;
    SingleAI singleAI;

    public int day;
    public int hour;
    public int rain;
    // 0 clear skies
    // 1 rain
    // 2 heavy rain
    public int fog;
    // 0 no fog
    // 1 fog
    // 2 dense fog

    public int fogRender;

    public float hourTimer;

    bool nightTime;
    public bool weatherReady;

    public string rainString;
    public string fogString;

    public Color dayColor;


    void Start () {

        dayColor = new Color(0.5f, 0.5f, 0.5f);

        nightTime = false;
        weatherReady = true;
        gameTimerObject = GameObject.Find("GameTimer");
        gameTimerText = gameTimerObject.GetComponent<Text>();

        lightObject = GameObject.Find("Directional Light");
        lightComp = lightObject.GetComponent<Light>();
        singleAI = gameObject.GetComponent<SingleAI>();

        day = 0;
        hour = 0;
        hourTimer = 0;
        rain = 0;
        fog = 0;

        RenderSettings.fog = true;

        fogRender = 500;
        SetWeather();      

    }
	
	void Update () {

        hourTimer += Time.deltaTime;
        DayTime();
        SetTimeText();

        RenderSettings.fogEndDistance = fogRender;

        if (Input.GetKeyDown("u"))
        {
            day = 10; // ENDS THE GAME MANUALLY, use for videos, presentations etc...
        }
    }

    void DayTime()
    {
        if(day == 10) // END THE GAME AT x Days
        {
            EndGame endGame = GameObject.Find("EndGame").GetComponent<EndGame>();
            endGame.EndGameResults();
        }


        if (hourTimer > 10) // sets time of hour in game to 10 real seconds.
        {
            hour = hour + 1;
            hourTimer = 0;
            singleAI.BritishAI(); // iterates britishAI
            singleAI.GermanAI(); // iterates german AI

            int weatherChance = Random.Range(1, 6); // This chance determines how often the weather changes.
            if(weatherChance == 1)
            {
                weatherReady = true;
                SetWeather();
            }
        }

        if (hour > 23)
        {
            hour = 0;
            day = day + 1;
        }

        if( (hour > 6) &&(hour < 20))
        {
            nightTime = false;
            if(dayColor.r < 0.99f)
            {
                dayColor.r = dayColor.r + 0.01f;
            }
            if (dayColor.g < 0.95f)
            {
                dayColor.g = dayColor.r + 0.01f;
            }
            if (dayColor.b < 0.83f)
            {
                dayColor.b = dayColor.r + 0.01f;
            }
        }
        else
        {
            nightTime = true;
            if (dayColor.r > 0.1f)
            {
                dayColor.r = dayColor.r - 0.01f;
            }
            if (dayColor.g > 0.1f)
            {
                dayColor.g = dayColor.r - 0.01f;
            }
            if (dayColor.b > 0.1f)
            {
                dayColor.b = dayColor.r - 0.01f;
            }
        }

        lightComp.color = dayColor;
    }

    void SetTimeText()
    {
        if (nightTime == false)
        {
            gameTimerText.text = "Current day: " + day.ToString() + "\n" + "Current Hour: " + hour.ToString() + "\n" + "Daytime" + "\n" + rainString + " with " + fogString;
        }

        if (nightTime == true)
        {
            gameTimerText.text = "Current day: " + day.ToString() + "\n" + "Current Hour: " + hour.ToString() + "\n" + "Night-time" + "\n" + rainString + " with " + fogString;
        }
    }

    void SetWeather()
    {

        fogRender = 500; // Returns the fog to the default clear skies and no rain setting

        if(weatherReady == true) // Becomes true if the game timer wants to change the weather
        {
            int rainChance = Random.Range(1, 3); // chance of rain
            if(rainChance == 1)
            {
                rain = 1;
                fogRender = fogRender - 40;
                rainString = "Rain";
                int heavyRainChance = Random.Range(1, 3); // chance of heavy rain ( after rain)
                if(heavyRainChance == 1)
                {
                    rain = 2;
                    fogRender = fogRender - 50;
                    rainString = "Heavy rain";
                }
            }
            else
            {
                rain = 0;
                rainString = "Clear skies";
            }

            int fogChance = Random.Range(1, 5); // chance of fog
            if (fogChance == 1)
            {
                fog = 1;
                fogRender = fogRender - 60;
                fogString = "Fog";
                int denseFogChance = Random.Range(1, 5); // chance of dense fog (after fog)
                if(denseFogChance == 1)
                {
                    fog = 2;
                    fogRender = fogRender - 70;
                    fogString = "Dense fog";
                }
            }
            else
            {
                fog = 0;
                fogString = "No fog";
            }

            weatherReady = false;
        }
    }

    public int GetRain()
    {
        return rain;
    }

    public int GetFog()
    {
        return fog;
    }

    public bool GetNightTime()
    {
        return nightTime;
    }

}
