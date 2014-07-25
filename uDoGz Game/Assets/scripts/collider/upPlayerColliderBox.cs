using UnityEngine;
using System.Collections;

public class upPlayerColliderBox : MonoBehaviour {

	public bool collision;
	private GameObject player;
	public string testInt;
	public bool door;
	public string building;
	// Use this for initialization
	void Start () {
		collision = false;
		door = false;
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 temp = new Vector3(0, 1, 0);
		transform.position = temp + player.transform.position;
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Building" || coll.gameObject.tag == "Friendly") 
		{
			collision = true;
		}
		if (coll.gameObject.tag == "Door") 
		{
			door = true;
			building = coll.gameObject.name;
		}
		Debug.Log ("Up collision box has detected an obstacle");
	}

	void OnTriggerExit2D()
	{
		collision = false;
		door = false;
	}

	public bool getColl()
	{
		return collision;
	}
}
