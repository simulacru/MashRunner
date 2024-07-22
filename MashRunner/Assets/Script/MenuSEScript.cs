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
        //シーン切り替え時に音が鳴るように保存
        DontDestroyOnLoad(this.gameObject);

        audio = gameObject.GetComponent<AudioSource>();
    }

    //次のシーンへ
    public void NextSceneSE()
    {
        audio.PlayOneShot(menuNextSE);
        //音が鳴ってから削除
        Invoke("DeleteObj", 2.0f);
    }

    //前のシーンへ
    public void BackSceneSE()
    {
        audio.PlayOneShot(menuBackSE);
        //音が鳴ってから削除
        Invoke("DeleteObj", 2.0f);
    }

    //オブジェクト削除
    void DeleteObj()
    {
        Destroy(this.gameObject);
    }
}