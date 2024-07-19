using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultMenuScript : MonoBehaviour
{
    //�V�[���J�ڊ֌W
    [SerializeField] private string stage1Scene;
    [SerializeField] private string stage2Scene;
    [SerializeField] private string stageSelectScene;
    [SerializeField] private string menuScene;

    Button selectButton;
    public GameObject menuFlameUI;

    bool isWait = true;

    static public bool canGoal = false;

    //�t�F�[�h�A�E�g
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

        //�{�^���I�����
        if(selectButton != null)
        {
            selectButton = GameObject.Find("Canvas/Retry").GetComponent<Button>();
            selectButton.Select();
        }

        canGoal = false;

        //���j���[���O�b�͕\�����Ȃ��悤�ɂ���
        Invoke("ResultStart", 4.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //UI�\��
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

    //���g���C��
    public void Retry()
    {
        //���Z�b�g
        CountdownScript.canCountdown = true;
        canGoal = true;
        PlayerOtherController.rank = 0;

        //SE
        menuSEScript.NextSceneSE();

        //�t�F�[�h�A�E�g
        StartCoroutine(fadeOutScript.FadeOut());

        if (StageSlectControll.isStage1) //�V�[��1
            SceneManager.LoadScene(stage1Scene);
        else if (StageSlectControll.isStage2) //�V�[��2
            SceneManager.LoadScene(stage2Scene);
    }

    //�X�e�[�W�Z���N�g��
    public void StageSlect()
    {
        //���Z�b�g
        canGoal = true;
        PlayerOtherController.rank = 0;

        //SE
        menuSEScript.NextSceneSE();

        //�t�F�[�h�A�E�g
        StartCoroutine(fadeOutScript.FadeOut());

        SceneManager.LoadScene(stageSelectScene);
    }

    //���j���[��
    public void Menu()
    {
        //���Z�b�g
        canGoal = true;

        //���j���[��ʂɖ߂��Player�^�O�̃I�u�W�F�N�g������
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in objects)
        {
            Destroy(player);
        }

        //bool�̃��Z�b�g
        GetPlayer.isJoinPlayer1 = true;
        GetPlayer.isJoinPlayer2 = true;
        GetPlayer.isJoinPlayer3 = true;
        GetPlayer.isJoinPlayer4 = true;

        //SE
        menuSEScript.NextSceneSE();

        //�t�F�[�h�A�E�g
        StartCoroutine(fadeOutScript.FadeOut());

        SceneManager.LoadScene(menuScene);
    }

    //���j���[������
    public void Back()
    {
        PlayerOtherController.isDisplay = false;
        hasPlayedNextSE = false;
        audioSource.PlayOneShot(backSE);
    }
}
