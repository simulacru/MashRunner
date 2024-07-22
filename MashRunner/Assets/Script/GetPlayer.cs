using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GetPlayer : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;

    //各プレイヤーがアクティブできるか
    public static bool isJoinPlayer1 = true;
    public static bool isJoinPlayer2 = true;
    public static bool isJoinPlayer3 = true;
    public static bool isJoinPlayer4 = true;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        Userindex();

        if (playerInput == null)
            return;

        // プレイヤーはアクティブかどうかチェック
        if (!playerInput.user.valid)
        {
            Debug.Log("アクティブなプレイヤーではありません");
            return;
        }

        // プレイヤー番号をログ出力
        Debug.Log($"===== プレイヤー#{playerInput.user.index} =====");
    }

    void Userindex()
    {
        //タグを取得
        int playerCount = playerInput.user.index;

        //子オブジェクトを取得
        GameObject player1 = transform.GetChild(0).gameObject;
        GameObject player2 = transform.GetChild(1).gameObject;
        GameObject player3 = transform.GetChild(2).gameObject;
        GameObject player4 = transform.GetChild(3).gameObject;

        //各boolがtrueの場合、各プレイヤーをアクティブに設定
        if (isJoinPlayer4)
            player4.SetActive(2 < playerCount && playerCount <= 3);
        if (isJoinPlayer3)
            player3.SetActive(1 < playerCount && playerCount <= 2);
        if (isJoinPlayer2)
            player2.SetActive(0 < playerCount && playerCount <= 1);
        if (isJoinPlayer1)
            player1.SetActive(-1 < playerCount && playerCount <= 0);

        //各プレイヤーがが存在するかをチェックして、存在する場合はisJoinplayerをfalseに設定
        if (player1.activeSelf)
        {
            isJoinPlayer1 = false;
            Debug.Log("Player1参加");
        }
        if (player2.activeSelf)
        {
            isJoinPlayer2 = false;
            Debug.Log("Player2参加");
        } 
        if (player3.activeSelf)
        {
            isJoinPlayer3 = false;
            Debug.Log("Player3参加");
        }
        if (player4.activeSelf)
        {
            isJoinPlayer4 = false;
            Debug.Log("Player4参加");
        }
    }
}