using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace MadLagBots
{
    public class CameraManager : MonoBehaviour
    {
        private bool _initted;
		private List<RoboModule> _robots;
		[SerializeField] private Vector3 _offset;
		public float Speed = 5f;

        public void Init(List<RoboModule> robots)
        {
			_robots = robots;
			_initted = true;
        }

		public void PlayerDied(RoboModule player)
		{
			_robots.Remove(player);
		}

        void Update()
        {
            if (_initted)
            {
				var finalPos = _offset + AvgPosition();
				transform.position = Vector3.Lerp(transform.position, finalPos, Speed*Time.deltaTime);
            }
        }

		private Vector3 AvgPosition()
		{
			var avg = Vector3.zero;
			foreach(var r in _robots)
			{
				avg += r.transform.position;
			}
			avg /= _robots.Count;
			return avg;
		}
    }
}