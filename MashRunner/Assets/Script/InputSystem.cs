using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class InputSystem : MonoBehaviour
{
    //��{����
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    bool isJump = true;
    Vector2 moveInput;
    Rigidbody2D rb;

    //�S�[���֌W
    bool isGool = false;
    public TextMeshProUGUI textRankingUGUI;
    public static int rank;

    //�W�����v��֌W
    [SerializeField] float jumpDushSpeed;
    [SerializeField] float defaltDushSpeed;
    [SerializeField] float additionDushSpeed;
    [SerializeField] float newJumpForce;
    bool isJumpDush = false;

    //�x���g�R���x�A�֌W
    [SerializeField] float beltConveyorSpeed;
    [SerializeField] float defaltConveyorSpeed;
    [SerializeField] float additionConveyorSpeed;
    bool isConveyorDush = false;
    bool isConveyorSlow = false;

    //�V�[���J�ڊ֌W
    [SerializeField] private string stageSelectScene;
    [SerializeField] private string menuScene;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //�I�u�W�F�N�g�ێ�
        DontDestroyOnLoad(this.gameObject);
    }

    //���E�ړ�����
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    //�W�����v����
    public void OnJump(InputAction.CallbackContext context)
    {
        if (isJump)
            Jump();
    }

    //�}�b�v�I����ʂ�
    public void OnReady(InputAction.CallbackContext context)
    {
        //�v���C���[�Q�����
        if (SceneManager.GetActiveScene().name == "PlayerJoinScene")
            SceneManager.LoadScene(stageSelectScene);
    }

    //���j���[��ʂ�
    public void OnBack(InputAction.CallbackContext context)
    {
        //�v���C���[�Q����ʂ̎�
        if (SceneManager.GetActiveScene().name == "PlayerJoinScene")
        {
            SceneManager.LoadScene(menuScene);
            //���j���[��ʂɖ߂��Player�^�O�̃I�u�W�F�N�g������
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in objects)
            {
                Destroy(player);
            }
            //bool�̃��Z�b�g
            GetPlayer.isJoinPlayer1 = true;
            GetPlayer.isJoinPlayer2 = true;
            GetPlayer.isJoinPlayer3 = true;
            GetPlayer.isJoinPlayer4 = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGool) //�S�[���ɓ��B�ő���s�\��
            rb.velocity = Vector2.zero;
        else
            rb.velocity = new Vector2(moveInput.x * moveSpeed * jumpDushSpeed + beltConveyorSpeed, rb.velocity.y);

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
    }

    //�W�����v
    void Jump()
    {
        rb.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
        isJump = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // �n�ʂɐڐG������W�����v�\�ɂ���
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJump = true;

            //�n�ʂɐڐG�ŃX�s�[�h�����ɖ߂�(�W�����v��)
            isJumpDush = false;
            
            //�n�ʂɐڐG�ŃX�s�[�h�����ɖ߂�(�x���g�R���x�A)
            isConveyorDush = false;
            isConveyorSlow = false;
        }

        //�W�����v��ɐڐG������_�b�V���������W�����v
        if (collision.gameObject.CompareTag("Jump Stand"))
        {
            isJumpDush = true;
            rb.AddForce(new Vector2(0.0f, newJumpForce), ForceMode2D.Impulse);
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        //�S�[���ɓ��B
        if(collision.gameObject.CompareTag("Goal"))
        {
            isGool = true;
            //�����L���O�\��
            rank = GoalScript.goalPlayerCount + 1;
            textRankingUGUI.text = "Goal!!";
        }
    }
}