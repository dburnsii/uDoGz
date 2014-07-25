using UnityEngine;
using System.Collections;

public class upColliderBox : MonoBehaviour {

	public bool collision;
	private GameObject player;
	public string testInt;
	public bool door;
	public string building;
	public string target;
	// Use this for initialization
	void Start () {
		collision = false;
		door = false;
		player = GameObject.Find (target);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 temp = new Vector3(0, 1, 0);
		transform.position = temp + player.transform.position;
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		collision = true;
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
