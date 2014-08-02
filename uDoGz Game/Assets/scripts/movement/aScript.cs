using UnityEngine;
using System.Collections;

public delegate void aButttonPressEventHandler();

public class aScript : MonoBehaviour {

	public static event aButttonPressEventHandler aButtonPressed;
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
		if (GUI.Button (new Rect (Camera.main.pixelWidth - 150, Camera.main.pixelHeight - 150, 100, 100), "", style))
				if (aButtonPressed != null)
						aButtonPressed ();
	}
}
