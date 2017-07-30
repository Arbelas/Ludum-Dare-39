﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Play() {
		SceneManager.LoadScene("MainScene");
	}

	public void Quit() {
		Application.Quit();
	}

	public void Stats() {
		SceneManager.LoadScene("Stats");
	}

	public void Tutorial() {
		SceneManager.LoadScene("Tutorial");
	}

}
