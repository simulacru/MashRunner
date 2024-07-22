using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChildPlayerController : MonoBehaviour
{
    //親オブジェクト取得
    GameObject playerParent;
    PlayerController playerController;
    PlayerOtherController playerOtherController;

    //アニメーション関係
    Animator anim = null;
    Vector2 moveInput;
    bool isJump;
    bool isSlide;
    bool isDash;
    double chageGauge;
    bool isGoal;
    Vector2 current;

    //コライダー関係
    BoxCollider2D playerCollider;
    Vector2 colliderOffset;
    Vector2 colliderSize;

    //スライディング
    [SerializeField] Vector2 slideOffset;
    [SerializeField] Vector2 slideSize;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerCollider = GetComponent<BoxCollider2D>();

        colliderOffset = playerCollider.offset;
        colliderSize = playerCollider.size;

        //親オブジェクト取得
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

        //移動
        anim.SetBool("Move Anim", moveInput.x != 0 || (isGoal && current.x < 1060)); //ゴール後
        if (moveInput.x < 0.0f)
            this.GetComponent<SpriteRenderer>().flipX = true;
        else if (moveInput.x > 0.0f)
            this.GetComponent<SpriteRenderer>().flipX = false;

        //ジャンプ
        if (!isJump)
            anim.SetBool("Jump Anim", true);
        else if (isJump)
            anim.SetBool("Jump Anim", false);

        //スライディング
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

        //速度変化
        if (isJump && isDash && chageGauge > 0 && anim.speed != 1.5f)
            anim.speed = 1.5f;
        else if (anim.speed != 1.0f)
            anim.speed = 1.0f;
    }
}
