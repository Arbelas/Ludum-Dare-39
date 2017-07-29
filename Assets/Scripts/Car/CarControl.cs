using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour {

	public float accelerationRate;
	public float maxSpeed;
	public float reverseMaxSpeed;
	public float turnSpeed;

	[HideInInspector]
	public float speed;

	private float verticalInput, horizontalInput;
	private CarPowerManager powerManager;

	public float acceleration {
		get {
			return accelerationRate * verticalInput * (verticalInput < 0 && speed > 0 ? 2 : 1);
		}
	}

	private float maximumSpeed {
		get {
			return maxSpeed * powerManager.powerModifier;
		}
	}

	private float reverseMaximumSpeed {
		get {
			return reverseMaxSpeed * powerManager.powerModifier;
		}
	}

	// Use this for initialization
	void Start() {
		speed = 0;
		powerManager = GetComponent<CarPowerManager>();
	}

	// Update is called once per frame
	void Update() {
		/*if (Input.GetKeyDown(KeyCode.UpArrow)) maxSpeed++;
		if (Input.GetKeyDown(KeyCode.DownArrow)) maxSpeed--;
		if (Input.GetKeyDown(KeyCode.LeftArrow)) turnSpeed -= 5;
		if (Input.GetKeyDown(KeyCode.RightArrow)) turnSpeed += 5;
		if (Input.GetKeyDown(KeyCode.Period)) acceleration++;
		if (Input.GetKeyDown(KeyCode.Comma)) acceleration--;*/
	}

	void FixedUpdate() {

		verticalInput = Input.GetAxis("Vertical");
		horizontalInput = Input.GetAxis("Horizontal");

		verticalInput *= powerManager.powerModifier;

		Move();
	}

	void Move() {

		//This could probably be done in a much better fashion, but this was the first thing that came to mind.
		//TODO Experiment with different braking acceleration
		if (verticalInput != 0) speed += acceleration * Time.fixedDeltaTime;
		else if (Mathf.Abs(speed) < accelerationRate * 2 * Time.fixedDeltaTime) speed = 0;
		else if (speed < 0) speed += accelerationRate * 2 * Time.fixedDeltaTime;
		else speed -= accelerationRate * 2 * Time.fixedDeltaTime;

		//Debug.Log(speed);

		speed = Mathf.Clamp(speed, reverseMaximumSpeed, maximumSpeed);

		//Debug.Log(turnSpeed * horizontalInput * (speed / maxSpeed) * Time.fixedDeltaTime);
		transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + (turnSpeed * horizontalInput * speed / maxSpeed * Time.fixedDeltaTime));

		transform.Translate(Vector2.up * speed * Time.fixedDeltaTime);
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.tag == "PowerUp") collision.GetComponent<PowerUpBehaviour>().GetPowerUp(gameObject);
	}

	void OnCollisionStay2D(Collision2D collision) {
		if (Mathf.Abs(speed) < accelerationRate * 2 * Time.fixedDeltaTime) speed = 0;
		else if (speed < 0) speed += accelerationRate * 2 * Time.fixedDeltaTime;
		else speed -= accelerationRate * 2 * Time.fixedDeltaTime;

		transform.Translate((-collision.contacts[0].point - (Vector2) transform.position).normalized/5);
	}

}
