﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;

namespace MadLagBots
{
    public class RoboModule : MonoBehaviour
    {

        public float thrust;
        public float torque;
        public Rigidbody rb;
        public WeaponBase Weapon;
        public ParticleSystem sparkEmitter;

        private InputModule _inputModule;
        public InputModule InputModule => GetComponent<InputModule>();
        public HealthModule HealthModule => GetComponent<HealthModule>();

		public List<MeshRenderer> MeshesToColor;

		public void Start() {
			// move the center of mass a biiiit down
			// this very reliably prevents the thing from flipping over
			// sometimes the thing might behave weirdly
			// if it's too weird make it closer to 0, or just 0
			rb.centerOfMass = new Vector3(0, -0.07f, 0);
		}

        public void Update()
        {
            if (rb.position.y < -10)
            {
                HealthModule.Die();
            }
        }

		void FixUpsideDown()
		{
			var up = transform.up;
			if(up.y < 0)
			{
				
			}
		}

		public void SetColor(Color color)
		{
			foreach(var m in MeshesToColor)
			{
				m.material.color = color;
			}
		}

        public void DeathAnimation() // amazing tortouise animation
        {
            rb.isKinematic = true;
            transform
                .DORotate(new Vector3(180,180,0), 1.5f, RotateMode.Fast)
                .SetEase(Ease.OutQuad);
        }

        private float _maxVelocity = 5;

        // Particles
        ParticleSystem exhaust;

        public void HandleInput(InputType input, float lagSeconds)
        {
            Observable.Timer(TimeSpan.FromSeconds(lagSeconds)).TakeUntilDestroy(this).Subscribe(_ =>
            {
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

        public void Attack(InputType input)
        {
            Weapon.TryAttack();

        }

        public void Turn(InputType input)
        {
            int dir = input == InputType.Left ? -1 : 1;
            rb.AddTorque(transform.up * torque * dir);

        }

        void Forward(float t)
        {
            var planeNormal = Vector3.up;
            var forwardInPlane = Vector3.ProjectOnPlane(transform.forward, planeNormal);
            var forwardNormalized = Vector3.Normalize(forwardInPlane);
            var thrustNormalized = t * rb.mass;

            rb.AddForce(forwardNormalized * thrustNormalized);
            if (rb.velocity.magnitude > _maxVelocity)
            {
                rb.velocity = rb.velocity * 0.95f;
            }
        }

        public void Accelerate(InputType input)
        {
            if (rb.velocity.magnitude < _maxVelocity)
            {
                Forward(thrust);
            }
        }

        public void Reverse(InputType input)
        {
            var planeNormal = Vector3.up;
            var forwardInPlane = Vector3.ProjectOnPlane(transform.forward, planeNormal);
            var forwardNormalized = Vector3.Normalize(forwardInPlane);
            var thrustNormalized = thrust * rb.mass * 0.5f;


            rb.AddForce(-forwardNormalized * thrustNormalized);
        }

        public void ReduceMass()
        {
            rb.mass = rb.mass * (2/3f);
            InputModule.AdjustLag(rb.mass);
            sparkEmitter.Play();
        }
    }
}
