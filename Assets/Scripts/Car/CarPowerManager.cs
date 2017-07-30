using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPowerManager : MonoBehaviour {

	public float maxPower;
	public float rechargeRate;

	[HideInInspector]
	public float power;

	[HideInInspector]
	public float powerModifier;

	private CarControl controller;

	// Use this for initialization
	void Start() {
		controller = GetComponent<CarControl>();
		power = maxPower;

	}

	// Update is called once per frame
	void FixedUpdate() {
		// Debug.Log(power);

		if (controller.acceleration != 0) power -= Mathf.Abs(controller.acceleration) * Time.fixedDeltaTime * (controller.boosting ? 20 : 10);
		else power += rechargeRate * Time.fixedDeltaTime;
		power = Mathf.Clamp(power, 0, maxPower);

		powerModifier = power > maxPower * 0.25f ? 1 : power / (maxPower * 0.25f);
	}
}
