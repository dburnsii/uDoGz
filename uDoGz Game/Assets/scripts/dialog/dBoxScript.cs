using UnityEngine;
using System.Collections;
using System.Xml;
using System.Text;

public class dBoxScript : MonoBehaviour
{

	public bool visible;
	public string text;
	private GameObject section;
	private Camera gameCamera;
	private playerMovementScript player;
	public float ratio;
	private Renderer spriteRenderer;
	public string name;
	private TextMesh dialog;
	private int index;
	public string subject;
	private float middleWidth;
	public ArrayList twoLines;
	private int lineIndex;
	private GameObject leftSide;
	private GameObject rightSide;
	private int numberMessages;

	void Start ()
	{
		visible = false;
		spriteRenderer = renderer as SpriteRenderer;
		gameCamera = Camera.main;
		ratio = gameCamera.aspect;
		middleWidth = (float) ((4*ratio)-0.5);
		player = ( playerMovementScript )FindObjectOfType ( typeof( playerMovementScript ) );
		section = GameObject.Find (name);
		TextMesh[] temp;
		temp = (TextMesh[])FindObjectsOfType (typeof(TextMesh));
		for (int i = 0; i < temp.Length; i++) 
		{
			if(temp[i].name == "dialog")
			{
				dialog = temp[i];
				break;
			}
		}
		twoLines = new ArrayList ();
		leftSide = GameObject.Find ("dBoxLeft");
		rightSide = GameObject.Find ("dBoxRight");
	}

	void Update ()
	{

		if (visible) 
		{
			spriteRenderer.enabled = true;
			leftSide.renderer.enabled = true;
			rightSide.renderer.enabled = true;
			dialog.renderer.enabled = true;
			ratio = gameCamera.aspect;
			section.transform.position = new Vector2 ( gameCamera.transform.position.x, gameCamera.transform.position.y - 4);
			leftSide.transform.position = new Vector2 (gameCamera.transform.position.x - (4 * ratio), gameCamera.transform.position.y - 4);
			rightSide.transform.position = new Vector2 (gameCamera.transform.position.x + (4 * ratio), gameCamera.transform.position.y - 4);
			dialog.transform.position = new Vector3( gameCamera.transform.position.x - (4 * ratio), gameCamera.transform.position.y - 3f, -1);
			middleWidth = (float) ((4*ratio)-0.5);
			if(name == "dBoxMiddle")
			{
				section.transform.localScale = new Vector3( middleWidth, (float) 0.5, 1);
			}
		} 
		else 
		{
			spriteRenderer.enabled = false;
			leftSide.renderer.enabled = false;
			rightSide.renderer.enabled = false;
			dialog.renderer.enabled = false;
		}
	}

	public void gatherDialog(string subject)
	{
		Debug.Log("Getting here 3");
		string line;
		string index = "";
		this.subject = subject;
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
		xmlReader.Close ();
		return;
	}

	public void writeDialog(string input)
	{
		Debug.Log("Getting here 2");
		int maxLineChars = (int)  middleWidth * 10;
		int minLineChars = 10;
		string[] words;
		string result = "";
		int charCount = 0;
		int currentLine = 1;
		words = input.Split (" " [0]);
		int lines = 0;
		twoLines.Clear ();

		for(int i = 0; i < words.Length; i++)
		{
			string word = words[i].Trim ();
			if(i == 0)
			{
				result = words[0];
				lines = 1;
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
					twoLines.Add (result);
					result = word;
				}
			}
		}
		if (result != "") 
		{
			twoLines.Add (result);
		}
		Debug.Log (twoLines.Count + " - twoLines.Count at the end of writeDialog()");
	}

	public bool applyText()
	{
		if (lineIndex + 1 <= twoLines.Count) 
		{
			if (twoLines.Count - lineIndex == 1) 
			{
				dialog.text = (string) twoLines [twoLines.Count - 1];
				visible = true;
				twoLines.Clear ();
				lineIndex++;
				player.enabled = false;
				return false;
			}
			dialog.text = twoLines [lineIndex] + "\n" + twoLines [lineIndex + 1];
			player.enabled = false;
			Debug.Log("Applied two lines: \n" + twoLines [lineIndex] + "\n" + twoLines [lineIndex + 1]);
			lineIndex += 2; //CHANGE THIS LINE TO CHANGE NUMBER OF LINES THAT SCROLL WHEN SPACE IS PRESSED
			visible = true;
			return false;
		} 
		else 
		{
			visible = false;
			player.enabled = true;
			Debug.Log("Not applying lines. lineIndex = " + lineIndex + " - twoLines.Count = " + twoLines.Count);
			twoLines.Clear ();
			lineIndex = 0;
			updateIndex (subject, 1);
			return true;
		}
	}

	void updateIndex(string subject, int modifier)
	{
		XmlDocument doc = new XmlDocument ();
		subject = "testHouseBook";
		doc.Load ("Assets/scripts/dialog/" + subject + ".xml");
		numberMessages = doc.SelectNodes (subject + "/location/location").Count;
		string temp = doc.SelectSingleNode(subject + "/location/location").Attributes["index"].InnerText;
		int indexValue = int.Parse (temp);
		indexValue++;
		if (indexValue > numberMessages) 
		{
			return;
		}
		doc.SelectSingleNode (subject + "/location/location").Attributes ["index"].InnerText = indexValue.ToString();
		doc.Save ("Assets/scripts/dialog/" + subject + ".xml");
		Debug.Log (indexValue + " - current index value in XML");
	}
}
