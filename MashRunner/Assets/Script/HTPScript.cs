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

        //���j���[�{�^���I����Ԏ擾
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

    //���̃y�[�W��
    public void ShowNextSprite()
    {
        if (currentIndex < sprites.Length - 1)
        {
            // ���̃C���f�b�N�X�Ɉړ�
            currentIndex = currentIndex + 1;

            // ���̃X�v���C�g��\��
            image.sprite = sprites[currentIndex];

            audioSource.PlayOneShot(nextSE);
        }
        else if (currentIndex == sprites.Length - 1)
        {
            //�Ō�̃X�v���C�g���ɓ��͂ŃV�[���؂�ւ�
            menuSEScript.NextSceneSE();
            SceneManager.LoadScene(nextScene);
        }
    }

    //�O�̃y�[�W��
    public void ShowPreviousSprite()
    {
        if (currentIndex > 0)
        {
            //�O�̃C���f�b�N�X�Ɉړ�
            currentIndex = currentIndex - 1;

            //�O�̃X�v���C�g��\��
            image.sprite = sprites[currentIndex];

            audioSource.PlayOneShot(backSE);
        }
        else if (currentIndex == 0)
        {
            //�Ō�̃X�v���C�g���ɓ��͂ŃV�[���؂�ւ�
            menuSEScript.BackSceneSE();
            SceneManager.LoadScene(nextScene);
        }
    }
}
