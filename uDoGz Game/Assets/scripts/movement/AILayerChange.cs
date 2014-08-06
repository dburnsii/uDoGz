using UnityEngine;
using System.Collections;

public class AILayerChange : MonoBehaviour {

	// Use this for initialization
	private playerMovementScript player;
	private Renderer render;

	void Start () {
		player = (playerMovementScript)FindObjectOfType (typeof(playerMovementScript));
		render = GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y > player.transform.position.y) 
		{
			render.sortingLayerName = "CharactersBehind";
		} 
		else 
		{
			render.sortingLayerName = "CharactersAhead";
		}
	}
}
