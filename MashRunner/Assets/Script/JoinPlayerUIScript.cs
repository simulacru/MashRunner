using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JoinPlayerUIScript : MonoBehaviour
{
    //プレイヤー参加UI関係
    public GameObject[] playerUIArray = new GameObject[4];
    public GameObject readyUI;

    //ReadyUI関係
    Button selectButton;
    [SerializeField] private string stageSelectScene = null;

    //SE関係
    AudioSource audioSource;
    public AudioClip nextSE;
    public AudioClip backSE;
    bool hasPlayedNextSE = false;
    bool hasPlayedBackSE = false;
    public MenuSEScript menuSEScript;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //ボタン選択状態
        if (selectButton != null)
        {
            selectButton = GameObject.Find("Canvas/OK").GetComponent<Button>();
            selectButton.Select();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーに対応したUIを表示
        playerUIArray[0].gameObject.SetActive(!GetPlayer.isJoinPlayer1);
        playerUIArray[1].gameObject.SetActive(!GetPlayer.isJoinPlayer2);
        playerUIArray[2].gameObject.SetActive(!GetPlayer.isJoinPlayer3);
        playerUIArray[3].gameObject.SetActive(!GetPlayer.isJoinPlayer4);

        //OKUIを表示
        readyUI.gameObject.SetActive(PlayerOtherController.isReady);

        //UI表示時SE
        if (PlayerOtherController.isReady && nextSE != null && !hasPlayedNextSE)
        {
            audioSource.PlayOneShot(nextSE);
            hasPlayedNextSE = true;
        }
        else if (!PlayerOtherController.isReady)
        {
            hasPlayedNextSE = false;
        }

        if (PlayerOtherController.isBack && !hasPlayedBackSE)
        {
            menuSEScript.BackSceneSE();
            hasPlayedBackSE = true;
        }
            
    }

    //ステージ選択へ
    public void OK()
    {
        menuSEScript.NextSceneSE();
        SceneManager.LoadScene(stageSelectScene);
        PlayerOtherController.isReady = false;
    }

    //UIを非表示
    public void Back()
    {
        audioSource.PlayOneShot(backSE);
        PlayerOtherController.isReady = false;
    }
}