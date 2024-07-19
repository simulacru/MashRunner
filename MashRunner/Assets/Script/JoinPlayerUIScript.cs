using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JoinPlayerUIScript : MonoBehaviour
{
    //�v���C���[�Q��UI�֌W
    public GameObject[] playerUIArray = new GameObject[4];
    public GameObject readyUI;

    //ReadyUI�֌W
    Button selectButton;
    [SerializeField] private string stageSelectScene = null;

    //SE�֌W
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

        //�{�^���I�����
        if (selectButton != null)
        {
            selectButton = GameObject.Find("Canvas/OK").GetComponent<Button>();
            selectButton.Select();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //�v���C���[�ɑΉ�����UI��\��
        playerUIArray[0].gameObject.SetActive(!GetPlayer.isJoinPlayer1);
        playerUIArray[1].gameObject.SetActive(!GetPlayer.isJoinPlayer2);
        playerUIArray[2].gameObject.SetActive(!GetPlayer.isJoinPlayer3);
        playerUIArray[3].gameObject.SetActive(!GetPlayer.isJoinPlayer4);

        //OKUI��\��
        readyUI.gameObject.SetActive(PlayerOtherController.isReady);

        //UI�\����SE
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

    //�X�e�[�W�I����
    public void OK()
    {
        menuSEScript.NextSceneSE();
        SceneManager.LoadScene(stageSelectScene);
        PlayerOtherController.isReady = false;
    }

    //UI���\��
    public void Back()
    {
        audioSource.PlayOneShot(backSE);
        PlayerOtherController.isReady = false;
    }
}