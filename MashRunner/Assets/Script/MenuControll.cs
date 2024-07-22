using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuControll : MonoBehaviour
{
    //�V�[��
    [SerializeField] private string playScene = null;
    [SerializeField] private string HTPScene = null; //How to Play
    [SerializeField] private string soundScene = null;
    [SerializeField] private string titleScene = null;

    //�{�^��
    Button playButton;
    Button HTPButton;
    Button soundButton;

    //�I����ԋL��
    public static bool isSound = false;
    public static bool isHTP = false;

    //�t�F�[�h�A�E�g
    public FadeOutScript fadeOutScript;

    //�V�[���؂�ւ��pSE
    public MenuSEScript menuSEScript;

    // Start is called before the first frame update
    void Start()
    {
        //���j���[��ʂɖ߂����ۂɌ��肵���{�^����I����Ԃɂ���
        if (isSound)
        {
            //�{�^����I��������Ԃɂ���
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

        //���Z�b�g
        PlayerCameraScript.joinPlayerCount = 0;
        StageSlectControll.isStage1 = false;
        StageSlectControll.isStage2 = false;
        PlayerOtherController.isReady = false;
        PlayerOtherController.isBack = false;
    }

    //�v���C���[�Q����ʂ�
    public void OnPlay()
    {
        isHTP = false;
        isSound = false;

        menuSEScript.NextSceneSE();
        SceneManager.LoadScene(playScene);
    }

    //�V�ѕ���ʂ�
    public void OnHTP()
    {
        isHTP = true;

        menuSEScript.NextSceneSE();
        SceneManager.LoadScene(HTPScene);
    }

    //�T�E���h�ݒ��ʂ�
    public void OnSound()
    {
        isSound = true;

        menuSEScript.NextSceneSE();
        SceneManager.LoadScene(soundScene);
    }

    //�^�C�g����
    public void BackScene()
    {
        isHTP = false;
        isSound = false;

        menuSEScript.BackSceneSE();
        //�t�F�[�h�A�E�g
        StartCoroutine(fadeOutScript.FadeOut());
        SceneManager.LoadScene(titleScene);
    }
}
