using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltConveyorChangeScript : MonoBehaviour
{
    //ベルトコンベア
    public GameObject beforeBC;
    public GameObject afterBC;
    private GameObject childObj;

    //SE
    AudioSource changeSE;
    bool hasPlayedSE = false; //サウンド再生追跡

    // Start is called before the first frame update
    void Start()
    {
        beforeBC.gameObject.SetActive(true);
        afterBC.gameObject.SetActive(false);

        //子オブジェクト取得
        childObj = transform.GetChild(0).gameObject;

        changeSE = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //プレイヤーに触れるとベルトコンベアを逆向きにする
        if (collision.gameObject.CompareTag("Player"))
        {
            beforeBC.gameObject.SetActive(false);
            afterBC.gameObject.SetActive(true);

            childObj.GetComponent<SpriteRenderer>().flipX = false;

            //SE
            if (changeSE != null && !hasPlayedSE)
            {
                changeSE.Play();
                hasPlayedSE = true;
            }
        }
    }
}
