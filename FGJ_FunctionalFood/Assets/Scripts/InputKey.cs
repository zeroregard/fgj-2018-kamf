using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;

public class InputKey : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _character;
	[SerializeField] private RectTransform _rect;

	public void Init(string character, float time, float start, float end)
	{
		_character.text = character;
		var dt = 0f;
		Observable.EveryUpdate().TakeUntilDestroy(this).Subscribe(_ =>
		{
			dt += Time.deltaTime;
			var t = dt/time;
			var x = Mathf.Lerp(start, end, t);
			_rect.anchoredPosition = new Vector2(x, 0);
			if(dt >= time)
			{
				Destroy(gameObject);
			}
		});
	}
}
