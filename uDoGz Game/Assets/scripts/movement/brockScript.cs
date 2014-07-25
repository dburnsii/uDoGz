using UnityEngine;
using System.Collections;

public class brockScript : MonoBehaviour {

	public Sprite[] sprites;
	public float FPS;
	private SpriteRenderer spriteRenderer;
	private Vector3 moveDirection;
	private Vector3 currentPosition;
	public float speed;
	private float speedBackup;
	private int dir;
	public int yMax;
	public int yMin;
	private upColliderBox upColl;
	private downColliderBox downColl;
	public bool upObstacle;
	public bool downObstacle;
	
	// Use this for initialization
	void Start () 
	{
		speedBackup = speed;
		upObstacle = false;
		downObstacle = false;
		spriteRenderer = renderer as SpriteRenderer;
		upColl = (upColliderBox) FindObjectOfType (typeof(upColliderBox));
		downColl = (downColliderBox)FindObjectOfType (typeof(downColliderBox));
		dir = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		upObstacle = upColl.collision;
		downObstacle = downColl.collision;
		currentPosition = transform.position;
		if (dir == 0) 
		{
			walkUp ();
		} 
		else 
		{
			walkDown ();
		}
		//int index = (int)(Time.timeSinceLevelLoad * FPS);
		//index = index % sprites.Length;
		//spriteRenderer.sprite = sprites [index];
	}

	void walkUp()
	{
		if (transform.position.y > yMax) 
		{
			dir = 1;
			return;
		}
		if (upObstacle) 
		{
			spriteRenderer.sprite = sprites [4];
			return;
		} 
		else 
		{
			speed = speedBackup;
		}
		int index = (int)(Time.timeSinceLevelLoad * FPS);
		index = index % (sprites.Length - 4);
		moveDirection.Set(0, 1, 0);
		//transform.position.Set((float) 2, (float) (currentPosition.y + 0.1), (float) 0.0);
		moveDirection.Normalize ();
		Vector3 target = moveDirection*speed + currentPosition;
		transform.position = Vector3.Lerp (currentPosition, target, Time.deltaTime);
		spriteRenderer.sprite = sprites [index+4];
	}

	void walkDown()
	{
		if (transform.position.y < yMin) {
			dir = 0;
			return;
				}
		if (downObstacle) {
			spriteRenderer.sprite = sprites [0];
			return;
		}
		else 
		{
			speed = speedBackup;
		}
		int index = (int)(Time.timeSinceLevelLoad * FPS);
		index = index % (sprites.Length - 4);
		moveDirection.Set(0, -1, 0);
		moveDirection.Normalize ();
		Vector3 target = moveDirection*speed + currentPosition;
		transform.position = Vector3.Lerp (currentPosition, target, Time.deltaTime);
		spriteRenderer.sprite = sprites [index];
	}

	void OnCollisionEnter(Collision coll)
	{

		}
}