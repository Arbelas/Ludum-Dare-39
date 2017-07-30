using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StatsPage : MonoBehaviour {

	Text stats;

	// Use this for initialization
	void Start () {
		stats = GetComponent<Text>();
		stats.text = $"Best Race: " + (Statistics._instance.bestRaceTime == Mathf.Infinity ? "NA" : TimerUI.FormatFloatTime(Statistics._instance.bestRaceTime)) +
				  $"\n  Best Lap: " + (Statistics._instance.bestLapTime == Mathf.Infinity ? "NA" : TimerUI.FormatFloatTime(Statistics._instance.bestLapTime));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Menu() {
		SceneManager.LoadScene("Menu");
	}
}
