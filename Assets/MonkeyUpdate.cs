using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Director;

public class MonkeyUpdate : MonoBehaviour {
	
	public float minDistance;
	GameObject player;
	public float speed;
	PlayerHealth pl;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("MainCamera");
		speed = 3f;
		minDistance = 2f;
		pl = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerHealth>();
	}
	
	// Update is called once per frame
	void Update () {
		// always look at player
		transform.LookAt(player.transform);
		
		if (Vector3.Distance(transform.position, player.transform.position) > minDistance) {
			transform.position += transform.forward * speed * Time.deltaTime;
		} else {
			// kill the player
			pl.damage(100f);
		}
	}
}
