using UnityEngine;
using System.Collections;

public class simpleAIWalkingScript : MonoBehaviour {

	// Use this for initialization
	public int xMax;
	public int xMin;
	public int yMax;
	public int yMin;
	public bool clockwise;
	public int speed;
	private int speedBackup;
	private char direction;
	public float time;
	public downColliderBox downCollBox;
	public upColliderBox upCollBox;
	public leftColliderBox leftCollBox;
	public rightColliderBox rightCollBox;
	public string upCollBoxName;
	public string downCollBoxName;
	public string leftCollBoxName;
	public string rightCollBoxName;
	private float beforeStopTime;
	private Animator animator; //0 = down, 1 = up, 2 = left, 3 = right
	private playerMovementScript player;
	private Renderer render;

	void Start () {
		player = (playerMovementScript)FindObjectOfType (typeof(playerMovementScript));
		speedBackup = speed;
		render = GetComponent<Renderer> ();
		direction = 'd';
		animator = (Animator) GetComponent<Animator>();
		time = Time.time;
		upColliderBox[] upBoxes = (upColliderBox[]) FindObjectsOfType (typeof(upColliderBox));
		foreach (upColliderBox box in upBoxes) 
		{
			if(box.name == upCollBoxName)
			{
				upCollBox = box;
			}
		}
		downColliderBox[] downBoxes = (downColliderBox[]) FindObjectsOfType (typeof(downColliderBox));
		foreach (downColliderBox box in downBoxes) 
		{
			if(box.name == downCollBoxName)
			{
				downCollBox = box;
				//Debug.Log ("Found down collider box for AI");
			}
		}
		leftColliderBox[] leftBoxes = (leftColliderBox[]) FindObjectsOfType (typeof(leftColliderBox));
		foreach (leftColliderBox box in leftBoxes) 
		{
			if(box.name == leftCollBoxName)
			{
				leftCollBox = box;
			}
		}
		rightColliderBox[] rightBoxes = (rightColliderBox[]) FindObjectsOfType (typeof(rightColliderBox));
		foreach (rightColliderBox box in rightBoxes) 
		{
			if(box.name == rightCollBoxName)
			{
				rightCollBox = box;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		float cameraX = Camera.main.transform.position.x;
		float cameraY = Camera.main.transform.position.y;
		if (player.transform.position.y < transform.position.y) //Changing render layer 
		{
			render.sortingLayerName = "CharactersBehind";
		}
		else 
		{
			render.sortingLayerName = "CharactersAhead";
		}
		if ((cameraX > transform.position.x + 10 || cameraX < transform.position.x - 10) && (cameraY > transform.position.y + 10 || cameraY < transform.position.y - 10)) 
		{
			return;
		}
		if (direction == 'd' && transform.position.y > yMin) 
		{
			moveDown ();
		} 
		else if (direction == 'd' && transform.position.y <= yMin) 
		{
			if(clockwise)
			{
				transform.position = new Vector3((float) xMax, (float) yMin, 0);
				direction = 'l';
				animator.SetInteger("directionCode", 2);
				time = Time.time;
				beforeStopTime = Time.time - time;
			}
			else
			{
				transform.position = new Vector3((float) xMin, (float) yMin, 0);
				direction = 'r';
				animator.SetInteger("directionCode", 3);
				time = Time.time;
				beforeStopTime = Time.time - time;
			}
		}
		if (direction == 'u' && transform.position.y < yMax) 
		{
			moveUp ();
		} 
		else if (direction == 'u' && transform.position.y >= yMax) 
		{
			if(clockwise)
			{
				transform.position = new Vector3((float) xMin, (float) yMax, 0);
				direction = 'r';
				animator.SetInteger("directionCode", 3);
				time = Time.time;
				beforeStopTime = Time.time - time;
			}
			else
			{
				transform.position = new Vector3((float) xMax, (float) yMax, 0);
				direction = 'l';
				animator.SetInteger("directionCode", 2);
				time = Time.time;
				beforeStopTime = Time.time - time;
			}
		}
		if (direction == 'l' && transform.position.x > xMin) 
		{
			moveLeft ();
		} 
		else if (direction == 'l' && transform.position.x <= xMin) 
		{
			if(clockwise)
			{
				transform.position = new Vector3((float) xMin, (float) yMin, 0);
				direction = 'u';
				animator.SetInteger("directionCode", 1);
				//beforeStopTime = Time.time;
				time = Time.time;
				beforeStopTime = Time.time - time;
			}
			else
			{
				transform.position = new Vector3((float) xMin, (float) yMax, 0);
				direction = 'd';
				animator.SetInteger("directionCode", 0);
				//beforeStopTime = Time.time;
				time = Time.time;
				beforeStopTime = Time.time - time;
			}
		}
		if (direction == 'r' && transform.position.x < xMax) 
		{
			moveRight ();
		} 
		else if (direction == 'r' && transform.position.x >= xMax) 
		{
			if(clockwise)
			{
				transform.position = new Vector3((float) xMax, (float) yMax, 0);
				direction = 'd';
				animator.SetInteger("directionCode", 0);
				time = Time.time;
				beforeStopTime = Time.time - time;
			}
			else
			{
				transform.position = new Vector3((float) xMax, (float) yMin, 0);
				direction = 'u';
				animator.SetInteger("directionCode", 1);
				time = Time.time;
				beforeStopTime = Time.time - time;
			}
		}
		
	}

	void moveDown()
	{
		if (!downCollBox.collision) 
		{
			transform.position = Vector3.Lerp(new Vector3(transform.position.x, yMax, 0), new Vector3(transform.position.x, yMin, 0), ((Time.time - time) * speed)/Mathf.Abs(yMax-yMin));
			animator.SetBool("stop", false);
			beforeStopTime = Time.time - time;
		} 
		else 
		{
			animator.SetBool("stop", true);
			//Debug.Log ("Stopping");
			time = Time.time - beforeStopTime;
		}
	}

	void moveUp()
	{
		if (!upCollBox.collision) 
		{
			transform.position = Vector3.Lerp(new Vector3(transform.position.x, yMin, 0), new Vector3(transform.position.x, yMax, 0), ((Time.time - time) * speed)/Mathf.Abs(yMax-yMin));
			animator.SetBool("stop", false);
			beforeStopTime = Time.time - time;
		} 
		else 
		{
			animator.SetBool("stop", true);
			//Debug.Log ("Stopping");
			time = Time.time - beforeStopTime;
		}
	}

	void moveLeft()
	{
		if (!leftCollBox.collision) 
		{
			transform.position = Vector3.Lerp(new Vector3(xMax, transform.position.y, 0), new Vector3(xMin, transform.position.y, 0), ((Time.time - time) * speed)/Mathf.Abs(xMax-xMin));
			animator.SetBool("stop", false);
			beforeStopTime = Time.time - time;
		} 
		else 
		{
			animator.SetBool("stop", true);
			//Debug.Log ("Stopping");
			time = Time.time - beforeStopTime;
		}
	}

	void moveRight()
	{
		if (!rightCollBox.collision) 
		{
			transform.position = Vector3.Lerp(new Vector3(xMin, transform.position.y, 0), new Vector3(xMax, transform.position.y, 0), ((Time.time - time) * speed)/Mathf.Abs(xMax-xMin));
			animator.SetBool("stop", false);
			beforeStopTime = Time.time - time;
		} 
		else 
		{
			animator.SetBool("stop", true);
			//Debug.Log ("Stopping");
			time = Time.time - beforeStopTime;
		}
	}
}
