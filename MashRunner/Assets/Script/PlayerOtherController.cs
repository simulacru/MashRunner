using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class PlayerOtherController : MonoBehaviour
{
    //シーン遷移関係
    [SerializeField] private string stageSelectScene = null;
    [SerializeField] private string menuScene = null;
    [SerializeField] private string playerJoinScene = null;

    //プレイヤー参加確認用
    public static bool isReady = false;
    public static bool isBack = false;

    //ゴール関係
    public GameObject goalBackground;
    public static int rank;
    public static int[] rankArray = new int[4];
    public bool isGoal = false;
    public bool hasGoal = false;
    bool[] playerArray = new bool[4] { false, false, false, false };

    //パーティクル関係
    [SerializeField] ParticleSystem particle;
    [SerializeField] Color[] colorArray = new Color[4];

    //メニューボタンの表示
    public static bool isDisplay = false;

    //キャラクターアイコン
    public Sprite[] characterUIArray;
    public Image characterImage;

    //オプション
    public GameObject optionOn;
    public GameObject optionOff;
    bool isDisplayOpt = true;

    //UI表示位置
    public GameObject mainUI;

    // Start is called before the first frame update
    void Start()
    {
        //オブジェクト保持
        DontDestroyOnLoad(this.gameObject);

        isReady = false;
        isBack = false;

        CharacterChange();

        goalBackground.SetActive(false);
        characterImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //二人プレイ時にUIの位置を変更
        if (2 <= PlayerCameraScript.joinPlayerCount && PlayerCameraScript.joinPlayerCount < 3)
        {
            mainUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(-520, 0);
        }
        else
        {
            mainUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(-220, 0);
        }

        //ゴールした後に少し右へ移動
        if (isGoal)
        {
            Vector2 current = transform.position;
            Vector2 target = new Vector2(1060, current.y);
            float step = 8.0f * Time.deltaTime;
            transform.position = Vector2.MoveTowards(current, target, step);
        }

        ResetStage();

        if (SceneManager.GetActiveScene().name == "ResultScene" || SceneManager.GetActiveScene().name == "StageSelectScene" || SceneManager.GetActiveScene().name == "PlayerJoinScene")
        {
            transform.position = new Vector3(0.0f, -3.0f, 0.0f);
            hasGoal = false;
        }

        //オプション表示切替
        optionOn.gameObject.SetActive(isDisplayOpt);
        optionOff.gameObject.SetActive(!isDisplayOpt);

        //if (SceneManager.GetActiveScene().name == "MenuScene")
        //    Destroy(gameObject);
    }

    //マップ選択画面へ
    public void OnReady(InputAction.CallbackContext context)
    {
        //確認UIを表示
        if (SceneManager.GetActiveScene().name == "PlayerJoinScene")
        {
            isReady = true;
        }  
    }

    //メニュー画面へ
    public void OnBack(InputAction.CallbackContext context)
    {
        //プレイヤー参加画面の時
        if (SceneManager.GetActiveScene().name == "PlayerJoinScene" && !isReady)
        {
            isBack = true;
            SceneManager.LoadScene(menuScene);
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
        }
        //リザルト画面の時
        else if (SceneManager.GetActiveScene().name == "ResultScene")
        {
            isDisplay = false;
        }
    }

    public void OnSelect(InputAction.CallbackContext context)
    {
        //リザルト画面の時
        if (SceneManager.GetActiveScene().name == "ResultScene")
        {
            isDisplay = true;
            hasGoal = false;
        }
    }

    //オプション表示切替
    public void Option(InputAction.CallbackContext context)
    {
        isDisplayOpt = !isDisplayOpt;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //ゴールに到達
        if (collision.gameObject.CompareTag("Goal") && !hasGoal)
        {
            isGoal = true;

            //ランキング
            for (int i = 0; i < rankArray.Length; i++)
            {
                if (playerArray[i])
                    rankArray[i] = GoalScript.goalPlayerCount + 1;
            }

            goalBackground.SetActive(true);

            hasGoal = true;
        }
    }

    void CharacterChange()
    {
        var main = particle.main;

        //プレイヤーによって変える
        if (!GetPlayer.isJoinPlayer4)
        {
            playerArray[3] = true;
            characterImage.sprite = characterUIArray[3];
            main.startColor = new ParticleSystem.MinMaxGradient(colorArray[3]);
        }
        else if (!GetPlayer.isJoinPlayer3)
        {
            playerArray[2] = true;
            characterImage.sprite = characterUIArray[2];
            main.startColor = new ParticleSystem.MinMaxGradient(colorArray[2]);
        }
        else if (!GetPlayer.isJoinPlayer2)
        {
            playerArray[1] = true;
            characterImage.sprite = characterUIArray[1];
            main.startColor = new ParticleSystem.MinMaxGradient(colorArray[1]);
        }
        else if (!GetPlayer.isJoinPlayer1)
        {
            playerArray[0] = true;
            characterImage.sprite = characterUIArray[0];
            main.startColor = new ParticleSystem.MinMaxGradient(colorArray[0]);
        }
    }

    void ResetStage()
    {
        //スタート位置をリセット
        if (ResultMenuScript.canGoal)
        {
            transform.position = new Vector3(0.0f, -3.0f, 0.0f);
            isDisplay = false;
            isDisplayOpt = true;
            isGoal = false;

            //Goal背景リセット
            goalBackground.SetActive(false);
        }
    }
}
