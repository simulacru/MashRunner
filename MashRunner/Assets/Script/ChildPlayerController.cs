using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChildPlayerController : MonoBehaviour
{
    //�e�I�u�W�F�N�g�擾
    GameObject playerParent;
    PlayerController playerController;
    PlayerOtherController playerOtherController;

    //�A�j���[�V�����֌W
    Animator anim = null;
    Vector2 moveInput;
    bool isJump;
    bool isSlide;
    bool isDash;
    double chageGauge;
    bool isGoal;
    Vector2 current;

    //�R���C�_�[�֌W
    BoxCollider2D playerCollider;
    Vector2 colliderOffset;
    Vector2 colliderSize;

    //�X���C�f�B���O
    [SerializeField] Vector2 slideOffset;
    [SerializeField] Vector2 slideSize;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerCollider = GetComponent<BoxCollider2D>();

        colliderOffset = playerCollider.offset;
        colliderSize = playerCollider.size;

        //�e�I�u�W�F�N�g�擾
        playerParent = transform.parent.gameObject;
        playerController = playerParent.GetComponent<PlayerController>();
        playerOtherController = playerParent.GetComponent<PlayerOtherController>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = playerController.moveInput;
        isJump = playerController.isJump;
        isSlide = playerController.isSlide;
        isDash = playerController.isDash;
        chageGauge = playerController.chageGauge;

        isGoal = playerOtherController.isGoal;
        current = transform.position;

        //�ړ�
        anim.SetBool("Move Anim", moveInput.x != 0 || (isGoal && current.x < 1060)); //�S�[����
        if (moveInput.x < 0.0f)
            this.GetComponent<SpriteRenderer>().flipX = true;
        else if (moveInput.x > 0.0f)
            this.GetComponent<SpriteRenderer>().flipX = false;

        //�W�����v
        if (!isJump)
            anim.SetBool("Jump Anim", true);
        else if (isJump)
            anim.SetBool("Jump Anim", false);

        //�X���C�f�B���O
        if (isSlide)
        {
            anim.SetBool("Slide Anim", isSlide);
            playerCollider.offset = slideOffset;
            playerCollider.size = slideSize;
        }
        else if (!isSlide)
        {
            anim.SetBool("Slide Anim", isSlide);
            playerCollider.offset = colliderOffset;
            playerCollider.size = colliderSize;
        }

        //���x�ω�
        if (isJump && isDash && chageGauge > 0 && anim.speed != 1.5f)
            anim.speed = 1.5f;
        else if (anim.speed != 1.0f)
            anim.speed = 1.0f;
    }
}
