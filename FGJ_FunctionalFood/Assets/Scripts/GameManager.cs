using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadLagBots
{
    public class GameManager : MonoBehaviour
    {
        public RoboModule HammerBotPrefab;
		public Transform SpawnPointOne;
		public Transform SpawnPointTwo;
        // Use this for initialization
        void Start()
        {
			BeginGame();
        }

        void BeginGame()
        {
			var p1 = Instantiate(HammerBotPrefab, SpawnPointOne.position, Quaternion.identity);
			p1.GetComponent<InputModule>().Player = 1;
			var p2 = Instantiate(HammerBotPrefab, SpawnPointTwo.position, Quaternion.identity);
			p2.GetComponent<InputModule>().Player = 2;
        }

		public void PlayerDied(RoboModule player)
		{
			print ("PLAYER DIED");
		}

		public void GameOver()
		{
			// stuff
		}
		
    }
}