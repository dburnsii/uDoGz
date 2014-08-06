using UnityEngine;
using System.Collections;

public class downPlayerColliderBox : MonoBehaviour {

	public bool collision;
	public bool door;
	public bool talk;
	public string building;
	public int intruders;
	public GameObject thing1;
	public GameObject thing2;
	private GameObject player;
	private GameObject fakeGameobject;

	void Start () 
	{
		talk = false;
		collision = false;
		door = false;
		intruders = 0;
		player = GameObject.Find ("Player");
		fakeGameobject = new GameObject ("fakeGameobject"); //Using fakeGameobject so not as to create a new GameObject with every OnTriggerExit()
		thing1 = fakeGameobject;
		thing2 = fakeGameobject;
	}

	void Update () 
	{
		Vector3 temp = new Vector3(0, -1, 0); //Keeps box beneath the player
		transform.position = temp + player.transform.position;
//		if (thing1.name != "fakeGameobject" && thing1.gameObject.tag == "talkable") 
//		{
//			building = thing1.gameObject.name;
//		}
//		else if (thing2.name != "fakeGameobject" && thing2.gameObject.tag == "talkable") 
//		{
//			building = thing2.gameObject.name;
//		}
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (thing1.name == "fakeGameobject") //Sets thing1 if its "empty"
		{
			thing1 = coll.gameObject;
		}
		else if(thing2.name == "fakeGameobject") //Alternatively sets thing2
		{
			thing2 = coll.gameObject;
		}
		if (coll.gameObject.tag == "Building" || coll.gameObject.tag == "Friendly")  //Determining the booleans to change based on tag
		{
			collision = true;
		}
		else if (coll.gameObject.tag == "Talkable") 
		{
			collision = true;
			talk = true;
			building = coll.gameObject.name;
		}
		else if (coll.gameObject.tag == "Door") 
		{
			door = true;
			building = coll.gameObject.name;
		}
		intruders++; //Add one to number of intruders within collision box
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject.name == thing1.name) //The object that has left is thing1, so transfer permissions to that of thing2
		{
			thing1 = fakeGameobject;
			if (thing2.gameObject.tag == "Talkable") 
			{
				collision = true;
				talk = true;
				door = false;
				//building = thing2.gameObject.name;
			} 
			else if (thing2.gameObject.tag == "Building" || thing2.gameObject.tag == "Friendly") 
			{
				collision = true;
				door = false;
				talk = false;
				//building = thing2.gameObject.name;
			} 
			else if (thing2.gameObject.tag == "Door") 
			{
				door = true;
				talk = false;
				collision = false;
				//building = thing2.gameObject.name;
			}
		} 
		else if (coll.gameObject.name == thing2.name) //thing2 has left, transfer permissions to thing1
		{
			thing2 = fakeGameobject;
			if (thing1.gameObject.tag == "Talkable") 
			{
				collision = true;
				talk = true;
				door = false;
				//building = thing2.gameObject.name;
			} 
			else if (thing1.gameObject.tag == "Building" || thing1.gameObject.tag == "Friendly") 
			{
				collision = true;
				talk = false;
				door = false;
				//building = coll.gameObject.name;
			} 
			else if (thing1.gameObject.tag == "Door") 
			{
				door = true;
				talk = false;
				collision = false;
				//building = coll.gameObject.name;
			}
		} 
		intruders--;
		if (intruders == 0) //Since there is nothing in the collision box, all booleans are false
		{
			collision = false;
			door = false;
			talk = false;
		}
	}

	public bool getColl()
	{
		return collision;
	}
}
