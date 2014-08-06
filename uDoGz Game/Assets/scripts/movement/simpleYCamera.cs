using UnityEngine;
using System.Collections;

public class simpleYCamera : MonoBehaviour {


	private playerMovementScript player;

	void Start () 
	{
		player = (playerMovementScript)FindObjectOfType (typeof(playerMovementScript));
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = new Vector3(0, player.transform.position.y, -6);
	}
}
