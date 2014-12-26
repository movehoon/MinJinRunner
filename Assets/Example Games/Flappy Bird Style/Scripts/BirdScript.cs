using UnityEngine;
using System.Collections;

public class BirdScript : MonoBehaviour 
{
	public static BirdScript current;	//a reference to our bird so we can access it statically
	public float upForce;			//upward force of the "flap"
	public float forwardSpeed;		//forward movement speed
	public bool isDead = false;		//has the player collided with a wall?

	public Sprite[] sprites;
	public AudioClip[] clips;
	public AudioClip flipAudio;

	bool flap = false;				//has the player triggered a "flap"?

	void Start()
	{
		//if we don't currently have a game control...
		if (current == null)
			//...set this one to be it...
			current = this;
		//...otherwise...
		else if(current != this)
			//...destroy this one because it is a duplicate
			Destroy (gameObject);

		//set the bird moving forward
		rigidbody2D.velocity = new Vector2 (forwardSpeed, 0);
	}

	void Update()
	{
		//don't allow control if the bird has died
		if (isDead)
			return;
		//look for input to trigger a "flap"
		if (Input.anyKeyDown) {
			flap = true;
			int index = Random.Range(0, sprites.Length);
			SpriteRenderer sr = GetComponentInChildren<SpriteRenderer> ();
			sr.sprite = sprites[index];
		}
	}

	void FixedUpdate()
	{
		//if a "flap" is triggered...
		if (flap) 
		{
			flap = false;

			//...zero out the birds current y velocity before...
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
			//..giving the bird some upward force
			rigidbody2D.AddForce(new Vector2(0, upForce));

			CameraFollow.current.FlipSound ();
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		//if the bird collides with something set it to dead...
		isDead = true;
		//...and tell the game control about it
		GameControlScript.current.BirdDied ();

		audio.Stop ();
	}

	public void SoundPass ()
	{
		int index = Random.Range (0, clips.Length);
		audio.clip = clips [index];
		audio.Play ();
	}
}
