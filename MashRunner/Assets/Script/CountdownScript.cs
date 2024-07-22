using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountdownScript : MonoBehaviour
{
    [SerializeField] float countdown = 6.0f;
    public Sprite[] countdownArray;
    private Image image;
    private bool isCountdown = false;
    public static bool canCountdown = false;

    //�T�E���h
    AudioSource audioSorce;
    public AudioClip countdownSE;
    bool hasPlayedSE = false; //�T�E���h�Đ��ǐ�

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.enabled = false;
        Debug.Log($"�J�E���g�_�E�����: {canCountdown}");

        audioSorce = GetComponent<AudioSource>();
        CountSECount();
    }

    // Update is called once per frame
    void Update()
    {
        if (canCountdown)
            Countdown();

        //�J�E���g�_�E�����Z�b�g
        if (SceneManager.GetActiveScene().name == "ResultScene" || SceneManager.GetActiveScene().name == "StageSelectScene" || SceneManager.GetActiveScene().name == "PlayerJoinScene")
        {
            countdown = 6.0f;
            hasPlayedSE = false;
        }
    }

    void Countdown()
    {
        if (countdownSE != null && !hasPlayedSE && (int)countdown == 3)
        {
            audioSorce.PlayOneShot(countdownSE);
            hasPlayedSE = true;
        }

        isCountdown = false;
        if (countdown >= 0 && canCountdown)
        {
            countdown -= Time.deltaTime;

            for (int i = 0; i < countdownArray.Length; i++)
            {
                //countdown = i�̂Ƃ��Ή�����X�v���C�g��\��
                if ((int)countdown == i)
                {
                    image.enabled = true;
                    image.sprite = countdownArray[i];
                    isCountdown = true;
                    break;
                }
            }
        }
        if (!isCountdown)
            image.enabled = false;
    }

    //SE
    void CountSECount()
    {
        //�v���C���[1�ȊO�~���[�g�ɂ���
        if (!GetPlayer.isJoinPlayer4 || !GetPlayer.isJoinPlayer3 || !GetPlayer.isJoinPlayer2)
            audioSorce.mute = true;
        else if (!GetPlayer.isJoinPlayer1)
            audioSorce.mute = false;
    }
}
