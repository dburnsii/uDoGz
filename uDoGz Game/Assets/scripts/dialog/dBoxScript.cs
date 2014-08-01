using UnityEngine;
using System.Collections;
using System.Xml;
using System.Text;

public class dBoxScript : MonoBehaviour
{

	public bool visible;
	public string text;
	private GameObject section;
	private Camera camera;
	private playerMovementScript player;
	public float ratio;
	private Renderer spriteRenderer;
	public string name;
	public int transform;
	private TextMesh dialog;
	private float referenceTime;
	private int index;
	private bool donePrinting;
	public string subject;
	private float middleWidth;

	// Use this for initialization
	void Start ()
	{
		visible = false;
		spriteRenderer = renderer as SpriteRenderer;
		camera = Camera.main;
		ratio = camera.aspect;
		player = ( playerMovementScript )FindObjectOfType ( typeof( playerMovementScript ) );
		section = GameObject.Find (name);
		TextMesh[] temp;
		temp = (TextMesh[])FindObjectsOfType (typeof(TextMesh));
		referenceTime = 0;
		for (int i = 0; i < temp.Length; i++) 
		{
			if(temp[i].name == "dialog")
			{
				dialog = temp[i];
				break;
			}
		}

		//referenceTime = 0;
		//textMesh = new TextMesh ();
		//textMesh.transform.position = new Vector3 (0, 0, 0);
		//textMesh.renderer = new SpriteRenderer();
		//camera = GameObject.Find ("Camera");
		//leftSide.transform.position = new Vector3 ((float) (camera.transform.position.x - (0 * ratio)), camera.transform.position.y - 4, 0);
		//rightSide.transform.position = new Vector3 (camera.transform.position.x + 7, camera.transform.position.y - 4, 0);
		//middle.transform.position = new Vector3 (camera.transform.position.x , camera.transform.position.y - 4, 0);
		//player.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (visible) 
		{
			spriteRenderer.enabled = true;
			ratio = camera.aspect;
			player.enabled = false;
			section.transform.position = new Vector2 ( camera.transform.position.x + (4 * ratio * transform), camera.transform.position.y - 4);
			//rightSide.transform.position = new Vector2 ( camera.transform.position.x + (4 * ratio), camera.transform.position.y - 4);
			//middle.transform.position = new Vector3 (camera.transform.position.x , camera.transform.position.y - 4, 0);
			//
			dialog.transform.position = new Vector3( camera.transform.position.x - (4 * ratio), camera.transform.position.y - 3f, -1);
			middleWidth = (float) ((4*ratio)-0.5);
			if(name == "dBoxMiddle")
			{
				section.transform.localScale = new Vector3( middleWidth, (float) 0.5, 1);
			}
			gatherDialog(subject);
		} 
		else 
		{
			spriteRenderer.enabled = false;
		}
	}

	void runDialog(string text)
	{
		visible = true;
	}

	void gatherDialog(string subject)
	{
		string line;
		string index = "";
		XmlReader xmlReader = XmlReader.Create ("Assets/scripts/dialog/" + subject + ".xml");
		while (xmlReader.Read ()) 
		{
			if(xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "location")
			{
				if(xmlReader.GetAttribute("index") != null)
				{
					index = xmlReader.GetAttribute("index");
				}
			}
			else if(xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "message")
			{
				if(xmlReader.GetAttribute("index") == index)
				{
					text = xmlReader.GetAttribute("text");
					writeDialog (text);
				}
			}
		}
		return;
	}

	void writeDialog(string input)
	{
		int maxLineChars = (int)  middleWidth * 10;
		int minLineChars = 10;
		string[] words;
		string result = "";
		int charCount = 0;
		int currentLine = 1;
		words = input.Split (" " [0]);

		for(int i = 0; i < words.Length; i++)
		{
			string word = words[i].Trim ();
			if(i == 0)
			{
				result = words[0];
				dialog.text = result;
			}
			if(i > 0)
			{
				charCount += word.Length + 1;
				if(charCount <= maxLineChars)
				{
					result += " " + word;
				}
				else
				{
					charCount = 0;
					result += "\n " + word;
				}
			}
		}
		//if (result [0] != " ") 
		//{
		//	result = " " + result;
		//}
		dialog.text = result;
		//dialog.text = input;
	}

	void updateIndex(string subject, int modifier)
	{
		return;
	}

}
