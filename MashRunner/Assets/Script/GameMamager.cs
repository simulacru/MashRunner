using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMamager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        EndGame();
    }

    private void EndGame()
    {
        if (Input.GetKey(KeyCode.Escape))
        {

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            //ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
        }

    }
}
