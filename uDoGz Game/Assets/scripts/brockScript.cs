using UnityEngine;
using System.Collections;

public class brockScript : MonoBehaviour {

	public Sprite[] sprites;
	public float FPS;
	private SpriteRenderer spriteRenderer;
	private Vector3 moveDirection;
	private Vector3 currentPosition;
	public float speed;
	private int dir;
	
	// Use this for initialization
	void Start () 
	{
		spriteRenderer = renderer as SpriteRenderer;
		dir = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
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
		if (transform.position.y > 3) {
			dir = 1;
			return;
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
		if (transform.position.y < -3) {
			dir = 0;
			return;
				}
		int index = (int)(Time.timeSinceLevelLoad * FPS);
		index = index % (sprites.Length - 4);
		moveDirection.Set(0, -1, 0);
		moveDirection.Normalize ();
		Vector3 target = moveDirection*speed + currentPosition;
		transform.position = Vector3.Lerp (currentPosition, target, Time.deltaTime);
		spriteRenderer.sprite = sprites [index];
	}

	void OnCollisionEnter(Collider coll)
	{

		}
}