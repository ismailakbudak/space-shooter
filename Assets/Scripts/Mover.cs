using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

	public float speed;

	private Rigidbody rigidbody;

	void Start () {
		rigidbody = GetComponent<Rigidbody>(); 
		rigidbody.velocity = transform.forward * speed;
	}

}
