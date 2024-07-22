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

    //サウンド
    AudioSource audioSorce;
    public AudioClip countdownSE;
    bool hasPlayedSE = false; //サウンド再生追跡

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.enabled = false;
        Debug.Log($"カウントダウン状態: {canCountdown}");

        audioSorce = GetComponent<AudioSource>();
        CountSECount();
    }

    // Update is called once per frame
    void Update()
    {
        if (canCountdown)
            Countdown();

        //カウントダウンリセット
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
                //countdown = iのとき対応するスプライトを表示
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
        //プレイヤー1以外ミュートにする
        if (!GetPlayer.isJoinPlayer4 || !GetPlayer.isJoinPlayer3 || !GetPlayer.isJoinPlayer2)
            audioSorce.mute = true;
        else if (!GetPlayer.isJoinPlayer1)
            audioSorce.mute = false;
    }
}
