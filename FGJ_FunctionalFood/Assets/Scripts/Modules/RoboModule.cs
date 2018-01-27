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
		public WeaponBase Weapon;

		private InputModule _inputModule;
		public InputModule InputModule => GetComponent<InputModule>();

		public void HandleInput(InputType input, float lagSeconds) 
		{
			Observable.Timer(TimeSpan.FromSeconds(lagSeconds)).TakeUntilDestroy(this).Subscribe(_ => {
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
			Weapon.TryAttack();
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
			var planeNormal = Vector3.up;
			var forwardInPlane = Vector3.ProjectOnPlane(transform.forward, planeNormal);
			var forwardNormalized = Vector3.Normalize(forwardInPlane);
			var thrustNormalized = thrust * rb.mass;

			print($"Accelerating! input: {input}, thrust: {thrustNormalized}");

			rb.AddForce(forwardNormalized * thrustNormalized);
		}

		public void Reverse (InputType input) 
		{
			var planeNormal = Vector3.up;
			var forwardInPlane = Vector3.ProjectOnPlane(transform.forward, planeNormal);
			var forwardNormalized = Vector3.Normalize(forwardInPlane);
			var thrustNormalized = thrust * rb.mass;

			print($"Reversing! input: {input}, thrust: {thrustNormalized}");

			rb.AddForce(-forwardNormalized * thrustNormalized);
		}

		public void SetMass ()
		{
			rb.mass = rb.mass-0.1f;
		}
	}
}
