using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerGaugeScaler : MonoBehaviour {

	//public float zeroPoint;

	private CarPowerManager powerManager;
	private Transform rect;
	private float maxWidth;

	// Use this for initialization
	void Start() {
		rect = GetComponent<Transform>();
		powerManager = transform.parent.parent.GetComponent<CarPowerManager>();

		maxWidth = rect.localScale.x;
	}

	// Update is called once per frame
	void Update() {
		//ebug.Log(new Vector2(maxWidth * (powerManager.power / powerManager.maxPower), rect.localScale.y));
		rect.localScale = new Vector2(maxWidth * (powerManager.power / powerManager.maxPower), rect.localScale.y);
	}

}
