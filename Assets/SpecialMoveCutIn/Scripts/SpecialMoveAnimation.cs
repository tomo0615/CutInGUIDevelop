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

    [SerializeField, Header("一文字表示用コンポーネント")]
    private OneByOneText _oneByOneText = null;

    [SerializeField, Header("必殺技名コンポーネント")]
    private SpecialMoveText _specialMoveText = null;

    [SerializeField]
    private SoundTable _soundTable = null;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        
        _specialMoveText.gameObject.SetActive(false);
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

        _oneByOneText.gameObject.SetActive(true);

        foreach (string nameText in _specialMoveText.specialMoveNameList)
        {
            yield return _oneByOneText.ViewOneByOneText(nameText, textAnimationThreshold);

            _audioSource.PlayOneShot(_soundTable.nameShowSE);

            yield return new WaitForSeconds(textIntervalTime);
        }

        _oneByOneText.gameObject.SetActive(false);
    }

    private IEnumerator ShowSpecialMoveName()
    {
        _audioSource.PlayOneShot(_soundTable.SpecialMoveNameSE);

        _specialMoveText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        _specialMoveText.gameObject.SetActive(false);
    }
}
