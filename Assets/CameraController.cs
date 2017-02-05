using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public WebCamTexture mCamera = null;
	public GameObject plane;

	// Use this for initialization
	void Start ()
	{
		Debug.Log("Script has been started");
		plane = GameObject.FindWithTag("Player");

		mCamera = new WebCamTexture();
		plane.GetComponent<Renderer>().material.mainTexture = mCamera;
//		plane.GetComponent<Renderer>().material.mainTexture.transform.localScale = new Vector3(1, -1, 0.0);
//		plane.GetComponent<Renderer>().material.SetTextureScale("", new Vector2(1, -1));
//		plane.GetComponent<Renderer>().material.mainTexture.SetTextureScale();
		mCamera.Play();
		
		

	}

	// Update is called once per frame
	void Update()
	{

	}
}