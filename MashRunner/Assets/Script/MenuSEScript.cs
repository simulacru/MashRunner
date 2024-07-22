using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSEScript : MonoBehaviour
{
    new AudioSource audio;
    public AudioClip menuNextSE;
    public AudioClip menuBackSE;
    public static bool soundStop = false;

    // Start is called before the first frame update
    void Start()
    {
        //�V�[���؂�ւ����ɉ�����悤�ɕۑ�
        DontDestroyOnLoad(this.gameObject);

        audio = gameObject.GetComponent<AudioSource>();
    }

    //���̃V�[����
    public void NextSceneSE()
    {
        audio.PlayOneShot(menuNextSE);
        //�������Ă���폜
        Invoke("DeleteObj", 2.0f);
    }

    //�O�̃V�[����
    public void BackSceneSE()
    {
        audio.PlayOneShot(menuBackSE);
        //�������Ă���폜
        Invoke("DeleteObj", 2.0f);
    }

    //�I�u�W�F�N�g�폜
    void DeleteObj()
    {
        Destroy(this.gameObject);
    }
}