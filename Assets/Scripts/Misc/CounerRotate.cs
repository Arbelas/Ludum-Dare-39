using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CounerRotate : MonoBehaviour {

	//This script counters the rotation of the parent object

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.eulerAngles = Vector3.zero;
	}
}
