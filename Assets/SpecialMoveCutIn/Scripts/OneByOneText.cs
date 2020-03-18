using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class OneByOneText : MonoBehaviour
{
    private Text _text;

    private RectTransform _rectTransform;

    private Vector3 maxTextScala;
    private void Awake()
    {
        _text = GetComponent<Text>();
        _rectTransform = GetComponent<RectTransform>();

        maxTextScala = _rectTransform.localScale * 2;
    }

    public IEnumerator ViewOneByOneText(string nameText, float textAnimationThreshold)
    {         
        _rectTransform.localScale = maxTextScala;

        _text.color -= Color.black;

        _text.text = nameText;

        for (int i = 0; i < textAnimationThreshold; i++)
        {
            _rectTransform.localScale -= maxTextScala / (textAnimationThreshold * 2);

            _text.color += Color.black / textAnimationThreshold;

            yield return null;
        }
    }
}
