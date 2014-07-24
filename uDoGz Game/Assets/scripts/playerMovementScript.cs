﻿using UnityEngine;
using System.Collections;

public class playerMovementScript : MonoBehaviour
{

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
	public float time;
	public float sysTime;
	public float lastMoveTime;
	private int eyesClosed;
	public float eyesClosedTime;
	public bool stopStatus;
	public char collisionDirection;

	// Use this for initialization
	void Start ()
	{
		coll = false;
		spriteRenderer = renderer as SpriteRenderer;
		start = transform.position;
		done = true;
		moveTime = 0;
		ubool = false;
		dbool = false;
		lbool = false;
		rbool = false;
		eyesClosed = 0;
		eyesClosedTime = 0;
		stopStatus = false;
	}

	// Update is called once per frame
	void Update ()
	{
		if (stopStatus) 
		{
			stopStatus = false;
			collisionDirection = 'n';
			return;
		}
		sysTime = Time.time;
		if (done) {
			time = Time.time;
			idleTime = sysTime - lastMoveTime;
			if (Input.GetKey (last)) {
				if (dir == 'u') {
					moveUp ();
					return;
				}
				if (dir == 'd') {
					moveDown ();
					return;
				}
				if (dir == 'l') {
					moveLeft ();
					return;
				}
				if (dir == 'r') {
					moveRight ();
					return;
				}
			} else
			if (Input.GetKey (KeyCode.DownArrow)) {
				moveDown ();
				last = KeyCode.DownArrow;
				return;
			} else
			if (Input.GetKey (KeyCode.LeftArrow)) {
				moveLeft ();
				last = KeyCode.LeftArrow;
				return;
			} else
			if (Input.GetKey (KeyCode.RightArrow)) {
				moveRight ();
				last = KeyCode.RightArrow;
				return;
			} else
			if (Input.GetKey (KeyCode.UpArrow)) {
				moveUp ();
				last = KeyCode.UpArrow;
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
			if (dir == 'u') {
				moveUp ();
			}
			if (dir == 'd') {
				moveDown ();
			}
			if (dir == 'l') {
				moveLeft ();
			}
			if (dir == 'r') {
				moveRight ();
			}
			lastMoveTime = Time.time;
		}
	}

	void moveUp ()
	{
		if (collisionDirection == 'u') 
		{
			return;
		}
		dest.Set (0, 1, 0);
		target = start + dest;
		if (moveTime >= 1) {
			moveTime = 0;
			done = true;
			start = transform.position;
		} else {
			int index = (int)(Time.timeSinceLevelLoad * FPS);
			index = index % (sprites.Length - 16);
			spriteRenderer.sprite = sprites [index];
			moveTime = (sysTime - time) * speed;
			transform.position = Vector3.Lerp (start, target, moveTime);
			dir = 'u';
			done = false;
		}
	}

	void moveDown ()
	{
		if (collisionDirection == 'd') 
		{
			return;
		}
		dest.Set (0, -1, 0);
		target = start + dest;
		if (moveTime >= 1) {
			moveTime = 0;
			done = true;
			start = transform.position;
		} else {
			int index = (int)(Time.timeSinceLevelLoad * FPS);
			index = index % (sprites.Length - 16);
			spriteRenderer.sprite = sprites [index + 4];
			moveTime = (sysTime - time) * speed;
			transform.position = Vector3.Lerp (start, target, moveTime);
			dir = 'd';
			done = false;
		}
	}

	void moveLeft ()
	{
		if (collisionDirection == 'l') 
		{
			return;
		}
		dest.Set (-1, 0, 0);
		target = start + dest;
		if (moveTime >= 1) {
			moveTime = 0;
			done = true;
			start = transform.position;
		} else {
			int index = (int)(Time.timeSinceLevelLoad * FPS);
			index = index % (sprites.Length - 16);
			spriteRenderer.sprite = sprites [index + 8];
			moveTime = (sysTime - time) * speed;
			transform.position = Vector3.Lerp (start, target, moveTime);
			dir = 'l';
			done = false;
		}
	}

	void moveRight ()
	{
		if (collisionDirection == 'r') 
		{
			return;
		}
		dest.Set (1, 0, 0);
		target = start + dest;
		if (moveTime >= 1) {
			moveTime = 0;
			done = true;
			start = transform.position;
		} else {
			int index = (int)(Time.timeSinceLevelLoad * FPS);
			index = index % (sprites.Length - 16);
			spriteRenderer.sprite = sprites [index + 12];
			moveTime = (sysTime - time) * speed;
			transform.position = Vector3.Lerp (start, target, moveTime);
			dir = 'r';
			done = false;
		}
	}

	void blinkAnimation ()
	{
		int random = Random.Range (0, 100);
		//Debug.Log ( random );
		if (Time.time - eyesClosedTime > 0.25f && eyesClosedTime != 0) {
			if (dir == 'd')
				spriteRenderer.sprite = sprites [4];
			if (dir == 'l')
				spriteRenderer.sprite = sprites [8];
			if (dir == 'r')
				spriteRenderer.sprite = sprites [12];
			eyesClosedTime = 0;
			Debug.Log ("Eyes are now open");
		} else if (random < 1 && eyesClosedTime == 0) {
			Debug.Log ("Eyes are now closed");
			if (dir == 'd') {
				spriteRenderer.sprite = sprites [17];
			} else if (dir == 'l') {
				spriteRenderer.sprite = sprites [18];
			} else if (dir == 'r') {
				spriteRenderer.sprite = sprites [19];
			}
			eyesClosedTime = Time.time;
		}
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "Building") {
			//Debug.Log ("Hit a building " + Time.time);
			stopChar();
			stopStatus = true;
		}
		//Debug.Log (coll.gameObject.tag + " - tag");
	}

