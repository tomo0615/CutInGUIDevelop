using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SpecialMoveAnimation : MonoBehaviour
{
    [SerializeField, Header("文字の表示間隔（秒）")]
    private float textIntervalTime = 0.1f;

    [SerializeField, Header("一文字表示用テキスト")]
    private Text oneByOneText = null;
    private RectTransform oneByOneTextRect
        => oneByOneText.gameObject.GetComponent<RectTransform>();

    [SerializeField, Header("必殺技名コンポーネント")]
    private SpecialMoveText _specialMoveText = null;
    
    [SerializeField, Header("OneByOneText表示の音声")]
    private AudioClip nameShowSE = null;

    [SerializeField, Header("必殺技表示の音声")]
    private AudioClip SpecialMoveNameSE = null;

    private AudioSource _audioSource
        => GetComponent<AudioSource>();

    private TextChanger _textChanger;

    private void Awake()
    {
        _textChanger = new TextChanger(oneByOneText, oneByOneTextRect);

        _specialMoveText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StartCoroutine(PlayAnimation());
        }
    }
    public IEnumerator PlayAnimation()
    {
        //一文字ずつを表示
        yield return StartCoroutine(ShowNameOneByOne());

        //最後に
        yield return StartCoroutine(ShowSpecialMoveName());
    }

    private IEnumerator ShowNameOneByOne()
    {
        _specialMoveText.gameObject.SetActive(false);

        Vector3 maxTextScala = oneByOneTextRect.localScale * 2;

        foreach (string nameText in _specialMoveText.specialMoveNameList)
        {
            _textChanger.SetTextScale(maxTextScala);
            //SetTextScale(maxTextScala);
            _textChanger.SetTextColor(oneByOneText.color - Color.black);
            //SetTextColor(oneByOneText.color - Color.black);

            _textChanger.SetTextString(nameText);
            //SetTextString(nameText);

            //TODO：マジックナンバーを消す
            for (int i = 0; i < 5; i++)
            {
                oneByOneTextRect.localScale -= maxTextScala / 10;
                //SetTextScale(maxTextScala / 10);
                oneByOneText.color += Color.black / 5;
                yield return null;
            }

            _audioSource.PlayOneShot(nameShowSE);
            yield return new WaitForSeconds(textIntervalTime);

            _textChanger.SetTextString("");
        }
    }

    //TODO：Textの変化をpureクラスにまとめる
    /*
    private void SetTextString(string text)
    {
        oneByOneText.text = text;
    }

    private void SetTextScale(Vector3 scale)
    {
        oneByOneTextRect.localScale = scale;
    }

    private void SetTextColor(Color color)
    {
        oneByOneText.color = color;
    }
    */
    private IEnumerator ShowSpecialMoveName()
    {
        yield return new WaitForSeconds(1f);

        _audioSource.PlayOneShot(SpecialMoveNameSE);
        _specialMoveText.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        _specialMoveText.gameObject.SetActive(false);
    }
}
