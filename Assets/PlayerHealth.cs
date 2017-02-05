using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Director;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public float health = 100f;
	// Use this for initialization
	Text gm;
	PlayerLook pl;
	void Start() {
		pl = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerLook>();
		
		gm = GameObject.FindGameObjectWithTag("GameOver").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update() {
		
	}
	
	public void damage(float d) {
		health -= d;
		if (health <= 0) {
			pl.alive = false;
			gm.enabled = true;
		}
	}
}
