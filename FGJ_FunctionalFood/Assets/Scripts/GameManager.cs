using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;
using UnityEngine.SceneManagement;

namespace MadLagBots
{
    public class GameManager : MonoBehaviour
    {
        public RoboModule HammerBotPrefab;
        public List<Transform> _spawnPoints;
        public UIManager UIManager;

        private static int[] Players = new int[] { 1, 2 };
        private List<RoboModule> _playerInstances = new List<RoboModule>();

        // Use this for initialization
        void Start()
        {
            BeginGame();
        }

        void BeginGame()
        {
            foreach (var p in Players)
            {
                var playa = Instantiate(HammerBotPrefab, _spawnPoints[p - 1].position, Quaternion.identity);
                _playerInstances.Add(playa);
                playa.GetComponent<InputModule>().Player = p;
                UIManager.AddVisualizer(playa);
            }
        }

        public void PlayerDied(RoboModule player)
        {
            _playerInstances.Remove(player);
            if (_playerInstances.Count == 1)
            {
                PlayerWon(_playerInstances.FirstOrDefault());
            }
        }

        void PlayerWon(RoboModule player)
        {
            print($"PLAYER {player.InputModule.Player} WON!");
            Observable.Timer(System.TimeSpan.FromSeconds(3)).Subscribe(_ =>
            {
                SceneManager.LoadScene(0);
            });
        }
    }
}