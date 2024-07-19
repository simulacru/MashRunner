using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalScript : MonoBehaviour
{
    //シーン移行関係
    [SerializeField] private string resultScene;

    //リザルト, ゴール関係
    int maxPlayerCount;
    public static int playerCount;
    public static int goalPlayerCount = 0;
    public static int[] rankArray = new int[4];
    bool isGoalMax = false;

    public static bool canGool = true;

    //フェードアウト
    public FadeOutScript fadeOutScript;

    // Start is called before the first frame update
    void Start()
    {
        //プレイヤー参加人数取得
        maxPlayerCount = GameObject.FindGameObjectsWithTag("Player").Length;
        Debug.Log($"maxPlaterCount: {maxPlayerCount}");

        RankReset();
    }

    // Update is called once per frame
    void Update()
    {
        //全員がゴール
        if (maxPlayerCount <= goalPlayerCount)
        {
            canGool = false;
            Debug.Log("ゲーム終了");

            playerCount = maxPlayerCount;
            isGoalMax = true;

            //フェードアウト
            StartCoroutine(fadeOutScript.FadeOut());
            SceneManager.LoadScene(resultScene);
        }

        Result();
    }

    //ランクリセット
    void RankReset()
    {
        if (ResultMenuScript.canGoal)
        {
            for (int i = 0; i < maxPlayerCount; i++)
            {
                rankArray[i] = 0; // プレイヤーの順位を初期化
            }
            goalPlayerCount = 0;
            ResultMenuScript.canGoal = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && maxPlayerCount > goalPlayerCount)
        {
            //ゴールに到達した人数をカウント
            goalPlayerCount++;
            Debug.Log($"playerCount: {goalPlayerCount}");
        }
    }

    //リザルト用
    void Result()
    {
        if (isGoalMax)
        {
            //各順位を配列に保存
            for (int i = 0; i < rankArray.Length; i++)
            {
                rankArray[i] = PlayerOtherController.rankArray[i];
            }
        }
    }
}
