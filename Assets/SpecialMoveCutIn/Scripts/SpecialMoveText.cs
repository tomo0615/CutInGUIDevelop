using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;

public class SpecialMoveText : MonoBehaviour
{
    public List<string> specialMoveNameList { get; private set; }
        = new List<string>();
    
    private void Awake()
    {
        List<Text> textList
            = GetComponentsInChildren<Text>().ToList();

        //一文字ずつに分割して格納
        foreach(Text text in textList)
        {
            for(int i = 0; i < text.text.Length; i++)
            {
                specialMoveNameList.Add(text.text[i].ToString());
            }
        }
    }
}
