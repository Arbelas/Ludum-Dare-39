using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour {

	public float acceleration;
	public float maxSpeed;
	public float reverseMaxSpeed;
	public float turnSpeed;

	private float speed;

	// Use this for initialization
	void Start() {
		speed = 0;
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown(KeyCode.UpArrow)) maxSpeed++;
		if (Input.GetKeyDown(KeyCode.DownArrow)) maxSpeed--;
		if (Input.GetKeyDown(KeyCode.LeftArrow)) turnSpeed -= 5;
		if (Input.GetKeyDown(KeyCode.RightArrow)) turnSpeed += 5;
		if (Input.GetKeyDown(KeyCode.Period)) acceleration++;
		if (Input.GetKeyDown(KeyCode.Comma)) acceleration--;
	}

	void FixedUpdate() {

		Move();

	}

	void Move() {
		float verticalInput = Input.GetAxis("Vertical");
		float horizontalInput = Input.GetAxis("Horizontal");

		//This could probably be done in a much better fashion, but this was the first thing that came to mind.
		//TODO Experiment with different braking acceleration
		if (verticalInput != 0) speed += acceleration * Time.fixedDeltaTime * (verticalInput > 0 ? 1 : -2);
		else if (Mathf.Abs(speed) < acceleration / 2 * Time.fixedDeltaTime) speed = 0;
		else if (speed < 0) speed += acceleration / 2 * Time.fixedDeltaTime;
		else speed -= acceleration / 2 * Time.fixedDeltaTime;

		//Debug.Log(speed);

		if (speed > maxSpeed) speed = maxSpeed;
		else if (speed < reverseMaxSpeed) speed = reverseMaxSpeed;

		//Debug.Log(turnSpeed * horizontalInput * (speed / maxSpeed) * Time.fixedDeltaTime);
		transform.eulerAngles = new Vector3(0,0,transform.eulerAngles.z + (turnSpeed * Input.GetAxis("Horizontal") * (speed / maxSpeed) * Time.fixedDeltaTime));

		transform.Translate(Vector2.up * speed * Time.fixedDeltaTime);
	}
}
