using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	public static CameraFollow current;	//a reference to our CameraFollow so we can access it statically
	public Transform target;		//target for the camera to follow
	public float xOffset;			//how much x-axis space should be between the camera and target

	public AudioClip flip;
	public AudioClip gameOver;

	void Awake ()
	{
		//if we don't currently have a game control...
		if (current == null)
			//...set this one to be it...
			current = this;
		//...otherwise...
		else if(current != this)
			//...destroy this one because it is a duplicate
			Destroy (gameObject);
	}

	void Update()
	{
		//follow the target on the x-axis only
		transform.position = new Vector3 (target.position.x + xOffset, transform.position.y, transform.position.z);
	}

	public void FlipSound ()
	{
		audio.clip = flip;
		audio.Play ();
	}

	public void GameOverSound ()
	{
		audio.clip = gameOver;
		audio.Play ();
	}
}
