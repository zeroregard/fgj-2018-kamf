using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadLagBots
{
    public class InputStreamVisualizer : MonoBehaviour
    {
		[SerializeField] private InputKey _inputKeyPrefab;
		[SerializeField] private RectTransform _rect;

        private float _startWidth = 384f;

        public void VisualizeInput(InputType input, float lagSeconds)
        {
			string c = InputToString(input);
			var key = Instantiate(_inputKeyPrefab);
			var keyRect = key.GetComponent<RectTransform>();
			keyRect.SetParent(_rect);
			keyRect.anchoredPosition = Vector2.zero;
			key.Init(c, lagSeconds, 0, _rect.rect.width);
        }

        public void SetDelayVisualizer(float t)
        {
            var width = Mathf.Lerp(0, _startWidth, t);
            _rect.sizeDelta = new Vector2(width, _rect.sizeDelta.y);
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