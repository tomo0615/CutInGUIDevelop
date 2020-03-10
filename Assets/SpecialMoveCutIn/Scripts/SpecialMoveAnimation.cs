using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialMoveAnimation : MonoBehaviour
{
    [SerializeField]
    private Text OneByOneText = null;

    [SerializeField]
    private SpecialMoveText _specialMoveText = null;

    [SerializeField]
    private AudioClip nameShowSE = null;

    [SerializeField]
    private AudioClip SpecialMoveNameSE = null;

    public void PlayAnimation()
    {
        //一文字ずつを表示
        ShowNameOneByOne();

        //最後に
        ShowSpecialMoveName();
    }

    private void ShowNameOneByOne()
    {

    }

    private void ShowSpecialMoveName()
    {

    }



}
