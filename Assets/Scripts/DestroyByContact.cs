using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion, playerExplosion;

	void OnTriggerEnter(Collider other) {

		if (other.name == "Boundary")
			return;

		Instantiate (explosion, transform.position, transform.rotation);

		if (other.name == "Player")
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
		
		Destroy(other.gameObject);
		Destroy(gameObject);
	}
}
