using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultMenuScript : MonoBehaviour
{
    //シーン遷移関係
    [SerializeField] private string stage1Scene;
    [SerializeField] private string stage2Scene;
    [SerializeField] private string stageSelectScene;
    [SerializeField] private string menuScene;

    Button selectButton;
    public GameObject menuFlameUI;

    bool isWait = true;

    static public bool canGoal = false;

    //フェードアウト
    public FadeOutScript fadeOutScript;

    //SE
    AudioSource audioSource;
    public AudioClip nextSE;
    public AudioClip backSE;
    public MenuSEScript menuSEScript;
    bool hasPlayedNextSE = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //ボタン選択状態
        if(selectButton != null)
        {
            selectButton = GameObject.Find("Canvas/Retry").GetComponent<Button>();
            selectButton.Select();
        }

        canGoal = false;

        //メニューを三秒は表示しないようにする
        Invoke("ResultStart", 4.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //UI表示
        menuFlameUI.gameObject.SetActive(PlayerOtherController.isDisplay && !isWait);

        if(PlayerOtherController.isDisplay && nextSE != null && !hasPlayedNextSE && !isWait)
        {
            audioSource.PlayOneShot(nextSE);
            hasPlayedNextSE = true;
        }            
    }

    void ResultStart()
    {
        isWait = false;
        Debug.Log(isWait);
    }

    //リトライへ
    public void Retry()
    {
        //リセット
        CountdownScript.canCountdown = true;
        canGoal = true;
        PlayerOtherController.rank = 0;

        //SE
        menuSEScript.NextSceneSE();

        //フェードアウト
        StartCoroutine(fadeOutScript.FadeOut());

        if (StageSlectControll.isStage1) //シーン1
            SceneManager.LoadScene(stage1Scene);
        else if (StageSlectControll.isStage2) //シーン2
            SceneManager.LoadScene(stage2Scene);
    }

    //ステージセレクトへ
    public void StageSlect()
    {
        //リセット
        canGoal = true;
        PlayerOtherController.rank = 0;

        //SE
        menuSEScript.NextSceneSE();

        //フェードアウト
        StartCoroutine(fadeOutScript.FadeOut());

        SceneManager.LoadScene(stageSelectScene);
    }

    //メニューへ
    public void Menu()
    {
        //リセット
        canGoal = true;

        //メニュー画面に戻るとPlayerタグのオブジェクトを消す
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in objects)
        {
            Destroy(player);
        }

        //boolのリセット
        GetPlayer.isJoinPlayer1 = true;
        GetPlayer.isJoinPlayer2 = true;
        GetPlayer.isJoinPlayer3 = true;
        GetPlayer.isJoinPlayer4 = true;

        //SE
        menuSEScript.NextSceneSE();

        //フェードアウト
        StartCoroutine(fadeOutScript.FadeOut());

        SceneManager.LoadScene(menuScene);
    }

    //メニューを消す
    public void Back()
    {
        PlayerOtherController.isDisplay = false;
        hasPlayedNextSE = false;
        audioSource.PlayOneShot(backSE);
    }
}
