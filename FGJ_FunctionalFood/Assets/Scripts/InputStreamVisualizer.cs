using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadLagBots
{
    public class InputStreamVisualizer : MonoBehaviour
    {
		[SerializeField] private InputKey _inputKeyPrefab;
		[SerializeField] private RectTransform _rect;
        private float _lagMs = 3000f; // move this somewhere else, like InputModule


        public void VisualizeInput(InputType input)
        {
			string c = InputToString(input);
			var key = Instantiate(_inputKeyPrefab);
			key.Init(c, _lagMs, 0, _rect.rect.width);
        }

        private string InputToString(InputType input)
        {
            switch (input)
            {
                case InputType.Attack:
                    return "A";
                case InputType.Left:
                    return "<";
                case InputType.Right:
                    return ">";
                case InputType.Forward:
                    return "^";
                case InputType.Back:
                    return "v";
            }
			return "";
        }
    }
}