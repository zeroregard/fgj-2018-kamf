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

		RoboModule player1;
		RoboModule player2;

        // Use this for initialization
        void Start()
        {
			BeginGame();
        }

        void BeginGame()
        {
			player1 = Instantiate(HammerBotPrefab, SpawnPointOne.position, Quaternion.identity);
			player2 = Instantiate(HammerBotPrefab, SpawnPointTwo.position, Quaternion.identity);

			player1.GetComponent<InputModule>().Player = 1;
			player2.GetComponent<InputModule>().Player = 2;
        }

		public void PlayerDied(RoboModule player)
		{
			print ("PLAYER DIED");
			var winner = player == player1 ? player2 : player1;
			PlayerWon(winner);
		}

		void PlayerWon(RoboModule player)
		{
			print ("PLAYER WON");
		}

		public void GameOver()
		{
			// stuff
		}
		
    }
}