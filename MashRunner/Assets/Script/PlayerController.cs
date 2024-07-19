using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //基本動作
    [SerializeField] float moveSpeed;
    [SerializeField] float dashSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] double pushGauge;
    [SerializeField] double costGauge;
    [SerializeField] double fieldGauge;
    [SerializeField] float slidingBrake;
    [SerializeField] Vector2 slideOffset;
    [SerializeField] Vector2 slideSize;
    public double maxGauge = 100;
    public double chageGauge;
    float slideTime = 0;
    public bool isDash = false;
    public bool isSlide = false;
    public bool isJump = true;
    bool onGround = false;
    int onGraundNumber;

    //壁キック関係
    public bool canWallKick = false; //壁に接しているときtrue
    public bool isWallKick = false; //countWallKickが0の時true
    int countWallKick = 0;
    public bool wallKick = false;

    public Vector2 moveInput;
    Vector2 colliderOffset;
    Vector2 colliderSize;
    BoxCollider2D playerCollider;
    Rigidbody2D rb;
    [SerializeField] GameObject eff;

    [SerializeField] Slider gaude;//UIのスライダー
    public GameObject[] sliderUI = new GameObject[2];

    //ゴール関係
    bool isGool = false;

    //ジャンプ台関係
    float jumpDushSpeed = 1f;
    float defaltDushSpeed = 1f;
    [SerializeField] float additionDushSpeed;
    [SerializeField] float newJumpForce;
    bool isJumpDush = false;

    //ベルトコンベア関係
    float beltConveyorSpeed = 0f;
    float defaltConveyorSpeed = 0f;
    [SerializeField] float additionConveyorSpeed;
    bool isConveyorDush = false;
    bool isConveyorSlow = false;

    //サウンド関係
    AudioSource audioSource;
    public AudioClip jumpSE;
    public AudioClip dushSE;
    bool hasPlayedDushSE = false;
    public AudioClip slideSE;
    bool hasPlayedSlideSE;
    public AudioClip chageFieldSE;
    bool hasPlayedCFSE = false; //チャージフィールドSE
    public AudioClip goalSE;
    bool hasPlayedGoalSE = false;
    public AudioClip jumpDushSE;
    bool hasPlayedJDSE; //ジャンプダッシュSE

    //XボタンのUI
    Animator chageAnim = null;
    [SerializeField] GameObject xUI;

    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GetComponent<BoxCollider2D>();
        colliderOffset = playerCollider.offset;
        colliderSize = playerCollider.size;
        rb = GetComponent<Rigidbody2D>();
        eff.SetActive(false);
        chageGauge = 0;
        gaude.maxValue = (float)maxGauge;
        onGraundNumber = 0;

        audioSource = GetComponent<AudioSource>();

        chageAnim = xUI.GetComponent<Animator>();
    }

    //左右移動入力
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    //ジャンプ入力
    public void OnJump(InputAction.CallbackContext context)
    {
        if (canWallKick && isWallKick)
            WallKick();
        else if (isJump)
            Jump();
        Debug.Log(isJump);
    }

    //ダッシュ入力
    public void OnDash(InputAction.CallbackContext context)
    {
        isDash = true;
    }

    //ダッシュ入力解除
    public void OnWalk(InputAction.CallbackContext context)
    {
        isDash = false;
        hasPlayedDushSE = false;
    }

    //スライディング入力
    public void OnSlide(InputAction.CallbackContext context)
    {
        isSlide = true;
        playerCollider.offset = slideOffset;
        playerCollider.size = slideSize;
    }

    //スライディング入力解除
    public void OffSlide(InputAction.CallbackContext context)
    {
        isSlide = false;
        slideTime = 0;
        playerCollider.offset = colliderOffset;
        playerCollider.size = colliderSize;
        hasPlayedSlideSE = false;
    }

    //チャージ入力
    public void OnChage(InputAction.CallbackContext context)
    {
        if (onGround)
        {
            //アニメーション
            chageAnim.SetTrigger("X Anim");

            if (chageGauge <= maxGauge)
            {
                chageGauge += pushGauge / 3;
            }
            else
            {
                chageGauge = maxGauge;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "ResultScene" || SceneManager.GetActiveScene().name == "StageSelectScene" || SceneManager.GetActiveScene().name == "PlayerJoinScene")
        {
            audioSource.Stop();
            hasPlayedGoalSE = false;
        }

        if (isSlide)
        {
            slideTime += slidingBrake * Time.deltaTime;

            if (slideSE != null && !hasPlayedSlideSE)
            {
                audioSource.PlayOneShot(slideSE);
                hasPlayedSlideSE = true;
            }
        }

        if (isGool)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            if (isDash && chageGauge > 0)
            {
                if (dushSE != null && !hasPlayedDushSE)
                {
                    audioSource.PlayOneShot(dushSE);
                    hasPlayedDushSE = true;
                }

                if (moveInput.x > 0)
                {
                    if (moveInput.x * dashSpeed * (moveInput.x * dashSpeed - slideTime) > 0)
                    {
                        rb.velocity = new Vector2(moveInput.x * dashSpeed * jumpDushSpeed + beltConveyorSpeed - slideTime, rb.velocity.y);
                    }
                    else
                    {
                        rb.velocity = new Vector2(beltConveyorSpeed, rb.velocity.y);
                    }
                }
                else
                {
                    if (moveInput.x * dashSpeed * (moveInput.x * dashSpeed + slideTime) > 0)
                    {
                        rb.velocity = new Vector2(moveInput.x * dashSpeed * jumpDushSpeed + beltConveyorSpeed + slideTime, rb.velocity.y);
                    }
                    else
                    {
                        rb.velocity = new Vector2(beltConveyorSpeed, rb.velocity.y);
                    }
                }

                eff.SetActive(true);
                chageGauge -= costGauge * Time.deltaTime;
                if (chageGauge < 0)
                {
                    chageGauge = 0;
                }
            }
            else
            {
                if (moveInput.x > 0)
                {
                    if (moveInput.x * moveSpeed * (moveInput.x * moveSpeed - slideTime) > 0)
                    {
                        rb.velocity = new Vector2(moveInput.x * moveSpeed * jumpDushSpeed + beltConveyorSpeed - slideTime, rb.velocity.y);
                    }
                    else
                    {
                        rb.velocity = new Vector2(beltConveyorSpeed, rb.velocity.y);
                    }
                }
                else
                {
                    if (moveInput.x * moveSpeed * (moveInput.x * moveSpeed + slideTime) > 0)
                    {
                        rb.velocity = new Vector2(moveInput.x * moveSpeed * jumpDushSpeed + beltConveyorSpeed + slideTime, rb.velocity.y);
                    }
                    else
                    {
                        rb.velocity = new Vector2(beltConveyorSpeed, rb.velocity.y);
                    }
                }

                eff.SetActive(false);
            }
        }

        //壁キックが0以下か
        if (countWallKick <= 0)
            isWallKick = true;
        else
            isWallKick = false;

        //ジャンプ台
        if (isJumpDush) //ダッシュ
            jumpDushSpeed = additionDushSpeed;
        else if (!isJumpDush) //元に戻す
            jumpDushSpeed = defaltDushSpeed;
        else
            jumpDushSpeed = defaltDushSpeed;

        //ベルトコンベア
        if (isConveyorDush) //前進
            beltConveyorSpeed = additionConveyorSpeed;
        else if (isConveyorSlow) //後退
            beltConveyorSpeed = -additionConveyorSpeed;
        else if (!isConveyorDush || !isConveyorSlow) //元に戻す 
            beltConveyorSpeed = defaltConveyorSpeed;
        else
            beltConveyorSpeed = defaltConveyorSpeed;
        gaude.value = (float)chageGauge;

        //リザルト画面でゴール、チャージリセット
        if (SceneManager.GetActiveScene().name == "ResultScene")
        {
            isGool = false;
            chageGauge = 0;
        }

        //スライダーUIの表示切替
        sliderUI[0].gameObject.SetActive(chageGauge < maxGauge);
        sliderUI[1].gameObject.SetActive(chageGauge >= maxGauge);
    }

    //ジャンプ
    void Jump()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
        isJump = false;
        onGraundNumber = 0;

        audioSource.PlayOneShot(jumpSE);
    }

    void WallKick()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
        countWallKick++;
        wallKick = true;

        audioSource.PlayOneShot(jumpSE);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        //地面に触れた回数
        onGround = true;
        onGraundNumber++;

        // 地面に接触したらジャンプ可能にする
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.velocity = Vector2.zero;
            isJump = true;

            //地面に接触でスピードを元に戻す(ジャンプ台)
            isJumpDush = false;

            //地面に接触でスピードを元に戻す(ベルトコンベア)
            isConveyorDush = false;
            isConveyorSlow = false;

            //壁キックリセット
            countWallKick = 0;
        }

        //壁に接しているとき
        if (collision.gameObject.CompareTag("Wall"))
            canWallKick = true;

        //ジャンプ台に接触したらダッシュ＆強制ジャンプ
        if (collision.gameObject.CompareTag("Jump Stand"))
        {
            isJumpDush = true;
            rb.AddForce(new Vector2(0.0f, newJumpForce), ForceMode2D.Impulse);

            //SE
            if (goalSE != null && !hasPlayedJDSE)
            {
                audioSource.PlayOneShot(jumpDushSE);
                hasPlayedGoalSE = true;
            }
        }

        //ダッシュベルトコンベアに接触したら加速
        if (collision.gameObject.CompareTag("Dush Belt Conveyor"))
        {
            isJump = true;
            isConveyorDush = true;
            isConveyorSlow = false;
        }
        //スローベルトコンベアに接触したら減速
        else if (collision.gameObject.CompareTag("Slow Belt Conveyor"))
        {
            isJump = true;
            isConveyorDush = false;
            isConveyorSlow = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        //地面に触れた回数
        onGraundNumber--;
        if (onGraundNumber <= 0)
        {
            onGraundNumber = 0;
            onGround = false;
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            canWallKick = false;
            countWallKick = 0;
            wallKick = false;
        }

        //ダッシュベルトコンベアから離れるとスピードを元に戻す
        if (collision.gameObject.CompareTag("Dush Belt Conveyor"))
        {
            isConveyorDush = false;
        }
        //スローベルトコンベアから離れるとスピードを元に戻す
        else if (collision.gameObject.CompareTag("Slow Belt Conveyor"))
        {
            isConveyorSlow = false;
        }

        //ジャンプ台から離れたら再生できる
        if (collision.gameObject.CompareTag("Jump Stand"))
        {
            hasPlayedGoalSE = false;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        //加速チャージフィールド(仮称)範囲内にいるとチャージされる
        if (collision.gameObject.CompareTag("ChageField"))
        {
            if (chageGauge <= maxGauge)
            {
                //SE
                if (chageFieldSE != null && !hasPlayedCFSE)
                {
                    audioSource.PlayOneShot(chageFieldSE);
                    hasPlayedCFSE = true;
                }

                chageGauge += fieldGauge * Time.deltaTime; ;
            }
            else
            {
                chageGauge = maxGauge;
            }
        }

        //ゴールに到達
        if (collision.gameObject.CompareTag("Goal"))
        {
            isGool = true;

            //SE
            if (goalSE != null && !hasPlayedGoalSE)
            {
                audioSource.PlayOneShot(goalSE);
                hasPlayedGoalSE = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        //チャージフィールドから離れるとSEを鳴らせる
        if (collision.gameObject.CompareTag("ChageField"))
        {
            hasPlayedCFSE = false;
        }
    }
}
