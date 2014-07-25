﻿using UnityEngine;
using System.Collections;

public class downPlayerColliderBox : MonoBehaviour {

	public bool collision;
	private GameObject player;
	public string testInt;
	// Use this for initialization
	void Start () {
		collision = false;
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 temp = new Vector3(0, -1, 0);
		transform.position = temp + player.transform.position;
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Building") 
		{
			collision = true;
		}
		Debug.Log ("Down collision box has detected an obstacle");
	}

	void OnTriggerExit2D()
	{
		collision = false;
	}

	public bool getColl()
	{
		return collision;
	}
}
