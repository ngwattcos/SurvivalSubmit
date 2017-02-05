using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUICrosshair : MonoBehaviour {

	public Texture2D m_CrosshairTex;
	Vector2 m_WindowSize;
	Rect m_CrosshairRect;
	
	
	// Use this for initialization
	void Start() {
		m_CrosshairTex = new Texture2D(2, 2);
		m_WindowSize = new Vector2(Screen.width, Screen.height);
		CalculateRect();
	}
	
	// Update is called once per frame
	void Update() {
		if (m_WindowSize.x != Screen.width || m_WindowSize.y != Screen.height) {
//			CalculateRect();
		}
		
		m_CrosshairRect = new Rect(
			(m_WindowSize.x - m_CrosshairTex.width)/2.0f,
			(m_WindowSize.y - m_CrosshairTex.height)/2.0f,
			m_CrosshairTex.width, m_CrosshairTex.height);
		
//		GUI.DrawTexture(m_CrosshairRect, m_CrosshairTex);
	}
	
	void CalculateRect() {
		m_CrosshairRect = new Rect(
			(m_WindowSize.x - m_CrosshairTex.width)/2.0f,
			(m_WindowSize.y - m_CrosshairTex.height)/2.0f,
			m_CrosshairTex.width, m_CrosshairTex.height);
	}
	
//	void OnGui() {
//		
//	}
}
