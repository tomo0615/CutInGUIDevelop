using UnityEngine;
using UnityEngine.UI;

public class TextChanger
{
    private Text _text;
    private RectTransform _rectTransform;

    public TextChanger(Text text, RectTransform rect)
    {
        _text = text;
        _rectTransform = rect;
    }

    //TODO：Textの変化をpureクラスにまとめる
    public void SetTextString(string text)
    {
        _text.text = text;
    }

    public void SetTextScale(Vector3 scale)
    {
        _rectTransform.localScale = scale;
    }

    public void SetTextColor(Color color)
    {
        _text.color = color;
    }
}
