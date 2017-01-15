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
	public SimpleTouchPad touchPad;
	public SimpleTouchAreaButton areaButton;

	private Rigidbody rigidbody;
	private AudioSource audio;
	private float nextFire;
	private Quaternion calibrationQuaternion;

	void Start (){
		rigidbody = GetComponent<Rigidbody>(); 
		audio = GetComponent<AudioSource> ();
		CalibrateAccelerometer ();
	}

	void Update(){
//		if( Input.GetButton("Fire1") && Time.time > nextFire ){
		if (areaButton.CanFire () && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			foreach(var shotSpawn in shotSpawns){
				Instantiate (shot, shotSpawn.position, shotSpawn.rotation);	
			}
			audio.Play ();
		}
	}

	void FixedUpdate (){
//		float moveHorizontal = Input.GetAxis ("Horizontal");
//		float moveVertical = Input.GetAxis ("Vertical");
//		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

//		Vector3 accelerationRaw = Input.acceleration;
//		Vector3 acceleration = FixAcceleration (accelerationRaw);
//		Vector3 movement = new Vector3 (acceleration.x, 0.0f, acceleration.y);

		Vector2 direction = touchPad.GetDirection();
		Vector3 movement = new Vector3 (direction.x, 0.0f, direction.y);

		rigidbody.velocity = movement * speed;
		rigidbody.position = new Vector3 (
			Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
		);
		rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -tilt);
	}

	//Used to calibrate the Iput.acceleration input
	void CalibrateAccelerometer () {
		Vector3 accelerationSnapshot = Input.acceleration;
		Quaternion rotateQuaternion = Quaternion.FromToRotation (new Vector3 (0.0f, 0.0f, -1.0f), accelerationSnapshot);
		calibrationQuaternion = Quaternion.Inverse (rotateQuaternion);
	}

	//Get the 'calibrated' value from the Input
	Vector3 FixAcceleration (Vector3 acceleration) {
		Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
		return fixedAcceleration;
	}

}
