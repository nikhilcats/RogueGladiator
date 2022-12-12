using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class RetrieveStats : MonoBehaviour
{
    private TextMeshProUGUI timeText;
    private TextMeshProUGUI floorsText;
    private TextMeshProUGUI mobsText;
    private TextMeshProUGUI pointsText;

    void Start()
    {
      timeText = GameObject.Find("TimeText").GetComponent<TextMeshProUGUI>();
      floorsText = GameObject.Find("FloorText").GetComponent<TextMeshProUGUI>();
      mobsText = GameObject.Find("MobText").GetComponent<TextMeshProUGUI>();
      pointsText = GameObject.Find("PointText").GetComponent<TextMeshProUGUI>();

      //set text based on playerpref values
      timeText.text = "TIME ELAPSED: " + Math.Round(PlayerPrefs.GetFloat("timeTaken")) + " Seconds";
      floorsText.text = "FLOORS CLEARED: " + PlayerPrefs.GetInt("floorsCleared");
      mobsText.text = "MOBS KILLED: " + PlayerPrefs.GetInt("mobsKilled");
      pointsText.text = "SCORE: " + PlayerPrefs.GetInt("points");

      //reset playerprefs for next run
      PlayerPrefs.SetInt("floorsCleared", 0);
      PlayerPrefs.SetInt("mobsKilled", 0);
      PlayerPrefs.SetInt("points", 0);
      PlayerPrefs.SetFloat("timeTaken", 0f);
    }

    void Update()
    {

    }
}
