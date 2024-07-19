using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HTPScript : MonoBehaviour
{
    public Sprite[] sprites;
    int currentIndex = 0;
    private Image image;

    [SerializeField] private string nextScene = null;

    //SE
    AudioSource audioSource;
    public AudioClip nextSE;
    public AudioClip backSE;
    public MenuSEScript menuSEScript;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();

        //メニューボタン選択状態取得
        MenuControll.isHTP = true;

        if (sprites.Length > 0)
        {
            image.sprite = sprites[currentIndex];
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    //次のページへ
    public void ShowNextSprite()
    {
        if (currentIndex < sprites.Length - 1)
        {
            // 次のインデックスに移動
            currentIndex = currentIndex + 1;

            // 次のスプライトを表示
            image.sprite = sprites[currentIndex];

            audioSource.PlayOneShot(nextSE);
        }
        else if (currentIndex == sprites.Length - 1)
        {
            //最後のスプライト時に入力でシーン切り替え
            menuSEScript.NextSceneSE();
            SceneManager.LoadScene(nextScene);
        }
    }

    //前のページへ
    public void ShowPreviousSprite()
    {
        if (currentIndex > 0)
        {
            //前のインデックスに移動
            currentIndex = currentIndex - 1;

            //前のスプライトを表示
            image.sprite = sprites[currentIndex];

            audioSource.PlayOneShot(backSE);
        }
        else if (currentIndex == 0)
        {
            //最後のスプライト時に入力でシーン切り替え
            menuSEScript.BackSceneSE();
            SceneManager.LoadScene(nextScene);
        }
    }
}
