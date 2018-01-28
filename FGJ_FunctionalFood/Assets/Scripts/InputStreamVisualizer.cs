using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadLagBots
{
    public class SpriteRotation
    {
        public Sprite Sprite;
        public float ZRotation;
    }
    public class InputStreamVisualizer : MonoBehaviour
    {
		[SerializeField] private InputKey _inputKeyPrefab;
		[SerializeField] private RectTransform _rect;
        [SerializeField] private Sprite _arrow;
        [SerializeField] private Sprite _attack;

        private float _startWidth = 800f;

        void Start()
        {
            SetDelayVisualizer(1);
        }

        public void VisualizeInput(InputType input, float lagSeconds)
        {
			SpriteRotation sr = InputToString(input);
			var key = Instantiate(_inputKeyPrefab);
			var keyRect = key.GetComponent<RectTransform>();
			keyRect.SetParent(_rect);
			keyRect.anchoredPosition = Vector2.zero;
			key.Init(sr.Sprite, lagSeconds, 0, _rect.rect.width, sr.ZRotation);
        }

        public void SetDelayVisualizer(float t)
        {
            var width = Mathf.Lerp(0, _startWidth, t);
            _rect.sizeDelta = new Vector2(width, _rect.sizeDelta.y);
        }

        private SpriteRotation InputToString(InputType input)
        {
            switch (input)
            {
                case InputType.Attack:
                    return new SpriteRotation(){Sprite=_attack, ZRotation = 0};
                case InputType.Left:
                    return new SpriteRotation(){Sprite=_arrow, ZRotation = 90};
                case InputType.Right:
                    return new SpriteRotation(){Sprite=_arrow, ZRotation = -90};
                case InputType.Forward:
                    return new SpriteRotation(){Sprite=_arrow, ZRotation = 0};
                case InputType.Back:
                    return new SpriteRotation(){Sprite=_arrow, ZRotation = 180};
            }
			return new SpriteRotation(){Sprite=_attack, ZRotation = 0};
        }
    }
}