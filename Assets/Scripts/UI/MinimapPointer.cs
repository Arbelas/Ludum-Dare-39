using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MinimapPointer : MonoBehaviour {

	public float positionCoefficient;

	Transform playerCar;

	// Use this for initialization
	void Start () {
		playerCar = GameObject.Find("Player Car").transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = playerCar.position * positionCoefficient;
		transform.eulerAngles = playerCar.eulerAngles;
	}
}
