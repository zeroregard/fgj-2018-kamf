using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


namespace MadLagBots
{
    public enum InputType
    {
        Left,
        Right,
        Forward,
        Back,
        Attack
    }

    public class InputModule : MonoBehaviour
    {
        [Range(1, 4)]
        public int Player = 1;
        public RoboModule roboMovement;
        public InputStreamVisualizer visualizer;
        private const float _inputIntervalMs = 300;
        private bool _acceptInput = true;
        private float _baseLagSeconds = 1.5f;
        private float _currentLag;

        void Start()
        {
            _currentLag = _baseLagSeconds;
        }

        void Update()
        {
            if (_acceptInput)
            {
                GetInput();
            }
        }

        public void AdjustLag(float mass)
        {
            _currentLag = _baseLagSeconds * roboMovement.rb.mass;
            var t = Mathf.InverseLerp(0, _baseLagSeconds, _currentLag);
            visualizer.SetDelayVisualizer(t);
        }

        private void SendInput(InputType input)
        {
            _acceptInput = false;
            if (visualizer != null)
            {
                visualizer.VisualizeInput(input, _currentLag);
            }
            if (roboMovement != null)
            {
                roboMovement.HandleInput(input, _currentLag);
            }

        }

        private void GetInput()
        {
            var p = $"Player{Player}";
            var left = Input.GetButtonDown($"{p}_Left");
            var right = Input.GetButtonDown($"{p}_Right");
            var ver = Input.GetAxis($"{p}_Vertical");
            var attack = Input.GetButton($"{p}_Attack");

            if (!left && !right && ver == 0 && !attack)
            {
                return;
            }
            if (attack && (right || left) && ver != 0)
            {
                print("Honk!");
            }
            else if (attack)
            {
                SendInput(InputType.Attack);
            }
            else if (left)
            {

                SendInput(InputType.Left);
            }
            else if (right)
            {
                SendInput(InputType.Right);
            }
            else if (ver != 0)
            {
                if (ver > 0)
                {
                    SendInput(InputType.Forward);
                }
                else
                {
                    SendInput(InputType.Back);
                }
            }

            if (_acceptInput == false)
            {
                Observable.Timer(System.TimeSpan.FromMilliseconds(_inputIntervalMs)).Subscribe(_ =>
                {
                    _acceptInput = true;
                });
            }
        }
    }
}