using UnityEngine;
using System.Collections;

public class playerMovementScript : MonoBehaviour
{

	private Vector3 dest;
	private Vector3 start;
	private Vector3 target;
	public float speed;
	private SpriteRenderer spriteRenderer;
	public Sprite[] sprites;
	public char dir;
	public string dirString;
	public string lastString;
	private float moveTime;
	public bool done;
	private KeyCode last;
	public float FPS;
	private float idleTime;
	private float time;
	private float sysTime;
	private float lastMoveTime;
	private int eyesClosed;
	private float eyesClosedTime;
	public upPlayerColliderBox upCollBox;
	public downPlayerColliderBox downCollBox;
	public leftPlayerColliderBox leftCollBox;
	public rightPlayerColliderBox rightCollBox;
	public bool upBoxBool;
	public bool upDoorBool;
	public bool downDoorBool;
	public bool downBoxBool;
	public bool leftBoxBool;
	public bool rightBoxBool;
	private bool stopStatus;
	private char collisionDirection;
	private bool uObstacle;
	private bool dObstacle;
	private bool lObstacle;
	private bool rObstacle;
	public bool enabled;
	private dBoxScript[] dialogBox;
	//public leftPlayerColliderBox leftBox;

	void Start ()
	{
		//coll = false;
		enabled = true;
		spriteRenderer = renderer as SpriteRenderer;
		start = transform.position;
		done = true;
		moveTime = 0;
		//ubool = false;
		//dbool = false;
		//lbool = false;
		//rbool = false;
		eyesClosed = 0;
		eyesClosedTime = 0;
		upCollBox = ( upPlayerColliderBox )FindObjectOfType ( typeof( upPlayerColliderBox ) );
		downCollBox = ( downPlayerColliderBox )FindObjectOfType ( typeof( downPlayerColliderBox ) );
		leftCollBox = ( leftPlayerColliderBox )FindObjectOfType ( typeof( leftPlayerColliderBox ) );
		rightCollBox = ( rightPlayerColliderBox )FindObjectOfType ( typeof( rightPlayerColliderBox ) );
		dialogBox = (dBoxScript[])FindObjectsOfType (typeof(dBoxScript));

		//leftBoxBool = (bool)GameObject.Find ("leftPlayerCollBox").GetComponent ("collision");
		//leftBox = (leftPlayerColliderBox) GameObject.Find ("leftPlayerCollBox").GetComponent<leftPlayerColliderBox>();
	}

	// Update is called once per frame
	void Update ()
	{
		if ( !enabled ) 
		{
			if(Input.GetKey (KeyCode.Space))
			{
				Debug.Log ("Escaping");

			}
			return;
		}
		//GUI.Label (new Rect (0, 0, 10, 2), "Test Text");
		upBoxBool = upCollBox.collision;
		upDoorBool = upCollBox.door;
		downBoxBool = downCollBox.collision;
		downDoorBool = downCollBox.door;
		leftBoxBool = leftCollBox.collision;
		rightBoxBool = rightCollBox.collision;
		dirString = dir.ToString ();
		lastString = last.ToString ();
		sysTime = Time.time;
		if (stopStatus) 
		{
			stopStatus = false;
		}
		if ( done )
		{
			time = Time.time;
			idleTime = sysTime - lastMoveTime;
			if ( Input.GetKey ( last ) )
			{
				if ( dir == 'u' && upDoorBool )
				{
					//enterBuilding();
				}
				if ( dir == 'u' && !upBoxBool )
				{
					moveUp ();
					return;
				}
				if ( dir == 'd' && !downBoxBool )
				{
					moveDown ();
					return;
				}
				if ( dir == 'l' && !leftBoxBool )
				{
					moveLeft ();
					return;
				}
				if ( dir == 'r' && !rightBoxBool )
				{
					moveRight ();
					return;
				}
			}
			else if( Input.GetKey (KeyCode.Space))
			{
				if(dir == 'r' && rightCollBox.talk)
				{
					for(int i = 0; i < dialogBox.Length; i++)
					{
						dialogBox[i].visible = true;
						dialogBox[i].subject = rightCollBox.building;
					}
					Debug.Log ("Enabling dialog boxes");
				}
			}
			else if ( Input.GetKey ( KeyCode.DownArrow ))
			{
				last = KeyCode.DownArrow;
				if(!downBoxBool)
				{
					moveDown ();
					return;
				}
				else
				{
					spriteRenderer.sprite = sprites[4];
					dir = 'd';
					done = true;
				}
			}
			else if ( Input.GetKey ( KeyCode.LeftArrow ) )
			{
				last = KeyCode.LeftArrow;
				if(!leftBoxBool)
				{
					moveLeft ();
					return;
				}
				else
				{
					spriteRenderer.sprite = sprites[8];
					dir = 'l';
					done = true;
				}
			} 
			else if ( Input.GetKey ( KeyCode.RightArrow ))
			{
				last = KeyCode.RightArrow;
				if(!rightBoxBool)
				{
					moveRight ();
					return;
				}
				else
				{
					spriteRenderer.sprite = sprites[12];
					dir = 'r';
					done = true;
				}
			}
			else if ( Input.GetKey ( KeyCode.UpArrow ) )
			{
				last = KeyCode.UpArrow;
				if(upDoorBool)
				{
					//enterBuilding();
				}
				if(!upBoxBool )
				{
					moveUp ();
					return;
				}
				else
				{
					spriteRenderer.sprite = sprites[0];
					dir = 'u';
					done = true;
				}
			}
			else if (Input.GetKey (KeyCode.Space))
			{
				if(dir == 'u' && upCollBox.talk)
				{
					//dialogScript.;
				}
			}
			else
			{
				if ( idleTime < 1 )
				{
					if ( dir == 'u' )
					{
						spriteRenderer.sprite = sprites [ 0 ];
						return;
					}
					if ( dir == 'd' )
					{
						spriteRenderer.sprite = sprites [ 4 ];
						return;
					}
					if ( dir == 'l' )
					{
						spriteRenderer.sprite = sprites [ 8 ];
						return;
					}
					if ( dir == 'r' )
					{
						spriteRenderer.sprite = sprites [ 12 ];
						return;

					}
				}
				if ( idleTime > 2 )
				{
					blinkAnimation ();
				}
			}
		}
		else
		{
			if ( dir == 'u' )
			{
				moveUp ();
			}
			if ( dir == 'd' )
			{
				moveDown ();
			}
			if ( dir == 'l' )
			{
				moveLeft ();
			}
			if ( dir == 'r' )
			{
				moveRight ();
			}
			lastMoveTime = Time.time;
		}
	}

