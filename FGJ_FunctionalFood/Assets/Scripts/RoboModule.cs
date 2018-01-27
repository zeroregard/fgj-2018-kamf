using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadLagBots
{
	public class RoboModule : MonoBehaviour {

		public float thrust;
		public float torque = 5;
		public Rigidbody rb;

		public void Attack (InputType input) 
		{
			print($"Attack! input: {input}");
		}

		public void TurnLeft (InputType input) 
		{
			print($"Turning! input: {input}, thrust: {thrust}");

			rb.AddTorque(transform.up * torque * -1);

		}
		public void TurnRight (InputType input) 
		{
			print($"Turning! input: {input}, thrust: {thrust}");

			rb.AddTorque(transform.up * torque * 1);

		}

		public void Accelerate (InputType input) 
		{
			print($"Accelerating! input: {input}, thrust: {thrust}");

			rb.AddForce(transform.forward * thrust);
		}

		public void Reverse (InputType input) 
		{
			print($"Reversing! input: {input}, thrust: {thrust}");

			rb.AddForce(-transform.forward * thrust);
		}
	}
}
