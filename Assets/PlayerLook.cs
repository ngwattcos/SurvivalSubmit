using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerLook : MonoBehaviour {

	public GameObject camParent;
	
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 5F;
	public float sensitivityY = 5F;
	public float minimumX = -360F;
	public float maximumX = 360F;
	public float minimumY = -60F;
	public float maximumY = 60F;
	float rotationX = 0F;
	float rotationY = 0F;
	
	public bool alive = true;
	
	public int numEnemies = 3;
	
	public float time = 0f;
	
	public Vector2 mousePosition;
	
	public Rigidbody monkeyPrefab;
	public Rigidbody trumpPrefab;
	
	Quaternion originalRotation;
	
	Text txt;
	Text gm;
	
	public Boolean alreadySpawned = false;
	
	void Start() {
		txt = GameObject.FindGameObjectWithTag("EnemyLabel").GetComponent<Text>();
		
		gm = GameObject.FindGameObjectWithTag("GameOver").GetComponent<Text>();
		gm.enabled = false;
		
		// Make the rigid body not change rotation
		originalRotation = transform.localRotation;

//		mousePosition = Event.current.mousePosition;

		camParent = new GameObject("camParent");
		camParent.transform.position = this.transform.position;
		this.transform.parent = camParent.transform;
		Input.gyro.enabled = true;
		Input.gyro.updateInterval = 0.01f;
	}
	
	void Update() {
//		Debug.Log(camParent.name);
		Debug.Log(Input.gyro.enabled);
		camParent.transform.Rotate(0, -Input.gyro.rotationRateUnbiased.y * 4, 0);
		transform.Rotate(-Input.gyro.rotationRateUnbiased.x * 4, 0, 0);
//		transform.localRotation = Quaternion.Euler(0, Input.compass.trueHeading, 0);
//		
//		if (Input.gyro.enabled) {
//			rotationY = Input.gyro.rotationRate.y;
//			rotationY = ClampAngle(rotationY, minimumY, maximumY);
//			Quaternion yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);
//			transform.localRotation = originalRotation * yQuaternion;
//		}
		
//		rotationX += 1;
//		rotationX = ClampAngle(rotationX, minimumX, maximumX);
//		Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
//		transform.localRotation = originalRotation * xQuaternion;
		
		/*if (axes == RotationAxes.MouseXAndY) {
			// Read the mouse input axis
			rotationX += Input.GetAxis("Mouse X") * sensitivityX;
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationX = ClampAngle(rotationX, minimumX, maximumX);
			rotationY = ClampAngle(rotationY, minimumY, maximumY);
			Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
			Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
			transform.localRotation = originalRotation * xQuaternion * yQuaternion;
			mousePosition.x = Mathf.Clamp(mousePosition. x, 10, 500);
			mousePosition.y = Mathf.Clamp(mousePosition. y, 10, 500);
		}
		else if(axes == RotationAxes.MouseX) {
			rotationX += Input.GetAxis("Mouse X") * sensitivityX;
			rotationX = ClampAngle(rotationX, minimumX, maximumX);
			Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
			transform.localRotation = originalRotation * xQuaternion;
			mousePosition.x = Mathf.Clamp(mousePosition. x, 10, 500);
			mousePosition.y = Mathf.Clamp(mousePosition. y, 10, 500);
		}
		else {
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = ClampAngle(rotationY, minimumY, maximumY);
			Quaternion yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);
			transform.localRotation = originalRotation * yQuaternion;
			mousePosition.x = Mathf.Clamp(mousePosition. x, 10, 500);
			mousePosition.y = Mathf.Clamp(mousePosition. y, 10, 500);
		}*/
		if (alive) {
			if (time > 5f && time % 5 < 1  && !alreadySpawned) {
				Rigidbody enemyInstance;
				if (UnityEngine.Random.Range(0, 1) < 0.5) {
					enemyInstance = Instantiate(monkeyPrefab,
						new Vector3(
							UnityEngine.Random.Range(-100, 100),
							5.1f,
							UnityEngine.Random.Range(-100, 100)), Quaternion.identity) as Rigidbody;
				} else {
					enemyInstance = Instantiate(trumpPrefab,
						new Vector3(
							UnityEngine.Random.Range(-100, 100),
							5.1f,
							UnityEngine.Random.Range(-100, 100)), Quaternion.identity) as Rigidbody;
				}
				
				numEnemies += 1;
				alreadySpawned = true;
			}
			if (time > 5f && time % 5 >= 1) {
				alreadySpawned = false;
			}
		}
		
		
		txt.text = "Enemies: " + numEnemies;
		time += Time.deltaTime;
	}
	

	
	public static float ClampAngle(float angle, float min, float max) {
		if (angle < -360F) {
			angle += 360F;
		}
		if (angle > 360F) {
			angle -= 360F;
		}
		return Mathf.Clamp(angle, min, max);
	}
}
