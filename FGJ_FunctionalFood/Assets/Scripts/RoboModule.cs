using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadLagBots
{
	public class RoboModule : MonoBehaviour {

		public float thrust;
		public Rigidbody rb;

		private void Attack (InputType input) 
		{
			print("Attack!", input);
		}

		private void Turn (InputType input) 
		{
			print("Turning!", input);
		}

		private void Accelerate (InputType input) 
		{
			print("Accelerating!", input);
			// rb.AddForce(transform.forward * thrust);
		}
	}
}
