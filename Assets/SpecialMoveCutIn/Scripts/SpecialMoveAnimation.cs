﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpecialMoveAnimation : MonoBehaviour
{
    [SerializeField]
    private float textIntervalTime = 0.1f;

    [SerializeField]
    private Text oneByOneText = null;

    private RectTransform oneByOneTextRect
        => oneByOneText.gameObject.GetComponent<RectTransform>();

    [SerializeField]
    private SpecialMoveText _specialMoveText = null;

    /*
    [SerializeField]
    private AudioClip nameShowSE = null;

    [SerializeField]
    private AudioClip SpecialMoveNameSE = null;
    */
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

            yield return new WaitForSeconds(textIntervalTime);

            oneByOneText.text = "";
        }
    }

    private IEnumerator ShowSpecialMoveName()
    {
        yield return null;

        _specialMoveText.gameObject.SetActive(true);
    }
}
