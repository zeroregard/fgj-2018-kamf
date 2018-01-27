using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class WeaponBase : MonoBehaviour
{
	protected bool _canAttack = true;
	protected float _cooldown = 2f;

	public virtual void TryAttack()
	{
		if(_canAttack)
		{
			DoAttack();
			_canAttack = false;
			Observable.Timer(TimeSpan.FromSeconds(_cooldown)).Subscribe(_ =>
			{
				_canAttack = true;
			});
		}
	}

	protected virtual void DoAttack()
	{

	}
}
