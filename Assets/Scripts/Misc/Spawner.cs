using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public float timeToRespawn;
	public GameObject spawnedItem;

	private GameObject instance;
	private bool bRespawning;
	// Use this for initialization
	void Start () {
		Spawn();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(instance == null && !bRespawning) {
			StartCoroutine(SpawnTimer());
		}

	}

	void Spawn() {
		instance = Instantiate(spawnedItem);
		instance.transform.SetParent(transform);
		instance.transform.localPosition = new Vector3(0, 0, 0);
	}

	IEnumerator SpawnTimer() {
		bRespawning = true;
		yield return new WaitForSeconds(timeToRespawn);
		Spawn();
		bRespawning = false;
	}
}
