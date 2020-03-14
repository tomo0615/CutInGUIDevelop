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

    private void Start()
    {
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
            oneByOneTextRect.localScale = maxTextScala;

            oneByOneText.color -= Color.black;

            oneByOneText.text = nameText;

            for(int i = 0; i < 5; i++)
            {
                oneByOneTextRect.localScale -= maxTextScala / 10;
                oneByOneText.color += Color.black / 5;
                yield return null;
            }

            _audioSource.PlayOneShot(nameShowSE);
            yield return new WaitForSeconds(textIntervalTime);

            oneByOneText.text = "";
        }
    }

    private IEnumerator ShowSpecialMoveName()
    {
        yield return null;

        _audioSource.PlayOneShot(SpecialMoveNameSE);
        _specialMoveText.gameObject.SetActive(true);

        yield return new WaitForSeconds(3);

        _specialMoveText.gameObject.SetActive(false);
    }
}
