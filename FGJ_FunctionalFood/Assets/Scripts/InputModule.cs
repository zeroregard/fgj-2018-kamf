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
        public int Player;
        public RoboModule roboMovement;
        private const float _inputDelayMs = 200;
        private bool _acceptInput = true;

        void Update()
        {
            if (_acceptInput)
            {
                GetInput();
            }
        }

        private void GetInput()
        {
            var p = $"Player{Player}";
            var hoz = Input.GetAxis($"{p}_Horizontal");
            var ver = Input.GetAxis($"{p}_Vertical");
            var attack = Input.GetKey($"{p}_Attack");

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
                roboMovement.Attack(InputType.Attack);
                _acceptInput = false;
            }
            else if (hoz != 0)
            {
                if (hoz > 0)
                {
                    roboMovement.Turn(InputType.Right);
                }
                else
                {
                    roboMovement.Turn(InputType.Left);
                }
            }
            else if (ver != 0)
            {
                if (ver > 0)
                {
                    roboMovement.Accelerate(InputType.Forward);
                }
                else
                {
                    roboMovement.Accelerate(InputType.Back);
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