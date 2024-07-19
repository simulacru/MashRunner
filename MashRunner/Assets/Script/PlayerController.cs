using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //��{����
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

    //�ǃL�b�N�֌W
    public bool canWallKick = false; //�ǂɐڂ��Ă���Ƃ�true
    public bool isWallKick = false; //countWallKick��0�̎�true
    int countWallKick = 0;
    public bool wallKick = false;

    public Vector2 moveInput;
    Vector2 colliderOffset;
    Vector2 colliderSize;
    BoxCollider2D playerCollider;
    Rigidbody2D rb;
    [SerializeField] GameObject eff;

    [SerializeField] Slider gaude;//UI�̃X���C�_�[
    public GameObject[] sliderUI = new GameObject[2];

    //�S�[���֌W
    bool isGool = false;

    //�W�����v��֌W
    float jumpDushSpeed = 1f;
    float defaltDushSpeed = 1f;
    [SerializeField] float additionDushSpeed;
    [SerializeField] float newJumpForce;
    bool isJumpDush = false;

    //�x���g�R���x�A�֌W
    float beltConveyorSpeed = 0f;
    float defaltConveyorSpeed = 0f;
    [SerializeField] float additionConveyorSpeed;
    bool isConveyorDush = false;
    bool isConveyorSlow = false;

    //�T�E���h�֌W
    AudioSource audioSource;
    public AudioClip jumpSE;
    public AudioClip dushSE;
    bool hasPlayedDushSE = false;
    public AudioClip slideSE;
    bool hasPlayedSlideSE;
    public AudioClip chageFieldSE;
    bool hasPlayedCFSE = false; //�`���[�W�t�B�[���hSE
    public AudioClip goalSE;
    bool hasPlayedGoalSE = false;
    public AudioClip jumpDushSE;
    bool hasPlayedJDSE; //�W�����v�_�b�V��SE

    //X�{�^����UI
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

    //���E�ړ�����
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    //�W�����v����
    public void OnJump(InputAction.CallbackContext context)
    {
        if (canWallKick && isWallKick)
            WallKick();
        else if (isJump)
            Jump();
        Debug.Log(isJump);
    }

    //�_�b�V������
    public void OnDash(InputAction.CallbackContext context)
    {
        isDash = true;
    }

    //�_�b�V�����͉���
    public void OnWalk(InputAction.CallbackContext context)
    {
        isDash = false;
        hasPlayedDushSE = false;
    }

    //�X���C�f�B���O����
    public void OnSlide(InputAction.CallbackContext context)
    {
        isSlide = true;
        playerCollider.offset = slideOffset;
        playerCollider.size = slideSize;
    }

    //�X���C�f�B���O���͉���
    public void OffSlide(InputAction.CallbackContext context)
    {
        isSlide = false;
        slideTime = 0;
        playerCollider.offset = colliderOffset;
        playerCollider.size = colliderSize;
        hasPlayedSlideSE = false;
    }

    //�`���[�W����
    public void OnChage(InputAction.CallbackContext context)
    {
        if (onGround)
        {
            //�A�j���[�V����
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

        //�ǃL�b�N��0�ȉ���
        if (countWallKick <= 0)
            isWallKick = true;
        else
            isWallKick = false;

        //�W�����v��
        if (isJumpDush) //�_�b�V��
            jumpDushSpeed = additionDushSpeed;
        else if (!isJumpDush) //���ɖ߂�
            jumpDushSpeed = defaltDushSpeed;
        else
            jumpDushSpeed = defaltDushSpeed;

        //�x���g�R���x�A
        if (isConveyorDush) //�O�i
            beltConveyorSpeed = additionConveyorSpeed;
        else if (isConveyorSlow) //���
            beltConveyorSpeed = -additionConveyorSpeed;
        else if (!isConveyorDush || !isConveyorSlow) //���ɖ߂� 
            beltConveyorSpeed = defaltConveyorSpeed;
        else
            beltConveyorSpeed = defaltConveyorSpeed;
        gaude.value = (float)chageGauge;

        //���U���g��ʂŃS�[���A�`���[�W���Z�b�g
        if (SceneManager.GetActiveScene().name == "ResultScene")
        {
            isGool = false;
            chageGauge = 0;
        }

        //�X���C�_�[UI�̕\���ؑ�
        sliderUI[0].gameObject.SetActive(chageGauge < maxGauge);
        sliderUI[1].gameObject.SetActive(chageGauge >= maxGauge);
    }

    //�W�����v
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
        //�n�ʂɐG�ꂽ��
        onGround = true;
        onGraundNumber++;

        // �n�ʂɐڐG������W�����v�\�ɂ���
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.velocity = Vector2.zero;
            isJump = true;

            //�n�ʂɐڐG�ŃX�s�[�h�����ɖ߂�(�W�����v��)
            isJumpDush = false;

            //�n�ʂɐڐG�ŃX�s�[�h�����ɖ߂�(�x���g�R���x�A)
            isConveyorDush = false;
            isConveyorSlow = false;

            //�ǃL�b�N���Z�b�g
            countWallKick = 0;
        }

        //�ǂɐڂ��Ă���Ƃ�
        if (collision.gameObject.CompareTag("Wall"))
            canWallKick = true;

        //�W�����v��ɐڐG������_�b�V���������W�����v
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

        //�_�b�V���x���g�R���x�A�ɐڐG���������
        if (collision.gameObject.CompareTag("Dush Belt Conveyor"))
        {
            isJump = true;
            isConveyorDush = true;
            isConveyorSlow = false;
        }
        //�X���[�x���g�R���x�A�ɐڐG�����猸��
        else if (collision.gameObject.CompareTag("Slow Belt Conveyor"))
        {
            isJump = true;
            isConveyorDush = false;
            isConveyorSlow = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        //�n�ʂɐG�ꂽ��
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

        //�_�b�V���x���g�R���x�A���痣���ƃX�s�[�h�����ɖ߂�
        if (collision.gameObject.CompareTag("Dush Belt Conveyor"))
        {
            isConveyorDush = false;
        }
        //�X���[�x���g�R���x�A���痣���ƃX�s�[�h�����ɖ߂�
        else if (collision.gameObject.CompareTag("Slow Belt Conveyor"))
        {
            isConveyorSlow = false;
        }

        //�W�����v�䂩�痣�ꂽ��Đ��ł���
        if (collision.gameObject.CompareTag("Jump Stand"))
        {
            hasPlayedGoalSE = false;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        //�����`���[�W�t�B�[���h(����)�͈͓��ɂ���ƃ`���[�W�����
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

        //�S�[���ɓ��B
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
        //�`���[�W�t�B�[���h���痣����SE��点��
        if (collision.gameObject.CompareTag("ChageField"))
        {
            hasPlayedCFSE = false;
        }
    }
}
