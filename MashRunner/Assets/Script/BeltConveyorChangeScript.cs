using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltConveyorChangeScript : MonoBehaviour
{
    //�x���g�R���x�A
    public GameObject beforeBC;
    public GameObject afterBC;
    private GameObject childObj;

    //SE
    AudioSource changeSE;
    bool hasPlayedSE = false; //�T�E���h�Đ��ǐ�

    // Start is called before the first frame update
    void Start()
    {
        beforeBC.gameObject.SetActive(true);
        afterBC.gameObject.SetActive(false);

        //�q�I�u�W�F�N�g�擾
        childObj = transform.GetChild(0).gameObject;

        changeSE = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //�v���C���[�ɐG���ƃx���g�R���x�A���t�����ɂ���
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
