using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class InputSystem : MonoBehaviour
{
    //基本動作
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    bool isJump = true;
    Vector2 moveInput;
    Rigidbody2D rb;

    //ゴール関係
    bool isGool = false;
    public TextMeshProUGUI textRankingUGUI;
    public static int rank;

    //ジャンプ台関係
    [SerializeField] float jumpDushSpeed;
    [SerializeField] float defaltDushSpeed;
    [SerializeField] float additionDushSpeed;
    [SerializeField] float newJumpForce;
    bool isJumpDush = false;

    //ベルトコンベア関係
    [SerializeField] float beltConveyorSpeed;
    [SerializeField] float defaltConveyorSpeed;
    [SerializeField] float additionConveyorSpeed;
    bool isConveyorDush = false;
    bool isConveyorSlow = false;

    //シーン遷移関係
    [SerializeField] private string stageSelectScene;
    [SerializeField] private string menuScene;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //オブジェクト保持
        DontDestroyOnLoad(this.gameObject);
    }

    //左右移動入力
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    //ジャンプ入力
    public void OnJump(InputAction.CallbackContext context)
    {
        if (isJump)
            Jump();
    }

    //マップ選択画面へ
    public void OnReady(InputAction.CallbackContext context)
    {
        //プレイヤー参加画面
        if (SceneManager.GetActiveScene().name == "PlayerJoinScene")
            SceneManager.LoadScene(stageSelectScene);
    }

    //メニュー画面へ
    public void OnBack(InputAction.CallbackContext context)
    {
        //プレイヤー参加画面の時
        if (SceneManager.GetActiveScene().name == "PlayerJoinScene")
        {
            SceneManager.LoadScene(menuScene);
            //メニュー画面に戻るとPlayerタグのオブジェクトを消す
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in objects)
            {
                Destroy(player);
            }
            //boolのリセット
            GetPlayer.isJoinPlayer1 = true;
            GetPlayer.isJoinPlayer2 = true;
            GetPlayer.isJoinPlayer3 = true;
            GetPlayer.isJoinPlayer4 = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGool) //ゴールに到達で操作不能に
            rb.velocity = Vector2.zero;
        else
            rb.velocity = new Vector2(moveInput.x * moveSpeed * jumpDushSpeed + beltConveyorSpeed, rb.velocity.y);

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
    }

    //ジャンプ
    void Jump()
    {
        rb.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
        isJump = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 地面に接触したらジャンプ可能にする
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJump = true;

            //地面に接触でスピードを元に戻す(ジャンプ台)
            isJumpDush = false;
            
            //地面に接触でスピードを元に戻す(ベルトコンベア)
            isConveyorDush = false;
            isConveyorSlow = false;
        }

        //ジャンプ台に接触したらダッシュ＆強制ジャンプ
        if (collision.gameObject.CompareTag("Jump Stand"))
        {
            isJumpDush = true;
            rb.AddForce(new Vector2(0.0f, newJumpForce), ForceMode2D.Impulse);
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        //ゴールに到達
        if(collision.gameObject.CompareTag("Goal"))
        {
            isGool = true;
            //ランキング表示
            rank = GoalScript.goalPlayerCount + 1;
            textRankingUGUI.text = "Goal!!";
        }
    }
}