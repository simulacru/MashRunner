using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuControll : MonoBehaviour
{
    //シーン
    [SerializeField] private string playScene = null;
    [SerializeField] private string HTPScene = null; //How to Play
    [SerializeField] private string soundScene = null;
    [SerializeField] private string titleScene = null;

    //ボタン
    Button playButton;
    Button HTPButton;
    Button soundButton;

    //選択状態記憶
    public static bool isSound = false;
    public static bool isHTP = false;

    //フェードアウト
    public FadeOutScript fadeOutScript;

    //シーン切り替え用SE
    public MenuSEScript menuSEScript;

    // Start is called before the first frame update
    void Start()
    {
        //メニュー画面に戻った際に決定したボタンを選択状態にする
        if (isSound)
        {
            //ボタンを選択した状態にする
            soundButton = GameObject.Find("Canvas/Sound").GetComponent<Button>();
            soundButton.Select();
        }
        else if (isHTP)
        {
            HTPButton = GameObject.Find("Canvas/HowtoPlay").GetComponent<Button>();
            HTPButton.Select();
        }
        else
        {
            playButton = GameObject.Find("Canvas/Play").GetComponent<Button>();
            playButton.Select();
        }

        //リセット
        PlayerCameraScript.joinPlayerCount = 0;
        StageSlectControll.isStage1 = false;
        StageSlectControll.isStage2 = false;
        PlayerOtherController.isReady = false;
        PlayerOtherController.isBack = false;
    }

    //プレイヤー参加画面へ
    public void OnPlay()
    {
        isHTP = false;
        isSound = false;

        menuSEScript.NextSceneSE();
        SceneManager.LoadScene(playScene);
    }

    //遊び方画面へ
    public void OnHTP()
    {
        isHTP = true;

        menuSEScript.NextSceneSE();
        SceneManager.LoadScene(HTPScene);
    }

    //サウンド設定画面へ
    public void OnSound()
    {
        isSound = true;

        menuSEScript.NextSceneSE();
        SceneManager.LoadScene(soundScene);
    }

    //タイトルへ
    public void BackScene()
    {
        isHTP = false;
        isSound = false;

        menuSEScript.BackSceneSE();
        //フェードアウト
        StartCoroutine(fadeOutScript.FadeOut());
        SceneManager.LoadScene(titleScene);
    }
}
