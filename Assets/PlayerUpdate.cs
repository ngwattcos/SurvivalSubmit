using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

public class PlayerUpdate : MonoBehaviour {
	
	GameObject[] enemies;
	public Rigidbody bulletPrefab;
	public float speed = 10f;
	// Use this for initialization
	
	PlayerLook pl;
	void Start() {
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		
		pl = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerLook>();
	}
	
	void fireBullet() {
		Rigidbody bulletInstance;
		bulletInstance = Instantiate(bulletPrefab, transform.position, transform.rotation) as Rigidbody;
		bulletInstance.AddForce(transform.forward * 1000);
//		bullet.velocity = transform.forward * speed;
	}
	
	// Update is called once per frame
	void Update() {
		if (Input.touchCount == 1 || Input.GetButtonDown("Fire1")) {

			// launch a projectile
			fireBullet();
			
			/*RaycastHit hit;
			Vector3 fwd = transform.TransformDirection(Vector3.forward);
			Ray landingRay = new Ray(transform.position, Vector3.forward);
			
//			Ray ray = Camera.main.ScreenPointToRay(vector);
			if (Physics.Raycast(landingRay, out hit, Mathf.Infinity)) {
				GameObject obj = hit.collider.gameObject;
				if (obj == gameObject) {
//					obj.GetComponent<MonkeyHealth>().attack();
					MonkeyHealth mh = hit.collider.GetComponent<MonkeyHealth>();
					mh.damage(100);
					
				}
			}*/
		}
	}
}
