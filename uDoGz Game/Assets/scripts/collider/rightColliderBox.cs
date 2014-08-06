using UnityEngine;
using System.Collections;

public class rightColliderBox : MonoBehaviour {

	public bool collision;
	private GameObject player;
	public string building;
	public string target;

	void Start () 
	{
		collision = false;
		player = GameObject.Find (target);
	}

	void Update () 
	{
		Vector3 temp = new Vector3(1, 0, 0);
		transform.position = temp + player.transform.position;
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		collision = true;
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
