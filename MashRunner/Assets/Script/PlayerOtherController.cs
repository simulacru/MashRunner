using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class PlayerOtherController : MonoBehaviour
{
    //�V�[���J�ڊ֌W
    [SerializeField] private string stageSelectScene = null;
    [SerializeField] private string menuScene = null;
    [SerializeField] private string playerJoinScene = null;

    //�v���C���[�Q���m�F�p
    public static bool isReady = false;
    public static bool isBack = false;

    //�S�[���֌W
    public GameObject goalBackground;
    public static int rank;
    public static int[] rankArray = new int[4];
    public bool isGoal = false;
    public bool hasGoal = false;
    bool[] playerArray = new bool[4] { false, false, false, false };

    //�p�[�e�B�N���֌W
    [SerializeField] ParticleSystem particle;
    [SerializeField] Color[] colorArray = new Color[4];

    //���j���[�{�^���̕\��
    public static bool isDisplay = false;

    //�L�����N�^�[�A�C�R��
    public Sprite[] characterUIArray;
    public Image characterImage;

    //�I�v�V����
    public GameObject optionOn;
    public GameObject optionOff;
    bool isDisplayOpt = true;

    //UI�\���ʒu
    public GameObject mainUI;

    // Start is called before the first frame update
    void Start()
    {
        //�I�u�W�F�N�g�ێ�
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
        //��l�v���C����UI�̈ʒu��ύX
        if (2 <= PlayerCameraScript.joinPlayerCount && PlayerCameraScript.joinPlayerCount < 3)
        {
            mainUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(-520, 0);
        }
        else
        {
            mainUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(-220, 0);
        }

        //�S�[��������ɏ����E�ֈړ�
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

        //�I�v�V�����\���ؑ�
        optionOn.gameObject.SetActive(isDisplayOpt);
        optionOff.gameObject.SetActive(!isDisplayOpt);

        //if (SceneManager.GetActiveScene().name == "MenuScene")
        //    Destroy(gameObject);
    }

    //�}�b�v�I����ʂ�
    public void OnReady(InputAction.CallbackContext context)
    {
        //�m�FUI��\��
        if (SceneManager.GetActiveScene().name == "PlayerJoinScene")
        {
            isReady = true;
        }  
    }

    //���j���[��ʂ�
    public void OnBack(InputAction.CallbackContext context)
    {
        //�v���C���[�Q����ʂ̎�
        if (SceneManager.GetActiveScene().name == "PlayerJoinScene" && !isReady)
        {
            isBack = true;
            SceneManager.LoadScene(menuScene);
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
        }
        //���U���g��ʂ̎�
        else if (SceneManager.GetActiveScene().name == "ResultScene")
        {
            isDisplay = false;
        }
    }

    public void OnSelect(InputAction.CallbackContext context)
    {
        //���U���g��ʂ̎�
        if (SceneManager.GetActiveScene().name == "ResultScene")
        {
            isDisplay = true;
            hasGoal = false;
        }
    }

    //�I�v�V�����\���ؑ�
    public void Option(InputAction.CallbackContext context)
    {
        isDisplayOpt = !isDisplayOpt;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //�S�[���ɓ��B
        if (collision.gameObject.CompareTag("Goal") && !hasGoal)
        {
            isGoal = true;

            //�����L���O
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

        //�v���C���[�ɂ���ĕς���
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
        //�X�^�[�g�ʒu�����Z�b�g
        if (ResultMenuScript.canGoal)
        {
            transform.position = new Vector3(0.0f, -3.0f, 0.0f);
            isDisplay = false;
            isDisplayOpt = true;
            isGoal = false;

            //Goal�w�i���Z�b�g
            goalBackground.SetActive(false);
        }
    }
}
