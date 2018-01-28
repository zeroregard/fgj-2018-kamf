using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadLagBots
{
	public class HealthModule : MonoBehaviour
	{
		public float MaxHealth = 100f;
		private float _health;
		private RoboModule robo;

		void Start()
		{
			robo = GetComponent<RoboModule>();
			_health = MaxHealth;

		}
		

		public void Hurt(float damage)
		{
			_health -= damage;
			print($"I'm Hurting! New health: {_health}");

			robo.ReduceMass();

			if(_health <= 0)
			{
				Die();
			}
		}

		public void Die()
		{
			var manager = FindObjectOfType<GameManager> ();
			robo.DeathAnimation();
			manager.PlayerDied (robo);
            var audioManager = FindObjectOfType<AudioManager>();
            audioManager.AnnounceDeath();
            Destroy(gameObject);
		}
	}
}