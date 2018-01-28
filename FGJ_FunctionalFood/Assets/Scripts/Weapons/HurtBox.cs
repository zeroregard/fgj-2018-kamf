using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
namespace MadLagBots
{
    public class HurtBox : MonoBehaviour
    {
        [SerializeField] private HealthModule _self;
		[SerializeField] private AudioSource _audio;
        public float Damage = 10;
        private bool _isHurting = false;
        private bool _canHurt = true;
        private float _hurtCooldown = 0.85f;
        public float PushForce = 1;

        public void SetHurting(bool hurting)
        {
            _isHurting = hurting;
        }

        void OnTriggerStay(Collider col)
        {
            if (_isHurting && _canHurt)
            {
                var healthModule = col.gameObject.GetComponent<HealthModule>();
                if (healthModule != null && healthModule != _self)
                {
                    _canHurt = false;
                    Observable.Timer(TimeSpan.FromSeconds(_hurtCooldown)).Subscribe(_ =>
                    {
                        _canHurt = true;
                    });
                    healthModule.Hurt(Damage);
                    var rb = col.gameObject.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
						_audio.pitch = UnityEngine.Random.Range(1.2f, 1.3f) - rb.mass*1.25f;
						_audio.Play();
                        var localDir = transform.right;
                        rb.AddForce(-transform.TransformDirection(localDir.x, localDir.y, localDir.z) * PushForce, ForceMode.Force);
                    }
                }
            }
        }
    }
}