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

    //���U���g�̃��g���C�p
    public static bool isStage1 = false;
    public static bool isStage2 = false;

    Button selectButton;

    public MenuSEScript menuSEScript;
    public FadeOutScript fadeOutScript;

    // Start is called before the first frame update
    void Start()
    {
        //�{�^���I�����
        selectButton = GameObject.Find("Canvas/Stage1").GetComponent<Button>();
        selectButton.Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�X�e�[�W1��
    public void Stage1()
    {
        //BGM��~
        TitleBGMPlayer.soundStop = true;
        menuSEScript.NextSceneSE(); //SE

        //�J�E���g�_�E�����Z�b�g
        CountdownScript.canCountdown = true;

        isStage1 = true;
        isStage2 = false;

        //�t�F�[�h�A�E�g
        StartCoroutine(fadeOutScript.FadeOut());

        SceneManager.LoadScene(stage1Scene);
    }

    //�X�e�[�W2��
    public void Stage2()
    {
        //BGM��~
        TitleBGMPlayer.soundStop = true;
        menuSEScript.NextSceneSE(); //SE

        //�J�E���g�_�E�����Z�b�g
        CountdownScript.canCountdown = true;

        isStage1 = false;
        isStage2 = true;

        //�t�F�[�h�A�E�g
        StartCoroutine(fadeOutScript.FadeOut());

        SceneManager.LoadScene(stage2Scene);
    }

    //�O�̃V�[����
    public void BackScene()
    {
        //SE
        menuSEScript.BackSceneSE();

        isStage1 = false;
        isStage2 = false;;

        SceneManager.LoadScene(prevScene);
    }
}
