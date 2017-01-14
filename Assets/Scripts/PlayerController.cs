using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	public float speed, tilt, fireRate;
	public Boundary boundary;
	public GameObject shot;
	public Transform[] shotSpawns;

	private Rigidbody rigidbody;
	private AudioSource audio;
	private float nextFire;

	void Start (){
		rigidbody = GetComponent<Rigidbody>(); 
		audio = GetComponent<AudioSource> ();
	}

	void Update(){
		if( Input.GetButton("Fire1") && Time.time > nextFire ){
			nextFire = Time.time + fireRate;
			foreach(var shotSpawn in shotSpawns){
				Instantiate (shot, shotSpawn.position, shotSpawn.rotation);	
			}
			audio.Play ();
		}
	}

	void FixedUpdate (){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rigidbody.velocity = movement * speed;
		rigidbody.position = new Vector3 (
			Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
		);
		rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -tilt);
	}
}
