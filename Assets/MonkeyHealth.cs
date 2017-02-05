using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyHealth : MonoBehaviour {

	public float health = 100f;
	// Use this for initialization
	PlayerHealth pl;
	void Start() {
		pl = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerHealth>();
	}
	
	// Update is called once per frame
	void Update() {
		if (health <= 0) {
			Destroy(gameObject);
		}
	}
	
	public void damage(float d) {
		health -= d;
	}
}
