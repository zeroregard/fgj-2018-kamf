using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace MadLagBots
{
	public class RoboModule : MonoBehaviour {

		public float thrust;
		public float torque;
		public Rigidbody rb;

		public void HandleInput(InputType input, float _lagS) 
		{
			Observable.Timer(TimeSpan.FromSeconds(_lagS)).TakeUntilDestroy(this).Subscribe(_ => {
				switch (input)
				{
					case InputType.Attack:
						Attack(InputType.Attack);
						break;
					case InputType.Forward:
						Accelerate(InputType.Back);
						break;
					case InputType.Back:
						Reverse(InputType.Forward);
						break;
					case InputType.Left:
						Turn(InputType.Left);
						break;
					case InputType.Right:
						Turn(InputType.Right);
						break;
				}
			});

			
		}

		public void Attack (InputType input) 
		{
			print($"Attack! input: {input}");
		}

		public void Turn (InputType input) 
		{
			print($"Turning! input: {input}, thrust: {thrust}");
			int dir = input == InputType.Left ? -1 : 1;
			rb.AddTorque(transform.up * torque * dir);

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
