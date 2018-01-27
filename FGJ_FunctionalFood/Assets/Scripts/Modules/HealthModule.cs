using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModule : MonoBehaviour
{
	public float MaxHealth = 100f;
	private float _health;

	void Start()
	{
		_health = MaxHealth;
	}
	

	public void Hurt(float damage)
	{
		_health -= damage;
		if(_health <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		Destroy(gameObject);
	}
}
