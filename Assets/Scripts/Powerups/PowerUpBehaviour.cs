using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PowerUpBehaviour : MonoBehaviour {

	public float powerBonus = -1f;
	public float rotationSpeed;

	public AudioClip pickupSound;

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {

		transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + rotationSpeed * Time.deltaTime);
		//TODO maybe add some kind of scale change

	}

	//Right now this will be hardcoded to give more power, but the behaviour may be later extended to other powerup effects
	public void GetPowerUp(GameObject car) {
		CarPowerManager powerManager = car.GetComponent<CarPowerManager>();
		Assert.IsNotNull(powerManager); // Ensure that I haven't messed up somewhere else.

		Debug.Log("Power up!");

		AudioSource.PlayClipAtPoint(pickupSound,transform.position);

		if (powerBonus > 0) powerManager.power += powerBonus;
		else powerManager.power = powerManager.maxPower;

		//TODO add some kind of pickup effect.

		Destroy(gameObject);
	}

}
