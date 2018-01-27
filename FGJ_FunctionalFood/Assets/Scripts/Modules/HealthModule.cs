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
			print("I'm Hurting");
			_health -= damage;

			robo.SetMass();

			if(_health <= 0)
			{
				Die();
			}
		}

		private void Die()
		{
			Destroy(gameObject);
			var manager = FindObjectOfType<GameManager> ();
			manager.PlayerDied (robo);
		}
	}
}