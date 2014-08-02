using UnityEngine;
using System.Collections;

public delegate void bButttonPressEventHandler();

public class bScript : MonoBehaviour {

	public static event bButttonPressEventHandler bButtonPressed;
	public Texture2D btnTexture;
	private float xPosition;
	private GUIStyle style;
	// Use this for initialization
	void Start () 
	{
		xPosition = Camera.main.transform.position.x + (Camera.main.pixelWidth / 100) - 1.5f;
		transform.position = new Vector3( xPosition , Camera.main.transform.position.y - 3.5f, -1);
		style = new GUIStyle();
		style.normal.background = btnTexture;
		//btnTexture.width = 100;
	}
	
	void OnGUI()
	{
		if (GUI.Button(new Rect(Camera.main.pixelWidth - 300, Camera.main.pixelHeight - 150, 100, 100),"",  style))
			Debug.Log("Clicked the button with an image - " + xPosition);
	}
}
