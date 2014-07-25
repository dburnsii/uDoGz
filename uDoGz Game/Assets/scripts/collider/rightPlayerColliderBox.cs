using UnityEngine;
using System.Collections;

public class rightPlayerColliderBox : MonoBehaviour {

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
		Vector3 temp = new Vector3(1, 0, 0);
		transform.position = temp + player.transform.position;
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Building" || coll.gameObject.tag == "Friendly") 
		{
			collision = true;
		}
		Debug.Log ("Right collision box has detected an obstacle");
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
