using System.Collections;
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

		[Header("Audio")]
		[SerializeField] private AudioClip[] _turnings;
		private int _turningIterator = 0;
		[SerializeField] private AudioClip _move;
		[SerializeField] private AudioClip _chargeAttack;

		[SerializeField] private AudioSource _turnAudio;
		[SerializeField] private AudioSource _moveAudio;
		[SerializeField] private AudioSource _attackAudio;

        public void Start()
        {
            // move the center of mass a biiiit down
            // this very reliably prevents the thing from flipping over
            // sometimes the thing might behave weirdly
            // if it's too weird make it closer to 0, or just 0
            rb.centerOfMass = new Vector3(0, -0.07f, 0);
        }

        public void Update()
        {
            if (rb.position.y < -10 || Mathf.Abs(rb.position.x) > 50 || Mathf.Abs(rb.position.z) > 50)
            {
                HealthModule.Die();
            }
            FixUpsideDown();
        }

        void FixUpsideDown()
        {
            var rotation = transform.rotation.eulerAngles;
            if (rotation.x < 270 && rotation.x > 90)
            {
                if (rotation.x < 180)
                {
                    rotation.x = 90;
                }
                else
                {
                    rotation.x = 270;
                }
            }
            if (rotation.z < 270 && rotation.z > 90)
            {
                if (rotation.z < 180)
                {
                    rotation.z = 90;
                }
                else
                {
                    rotation.z = 270;
                }
            }
            transform.rotation = Quaternion.Euler(rotation);
        }

        public void SetColor(Color color)
        {
            foreach (var m in MeshesToColor)
            {
                m.material.color = color;
            }
        }

        public void DeathAnimation() // amazing tortouise animation
        {
            rb.isKinematic = true;
            transform
                .DORotate(new Vector3(180, 180, 0), 1.5f, RotateMode.Fast)
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
                        Move(thrust, 1);
                        break;
                    case InputType.Back:
                        Move(thrust*0.75f, -1);
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
            if(Weapon.TryAttack())
			{
				_attackAudio.clip = _chargeAttack;
				_attackAudio.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
				_attackAudio.Play();
			}
        }

        public void Turn(InputType input)
        {
			var clip = _turnings[_turningIterator];
			_turningIterator = (int)Mathf.Repeat(_turningIterator+1, _turnings.Length);
			_turnAudio.clip = clip;
			_turnAudio.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
			_turnAudio.Play();
            int dir = input == InputType.Left ? -1 : 1;
            rb.AddTorque(transform.up * torque * dir);

        }

        void Move(float t, float dir)
        {
            if (rb.velocity.magnitude < _maxVelocity)
            {
                var planeNormal = Vector3.up;
                var forwardInPlane = Vector3.ProjectOnPlane(transform.forward, planeNormal);
                var forwardNormalized = Vector3.Normalize(forwardInPlane);
                var thrustNormalized = t * rb.mass * dir;

                rb.AddForce(forwardNormalized * thrustNormalized);
                if (rb.velocity.magnitude > _maxVelocity)
                {
                    rb.velocity = rb.velocity * 0.95f;
                }
				_moveAudio.clip = _move;
				_moveAudio.pitch = Mathf.Min(2, (rb.velocity.magnitude/16f + 0.85f)*UnityEngine.Random.Range(0.97f, 1.03f));
				_moveAudio.Play();
            }
        }

        public void ReduceMass()
        {
            rb.mass = rb.mass * (2 / 3f);
            InputModule.AdjustLag(rb.mass);
            sparkEmitter.Play();
        }
    }
}
