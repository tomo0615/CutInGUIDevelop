using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SpecialMoveAnimation : MonoBehaviour
{
    [SerializeField, Header("文字の表示間隔（秒）")]
    private float textIntervalTime = 0.001f;

    [SerializeField, Header("文字アニメーションの閾値")]
    private float textAnimationThreshold = 3f;

    [SerializeField, Header("一文字表示用テキスト")]
    private Text oneByOneText = null;
    private RectTransform oneByOneTextRect;

    [SerializeField, Header("必殺技名コンポーネント")]
    private SpecialMoveText _specialMoveText = null;

    [SerializeField]
    private SoundTable _soundTable = null;

    private AudioSource _audioSource;

    private TextChanger _textChanger;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        oneByOneTextRect
            = oneByOneText.gameObject.GetComponent<RectTransform>();
        
        _specialMoveText.gameObject.SetActive(false);

        _textChanger = new TextChanger(oneByOneText, oneByOneTextRect);
    }

    private void Update()
    {
        //デバック用
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

            _textChanger.SetTextColor(oneByOneText.color - Color.black);

            _textChanger.SetTextString(nameText);

            for (int i = 0; i < textAnimationThreshold; i++)
            {
                _textChanger.SetTextScale(
                    oneByOneTextRect.localScale - maxTextScala / (textAnimationThreshold * 2));

                _textChanger.SetTextColor(
                    oneByOneText.color + Color.black / textAnimationThreshold);

                yield return null;
            }

            _audioSource.PlayOneShot(_soundTable.nameShowSE);

            yield return new WaitForSeconds(textIntervalTime);

            _textChanger.SetTextString("");
        }
    }

    private IEnumerator ShowSpecialMoveName()
    {
        _audioSource.PlayOneShot(_soundTable.SpecialMoveNameSE);

        _specialMoveText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        _specialMoveText.gameObject.SetActive(false);
    }
}
