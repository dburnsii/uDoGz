using UnityEngine;
using System.Collections;

public class playerMovementScript : MonoBehaviour  {

	private Vector3 dest;
	private Vector3 start;
	private Vector3 target;
	public float speed;
	private SpriteRenderer spriteRenderer;
	public Sprite[] sprites;
	private char dir;
	public float moveTime;
	private bool done;
	private KeyCode last;
	public float FPS;
	public float idleTime;
	private bool ubool;
	private bool dbool;
	private bool lbool;
	private bool rbool;
	private bool coll;

	// Use this for initialization
	void Start () {
		coll = false;
		spriteRenderer = renderer as SpriteRenderer;
		start = transform.position;
		done = true;
		moveTime = 0;
		speed = speed * 0.075f;
		ubool = false;
		dbool = false;
		lbool = false;
		rbool = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (done || !coll) {
						if (Input.GetKey (last)) {
								if (last.ToString () == "UpArrow") {
										moveUp ();
										return;
								}
								if (last.ToString () == "DownArrow") {
										moveDown ();
										return;
								}
								if (last.ToString () == "LeftArrow") {
										moveLeft ();
										return;
								}
								if (last.ToString () == "RightArrow") {
										moveRight ();
										return;
								}
						} else if (Input.GetKey (KeyCode.DownArrow)) {
								moveDown ();
								last = KeyCode.DownArrow;
								return;
						} else if (Input.GetKey (KeyCode.LeftArrow)) {
								moveLeft ();
								last = KeyCode.LeftArrow;
								return;
						} else if (Input.GetKey (KeyCode.RightArrow)) {
								moveRight ();
								last = KeyCode.RightArrow;
								return;
						} else if (Input.GetKey (KeyCode.UpArrow)) {
								moveUp ();
								last = KeyCode.UpArrow;
								return;
						} else {
								idleTime = Time.time;
								if (last.ToString () == "UpArrow") {
										spriteRenderer.sprite = sprites [0];
										return;
								}
								if (last.ToString () == "DownArrow") {
										spriteRenderer.sprite = sprites [4];
										return;
								}
								if (last.ToString () == "LeftArrow") {
										spriteRenderer.sprite = sprites [8];
										return;
								}
								if (last.ToString () == "RightArrow") {
										spriteRenderer.sprite = sprites [12];
										return;

								}
						} 
				}
		else 
		{
			if (dir == 'u') 
			{
				moveUp ();
			}
			if (dir == 'd') 
			{
				moveDown ();
			}
			if (dir == 'l')
			{
				moveLeft ();
			}
			if (dir == 'r') 
			{
				moveRight ();
			}
		}
	}

	void moveUp()
	{
		dest.Set (0, 1, 0);
		target = start + dest;
		if (moveTime >= 1) 
		{
			moveTime = 0;
			done = true;
			start = transform.position;
		}
		else 
		{
			int index = (int)(Time.timeSinceLevelLoad * FPS);
			index = index % (sprites.Length - 12);
			spriteRenderer.sprite = sprites[index];
			moveTime += speed;
			transform.position = Vector3.Lerp (start, target, moveTime);
			dir = 'u';
			done = false;
		}
	}

	void moveDown()
	{
		dest.Set (0, -1, 0);
		target = start + dest;
		if (moveTime >= 1) 
		{
			moveTime = 0;
			done = true;
			start = transform.position;
		} 
		else 
		{
			int index = (int)(Time.timeSinceLevelLoad * FPS);
			index = index % (sprites.Length - 12);
			spriteRenderer.sprite = sprites[index + 4];
			moveTime += speed;
			transform.position = Vector3.Lerp (start, target, moveTime);
			dir = 'd';
			done = false;
		}
	}

	void moveLeft()
	{
		dest.Set (-1, 0, 0);
		target = start + dest;
		if (moveTime >= 1) 
		{
			moveTime = 0;
			done = true;
			start = transform.position;
		} 
		else 
		{
			int index = (int)(Time.timeSinceLevelLoad * FPS);
			index = index % (sprites.Length - 12);
			spriteRenderer.sprite = sprites[index + 8];
			moveTime += speed;
			transform.position = Vector3.Lerp (start, target, moveTime);
			dir = 'l';
			done = false;
		}
	}

	void moveRight()
	{
		dest.Set (1, 0, 0);
		target = start + dest;
		if (moveTime >= 1) 
		{
			moveTime = 0;
			done = true;
			start = transform.position;
		} 
		else 
		{
			int index = (int)(Time.timeSinceLevelLoad * FPS);
			index = index % (sprites.Length - 12);
			spriteRenderer.sprite = sprites[index + 12];
			moveTime += speed;
			transform.position = Vector3.Lerp (start, target, moveTime);
			dir = 'r';
			done = false;
		}
	}
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Building") 
		{
			Debug.Log("Hit a building");
			speed = 0;
		}
		Debug.Log (coll.gameObject.tag + " - tag");
	}
	void OnCollisionExit2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Building") 
		{
			Debug.Log("Hit a building");
			speed = 0.075f;
		}
		Debug.Log (coll.gameObject.tag + " - tag");
	}
}
