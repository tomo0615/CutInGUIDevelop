using UnityEngine;

[CreateAssetMenu(menuName = ("Create SpecialMoveSoundTable"))]
public class SoundTable : ScriptableObject
{
    [Header("一文字表示の音声")]
    public AudioClip nameShowSE = null;

    [Header("必殺技表示の音声")]
    public AudioClip SpecialMoveNameSE = null;
}
