using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadLagBots
{
    public class HurtBox : MonoBehaviour
    {
        [SerializeField] private HealthModule _self;
		public float Damage = 10;
		private bool _isHurting = false;

		public void SetHurting(bool hurting)
		{
			_isHurting = hurting;
		}

        void OnTriggerEnter(Collider col)
        {
			var healthModule = col.gameObject.GetComponent<HealthModule>();
			if(healthModule != null && healthModule != _self && _isHurting)
			{
				// hurt derp
				healthModule.Hurt(Damage);
			}
        }
    }
}