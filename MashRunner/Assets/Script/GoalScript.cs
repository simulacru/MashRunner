using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalScript : MonoBehaviour
{
    //�V�[���ڍs�֌W
    [SerializeField] private string resultScene;

    //���U���g, �S�[���֌W
    int maxPlayerCount;
    public static int playerCount;
    public static int goalPlayerCount = 0;
    public static int[] rankArray = new int[4];
    bool isGoalMax = false;

    public static bool canGool = true;

    //�t�F�[�h�A�E�g
    public FadeOutScript fadeOutScript;

    // Start is called before the first frame update
    void Start()
    {
        //�v���C���[�Q���l���擾
        maxPlayerCount = GameObject.FindGameObjectsWithTag("Player").Length;
        Debug.Log($"maxPlaterCount: {maxPlayerCount}");

        RankReset();
    }

    // Update is called once per frame
    void Update()
    {
        //�S�����S�[��
        if (maxPlayerCount <= goalPlayerCount)
        {
            canGool = false;
            Debug.Log("�Q�[���I��");

            playerCount = maxPlayerCount;
            isGoalMax = true;

            //�t�F�[�h�A�E�g
            StartCoroutine(fadeOutScript.FadeOut());
            SceneManager.LoadScene(resultScene);
        }

        Result();
    }

    //�����N���Z�b�g
    void RankReset()
    {
        if (ResultMenuScript.canGoal)
        {
            for (int i = 0; i < maxPlayerCount; i++)
            {
                rankArray[i] = 0; // �v���C���[�̏��ʂ�������
            }
            goalPlayerCount = 0;
            ResultMenuScript.canGoal = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && maxPlayerCount > goalPlayerCount)
        {
            //�S�[���ɓ��B�����l�����J�E���g
            goalPlayerCount++;
            Debug.Log($"playerCount: {goalPlayerCount}");
        }
    }

    //���U���g�p
    void Result()
    {
        if (isGoalMax)
        {
            //�e���ʂ�z��ɕۑ�
            for (int i = 0; i < rankArray.Length; i++)
            {
                rankArray[i] = PlayerOtherController.rankArray[i];
            }
        }
    }
}
