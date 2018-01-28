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

        private static int[] Players = new int[] { 1, 2, 3, 4 };
        private static Color[] PlayerColors = new Color[] { Color.cyan, Color.yellow, Color.magenta, Color.green };
        private List<RoboModule> _playerInstances = new List<RoboModule>();
        private bool _playerWon = false;

        // Use this for initialization
        void Start()
        {
            BeginGame();
    
        }

        private void ShuffleTransforms() // uiks, wat
        {
            int n = _spawnPoints.Count;
            while (n > 1)
            {
                int k = (Random.Range(0, n));
                n--;
                Transform value = _spawnPoints[k];
                _spawnPoints[k] = _spawnPoints[n];
                _spawnPoints[n] = value;
            }
            
        }

        void BeginGame()
        {
            ShuffleTransforms(); // nooooo
            foreach (var p in Players)
            {
                var spawnPoint = _spawnPoints[p - 1].position;
                var playa = Instantiate(HammerBotPrefab, spawnPoint, Quaternion.identity);
                _playerInstances.Add(playa);
                playa.GetComponent<InputModule>().Player = p;
                playa.SetColor(PlayerColors[p-1]);
                UIManager.AddVisualizer(playa, PlayerColors[p-1]);
  //              playa.GetComponent<HealthModule>().Die();
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
            if (_playerWon == false)
            {
                _playerWon = true;
                print($"PLAYER {player.InputModule.Player} WON!");
                Observable.Timer(System.TimeSpan.FromSeconds(3)).Subscribe(_ =>
                {
                    SceneManager.LoadScene("Main");
                });
            }
        }
    }
}