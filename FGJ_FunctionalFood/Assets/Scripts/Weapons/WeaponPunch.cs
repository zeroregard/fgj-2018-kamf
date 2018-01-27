using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
namespace MadLagBots
{
    public class WeaponPunch : WeaponBase
    {
        [SerializeField] private Transform _puncher;
        [SerializeField] private Vector3 _punchActivated;
        [SerializeField] private Vector3 _punchDeactivated;
        [SerializeField] private HurtBox _hurtbox;

        protected override void DoAttack()
        {
            var downTime = _cooldown / 5f;
            var upTime = _cooldown - downTime;
            _puncher.DOLocalMove(_punchActivated, downTime).SetEase(Ease.InBack).OnComplete(() =>
            {
                _hurtbox.SetHurting(true);
                Observable.Timer(System.TimeSpan.FromSeconds(upTime / 2f)).Subscribe(_ =>
                  {
                      _hurtbox.SetHurting(false);
                  });
                _puncher.DOLocalMove(_punchDeactivated, upTime).SetEase(Ease.OutBack);
            });
        }
    }
}