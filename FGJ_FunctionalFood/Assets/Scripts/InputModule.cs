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
        private const float _inputDelayMs = 200;
        private bool _acceptInput = true;

        void Update()
        {
            if (_acceptInput)
            {
                GetInput();
            }
        }

        private void SendInput(InputType input)
        {
            _acceptInput = false;
            if (visualizer != null)
            {
                visualizer.VisualizeInput(input);
            }
            if (roboMovement != null)
            {
                roboMovement.HandleInput(input);
            }

        }

        private void GetInput()
        {
            var p = $"Player{Player}";
            var hoz = Input.GetAxis($"{p}_Horizontal");
            var ver = Input.GetAxis($"{p}_Vertical");
            var attack = Input.GetButton($"{p}_Attack");

            if (hoz == 0 && ver == 0 && !attack)
            {
                return;
            }
            if (attack && hoz != 0 && ver != 0)
            {
                print("Honk!");
            }
            else if (attack)
            {
                SendInput(InputType.Attack);
            }
            else if (hoz != 0)
            {
                if (hoz > 0)
                {
                    SendInput(InputType.Right);
                }
                else
                {
                    SendInput(InputType.Left);
                }
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
                Observable.Timer(System.TimeSpan.FromMilliseconds(_inputDelayMs)).Subscribe(_ =>
                {
                    _acceptInput = true;
                });
            }
        }
    }
}