using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadLagBots
{
	public class RoboModule : MonoBehaviour {

		public float thrust;
		public Rigidbody rb;

		public void Attack (InputType input) 
		{
			print("Attack!");
			print(input);
		}

		public void Turn (InputType input) 
		{
			print("Turning!");
			print(input);
		}

		public void Accelerate (InputType input) 
		{
			print("Accelerating!");
			print(input);
			// rb.AddForce(transform.forward * thrust);
		}
	}
}
