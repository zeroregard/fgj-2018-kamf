using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;

public class InputKey : MonoBehaviour
{
    [SerializeField] private RectTransform _rect;
    [SerializeField] private Image _img;

    public void Init(Sprite character, float time, float start, float end, float zRotation)
    {
        _img.sprite = character;
		_img.rectTransform.localRotation = Quaternion.Euler(0, 0, zRotation);
        var dt = 0f;
        Observable.EveryUpdate().TakeUntilDestroy(this).Subscribe(_ =>
        {
            dt += Time.deltaTime;
            var t = dt / time;
            var x = Mathf.Lerp(start, end, t);
            _rect.anchoredPosition = new Vector2(x, 0);
            if (dt >= time)
            {
                Destroy(gameObject);
            }
        });
    }
}
