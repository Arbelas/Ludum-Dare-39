using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelGaugeScaler : MonoBehaviour {

	public float PLAYER_POWER;
	public float MAX_PLAYER_POWER;

	public float zeroPoint;

	private RectTransform rect;
	private float maxWidth;

	// Use this for initialization
	void Start() {
		rect = GetComponent<RectTransform>();
		maxWidth = rect.sizeDelta.x - zeroPoint;
	}

	// Update is called once per frame
	void Update() {
		//TODO add real player power to this equation
		rect.sizeDelta = new Vector2(maxWidth * (PLAYER_POWER / MAX_PLAYER_POWER) + zeroPoint, rect.sizeDelta.y);
	}
}
