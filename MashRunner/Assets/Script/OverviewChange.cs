using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class OverviewChange : MonoBehaviour
{
    //スライダー関係
    public Slider[] selectSlider;
    public static Slider findObj;
    public EventSystem eventSystem;

    //テキスト関係
    [SerializeField] TextMeshProUGUI BGMText;
    [SerializeField] TextMeshProUGUI SEText;

    //シーン関係
    [SerializeField] private string prevScene = null;

    //SE
    public MenuSEScript menuSEScript;

    // Start is called before the first frame update
    void Start()
    {
        //メニューボタン選択状態取得
        MenuControll.isSound = true;
    }

    // Update is called once per frame
    void Update()
    {
        findObj = eventSystem.currentSelectedGameObject.gameObject.GetComponentInParent<Slider>();

        //選択中のスライダーの文字の色を変える
        for (int i = 0; i < selectSlider.Length; i++)
        {
            if (findObj == selectSlider[0])
            {
                BGMText.color = new Color(1.0f, 0.0f, 0.0f, 1.0f); //赤
                SEText.color = new Color(1.0f, 1.0f, 1.0f, 1.0f); //白
            }
            else if(findObj == selectSlider[1])
            {
                BGMText.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                SEText.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            }
            else
            {
                BGMText.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                SEText.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
        }
    }

    //メニューへ
    public void BackScene()
    {
        menuSEScript.BackSceneSE();
        SceneManager.LoadScene(prevScene);
    }
}
