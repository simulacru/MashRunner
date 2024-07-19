using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideDoorSwitchScript : MonoBehaviour
{
    [SerializeField] float targetX;
    [SerializeField] float targetY;
    [SerializeField] float moveSpeed;
    public GameObject targetDoor;
    bool isMoveDoor = false;
    private GameObject childObj;

    //SE
    AudioSource onSE;
    bool hasPlayedSE = false; //�T�E���h�Đ��ǐ�

    // Start is called before the first frame update
    void Start()
    {
        onSE = GetComponent<AudioSource>();

        //�q�I�u�W�F�N�g�擾
        childObj = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoveDoor)
        {
            MoveDoor();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {   
        //�v���C���[�ɐڐG����ƃh�A������
        if(collision.gameObject.CompareTag("Player"))
        {
            isMoveDoor = true;
            childObj.GetComponent<SpriteRenderer>().flipX = false; //���]

            if (onSE != null && !hasPlayedSE)
            {
                onSE.Play();
                hasPlayedSE = true;
            }
        }
    }

    //�h�A�𓮂�������
    public void MoveDoor()
    {
        if(targetDoor != null)
        {
            Vector2 current = targetDoor.transform.position;
            Vector2 target = new Vector2(targetX, targetY);
            float step = moveSpeed * Time.deltaTime;
            targetDoor.transform.position = Vector2.MoveTowards(current, target, step);
        }
    }
}
