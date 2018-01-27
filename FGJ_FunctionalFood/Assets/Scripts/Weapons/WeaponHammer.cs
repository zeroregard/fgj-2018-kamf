using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace MadLagBots
{
    public class WeaponHammer : WeaponBase
    {
        [SerializeField] private Transform _hammerRotation;
        [SerializeField] private Vector3 _hammerDown;
        [SerializeField] private Vector3 _hammerUp;
        [SerializeField] private HurtBox _hurtbox;

        public bool TestAttack = true;

        void Update()
        {
            if (TestAttack)
            {
                TryAttack();
            }
        }

        protected override void DoAttack()
        {
            var downTime = _cooldown / 3f;
            var upTime = _cooldown - downTime;
			_hurtbox.SetHurting(true);
            _hammerRotation.DOLocalRotate(_hammerDown, downTime).SetEase(Ease.InBack).OnComplete(() =>
			{
				_hurtbox.SetHurting(false);
				_hammerRotation.DOLocalRotate(_hammerUp, upTime).SetEase(Ease.OutBack);
			});
        }
    }
}