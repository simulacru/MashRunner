using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSlectControll : MonoBehaviour
{
    [SerializeField] private string stage1Scene;
    [SerializeField] private string stage2Scene;
    [SerializeField] private string prevScene;

    //リザルトのリトライ用
    public static bool isStage1 = false;
    public static bool isStage2 = false;

    Button selectButton;

    public MenuSEScript menuSEScript;
    public FadeOutScript fadeOutScript;

    // Start is called before the first frame update
    void Start()
    {
        //ボタン選択状態
        selectButton = GameObject.Find("Canvas/Stage1").GetComponent<Button>();
        selectButton.Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ステージ1へ
    public void Stage1()
    {
        //BGM停止
        TitleBGMPlayer.soundStop = true;
        menuSEScript.NextSceneSE(); //SE

        //カウントダウンリセット
        CountdownScript.canCountdown = true;

        isStage1 = true;
        isStage2 = false;

        //フェードアウト
        StartCoroutine(fadeOutScript.FadeOut());

        SceneManager.LoadScene(stage1Scene);
    }

    //ステージ2へ
    public void Stage2()
    {
        //BGM停止
        TitleBGMPlayer.soundStop = true;
        menuSEScript.NextSceneSE(); //SE

        //カウントダウンリセット
        CountdownScript.canCountdown = true;

        isStage1 = false;
        isStage2 = true;

        //フェードアウト
        StartCoroutine(fadeOutScript.FadeOut());

        SceneManager.LoadScene(stage2Scene);
    }

    //前のシーンへ
    public void BackScene()
    {
        //SE
        menuSEScript.BackSceneSE();

        isStage1 = false;
        isStage2 = false;;

        SceneManager.LoadScene(prevScene);
    }
}
