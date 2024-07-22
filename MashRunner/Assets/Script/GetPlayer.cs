using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GetPlayer : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;

    //�e�v���C���[���A�N�e�B�u�ł��邩
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

        // �v���C���[�̓A�N�e�B�u���ǂ����`�F�b�N
        if (!playerInput.user.valid)
        {
            Debug.Log("�A�N�e�B�u�ȃv���C���[�ł͂���܂���");
            return;
        }

        // �v���C���[�ԍ������O�o��
        Debug.Log($"===== �v���C���[#{playerInput.user.index} =====");
    }

    void Userindex()
    {
        //�^�O���擾
        int playerCount = playerInput.user.index;

        //�q�I�u�W�F�N�g���擾
        GameObject player1 = transform.GetChild(0).gameObject;
        GameObject player2 = transform.GetChild(1).gameObject;
        GameObject player3 = transform.GetChild(2).gameObject;
        GameObject player4 = transform.GetChild(3).gameObject;

        //�ebool��true�̏ꍇ�A�e�v���C���[���A�N�e�B�u�ɐݒ�
        if (isJoinPlayer4)
            player4.SetActive(2 < playerCount && playerCount <= 3);
        if (isJoinPlayer3)
            player3.SetActive(1 < playerCount && playerCount <= 2);
        if (isJoinPlayer2)
            player2.SetActive(0 < playerCount && playerCount <= 1);
        if (isJoinPlayer1)
            player1.SetActive(-1 < playerCount && playerCount <= 0);

        //�e�v���C���[�������݂��邩���`�F�b�N���āA���݂���ꍇ��isJoinplayer��false�ɐݒ�
        if (player1.activeSelf)
        {
            isJoinPlayer1 = false;
            Debug.Log("Player1�Q��");
        }
        if (player2.activeSelf)
        {
            isJoinPlayer2 = false;
            Debug.Log("Player2�Q��");
        } 
        if (player3.activeSelf)
        {
            isJoinPlayer3 = false;
            Debug.Log("Player3�Q��");
        }
        if (player4.activeSelf)
        {
            isJoinPlayer4 = false;
            Debug.Log("Player4�Q��");
        }
    }
}