	void moveUp ()
	{
		if (uObstacle) 
		{
			return;
		}
		dest.Set ( 0, 1, 0 );
		target = start + dest;
		if ( moveTime >= 1 )
		{
			moveTime = 0;
			done = true;
			start = transform.position;
			dObstacle = false;
			lObstacle = false;
			rObstacle = false;
		}
		else
		{
			int index = ( int )( Time.timeSinceLevelLoad * FPS );
			index = index % ( sprites.Length - 16 );
			spriteRenderer.sprite = sprites [ index ];
			moveTime = ( sysTime - time ) * speed;
			transform.position = Vector3.Lerp ( start, target, moveTime );
			dir = 'u';
			done = false;
		}

	}

	void moveDown ()
	{
		if (dObstacle) 
		{
			return;
		}
		dest.Set ( 0, -1, 0 );
		target = start + dest;
		if ( moveTime >= 1 )
		{
			moveTime = 0;
			done = true;
			start = transform.position;
			uObstacle = false;
			lObstacle = false;
			rObstacle = false;
		}
		else
		{
			int index = ( int )( Time.timeSinceLevelLoad * FPS );
			index = index % ( sprites.Length - 16 );
			spriteRenderer.sprite = sprites [ index + 4 ];
			moveTime = ( sysTime - time ) * speed;
			transform.position = Vector3.Lerp ( start, target, moveTime );
			dir = 'd';
			done = false;
		}
	}

	void moveLeft ()
	{
		if (lObstacle) 
		{
			return;
		}
		dest.Set ( -1, 0, 0 );
		target = start + dest;
		if ( moveTime >= 1 )
		{
			moveTime = 0;
			done = true;
			start = transform.position;
			uObstacle = false;
			dObstacle = false;
			rObstacle = false;
		}
		else
		{
			int index = ( int )( Time.timeSinceLevelLoad * FPS );
			index = index % ( sprites.Length - 16 );
			spriteRenderer.sprite = sprites [ index + 8 ];
			moveTime = ( sysTime - time ) * speed;
			transform.position = Vector3.Lerp ( start, target, moveTime );
			dir = 'l';
			done = false;
		}
	}

	void moveRight ()
	{
		if (rObstacle) 
		{
			return;
		}
		dest.Set ( 1, 0, 0 );
		target = start + dest;
		if ( moveTime >= 1 )
		{
			moveTime = 0;
			done = true;
			start = transform.position;
			uObstacle = false;
			dObstacle = false;
			lObstacle = false;
		}
		else
		{
			int index = ( int )( Time.timeSinceLevelLoad * FPS );
			index = index % ( sprites.Length - 16 );
			spriteRenderer.sprite = sprites [ index + 12 ];
			moveTime = ( sysTime - time ) * speed;
			transform.position = Vector3.Lerp ( start, target, moveTime );
			dir = 'r';
			done = false;
		}
	}

