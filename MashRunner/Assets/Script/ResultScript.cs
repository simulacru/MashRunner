using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScript : MonoBehaviour
{
    public int result;
    public Sprite[] resultArray;
    private Image image;
    private bool isResult = false;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Result();
    }

    void Result()
    {
        isResult = false;
        for (int i = 0; i < resultArray.Length; i++)
        {
            //順位に対応したプレイヤーの画像を表示
            if (GoalScript.rankArray[i] <= result && GoalScript.rankArray[i] > result - 1)
            {
                image.enabled = true;
                image.sprite = resultArray[i];
                isResult = true;
                break;
            }
        }

        if (!isResult)
            image.enabled = false;
    }
}
