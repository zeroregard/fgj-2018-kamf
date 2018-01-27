using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboMovement : MonoBehaviour {

	public Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

		rigidBody.AddForce(0, 0, 10.0f);

	}
}