	void blinkAnimation ()
	{
		int random = Random.Range ( 0, 100 );
		//Debug.Log ( random );
		if ( Time.time - eyesClosedTime > 0.25f && eyesClosedTime != 0 )
		{
			if ( dir == 'd' )
			{
				spriteRenderer.sprite = sprites [ 4 ];
			}
			if ( dir == 'l' )
			{
				spriteRenderer.sprite = sprites [ 8 ];
			}
			if ( dir == 'r' )
			{
				spriteRenderer.sprite = sprites [ 12 ];
			}
			eyesClosedTime = 0;
			Debug.Log ( "Eyes are now open" );
		}
		else
		if ( random < 1 && eyesClosedTime == 0 )
		{
			Debug.Log ( "Eyes are now closed" );
			if ( dir == 'd' )
			{
				spriteRenderer.sprite = sprites [ 17 ];
			}
			else
			if ( dir == 'l' )
			{
				spriteRenderer.sprite = sprites [ 18 ];
			}
			else
			if ( dir == 'r' )
			{
				spriteRenderer.sprite = sprites [ 19 ];
			}
			eyesClosedTime = Time.time;
		}
	}

	void OnTriggerEnter2D ( Collider2D coll )
	{
		if ( coll.gameObject.tag == "Building" )
		{
			//Debug.Log ("Hit a building " + Time.time);
			stopChar ();
		}
		//Debug.Log (coll.gameObject.tag + " - tag");
	}

	void stopChar ()
	{
				Debug.Log ("Collision detected, running stopChar function with direction '" + dir + "'.");
				collisionDirection = dir;
		
				if (dir == 'u') {
						transform.position = start;
						uObstacle = true;
						done = true;
						Debug.Log ("Returning to position " + start.ToString ());
						return;
				}
				if (dir == 'd') {
						transform.position = start;
						dObstacle = true;
						done = true;
						Debug.Log ("Returning to position " + start.ToString ());
						return;
				}
				if (dir == 'l') {
						collisionDirection = 'l';
						transform.position = start;
						lObstacle = true;
						done = true;
						Debug.Log ("Returning to position " + start.ToString ());
						return;
				}
				if (dir == 'r') {
						transform.position = start;
						rObstacle = true;
						done = true;
						Debug.Log ("Returning to position " + start.ToString ());
						return;
				}
		

				collisionDirection = dir;
				sysTime = Time.time;
				if (done) {
						time = Time.time;
						idleTime = sysTime - lastMoveTime;
						if (Input.GetKey (KeyCode.DownArrow) && collisionDirection != 'd') {
								moveDown ();
								last = KeyCode.DownArrow;
								stopStatus = false;
								collisionDirection = 'n';
								return;
						} else if (Input.GetKey (KeyCode.LeftArrow) && collisionDirection != 'l') {
								moveLeft ();
								last = KeyCode.LeftArrow;
								stopStatus = false;
								collisionDirection = 'n';
								return;
						} else if (Input.GetKey (KeyCode.RightArrow) && collisionDirection != 'r') {
								moveRight ();
								last = KeyCode.RightArrow;
								Debug.Log ("Collision detected with direction " + collisionDirection);
								stopStatus = false;
								collisionDirection = 'n';
								return;
						} else if (Input.GetKey (KeyCode.UpArrow) && collisionDirection != 'u') {
								moveUp ();
								last = KeyCode.UpArrow;
								stopStatus = false;
								collisionDirection = 'n';
								return;
						} else {
								if (idleTime < 1) {
										if (dir == 'u') {
												spriteRenderer.sprite = sprites [0];
												return;
										}
										if (dir == 'd') {
												spriteRenderer.sprite = sprites [4];
												return;
										}
										if (dir == 'l') {
												spriteRenderer.sprite = sprites [8];
												return;
										}
										if (dir == 'r') {
												spriteRenderer.sprite = sprites [12];
												return;
										}
								}
								if (idleTime > 2) {
										blinkAnimation ();
								}
						}
				} else {
						transform.position = start;
						target = start;

						if (dir == 'u') {
								transform.position = start;
								target = start;
						}
						if (dir == 'd') {
								transform.position = start;
								target = start;
						}
						if (dir == 'l') {
								transform.position = start;
								target = start;
						}
						if (dir == 'r') {
								transform.position = start;
								target = start;
						}

						lastMoveTime = Time.time;
				}
		}

}
