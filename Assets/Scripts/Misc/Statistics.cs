using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Statistics {

	public static Statistics _instance = new Statistics();

	public float bestRaceTime = Mathf.Infinity;
	public float bestLapTime = Mathf.Infinity;
	public int powerupsCollected = 0;

}