	void stopChar()
	{
		Debug.Log ("Collision detected, running stopChar function with direction '" + dir + "'.");
		collisionDirection = dir;
		
		if (dir == 'u')
		{
			transform.position = start;
			done = true;
			Debug.Log ("Returning to position " + start.ToString());
			return;
		}
		if (dir == 'd') 
		{
			transform.position = start;
			done = true;
			Debug.Log ("Returning to position " + start.ToString());
			return;
		}
		if (dir == 'l') 
		{
			transform.position = start;
			done = true;
			Debug.Log ("Returning to position " + start.ToString());
			return;
		}
		if (dir == 'r') 
		{
			transform.position = start;
			done = true;
			Debug.Log ("Returning to position " + start.ToString());
			return;
		}
		
		/*
		collisionDirection = dir;
		sysTime = Time.time;
		if (done) 
		{
			time = Time.time;
			idleTime = sysTime - lastMoveTime;
			if (Input.GetKey (KeyCode.DownArrow) && collisionDirection != 'd') 
			{
				moveDown ();
				last = KeyCode.DownArrow;
				stopStatus = false;
				collisionDirection = 'n';
				return;
			} 
			else if (Input.GetKey (KeyCode.LeftArrow) && collisionDirection != 'l') 
			{
				moveLeft ();
				last = KeyCode.LeftArrow;
				stopStatus = false;
				collisionDirection = 'n';
				return;
			} 
			else if (Input.GetKey (KeyCode.RightArrow) && collisionDirection != 'r') 
			{
				moveRight ();
				last = KeyCode.RightArrow;
				Debug.Log ("Collision detected with direction " + collisionDirection);
				stopStatus = false;
				collisionDirection = 'n';
				return;
			} 
			else if (Input.GetKey (KeyCode.UpArrow) && collisionDirection != 'u') 
			{
				moveUp ();
				last = KeyCode.UpArrow;
				stopStatus = false;
				collisionDirection = 'n';
				return;
			} 
			else 
			{
				if (idleTime < 1) 
				{
					if (dir == 'u')
					{
						spriteRenderer.sprite = sprites [0];
						return;
					}
					if (dir == 'd') 
					{
						spriteRenderer.sprite = sprites [4];
						return;
					}
					if (dir == 'l') 
					{
						spriteRenderer.sprite = sprites [8];
						return;
					}
					if (dir == 'r') 
					{
						spriteRenderer.sprite = sprites [12];
						return;
					}
				}
				if (idleTime > 2) 
				{
					blinkAnimation ();
				}
			}
		} 
		else 
		{
			transform.position = start;
			target = start;

			if (dir == 'u') 
			{
				transform.position = start;
				target = start;
			}
			if (dir == 'd') 
			{
				transform.position = start;
				target = start;
			}
			if (dir == 'l') 
			{
				transform.position = start;
				target = start;
			}
			if (dir == 'r') 
			{
				transform.position = start;
				target = start;
			}

			lastMoveTime = Time.time;
		}
	*/
	}
}
