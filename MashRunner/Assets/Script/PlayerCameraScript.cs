using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCameraScript : MonoBehaviour
{
    //分割カメラ
    Camera cam;
    float rectPosX = 0.5015f;
    float rectPosY = 0.555f;
    float rectW = 0.4985f;
    float rectH = 0.445f;

    bool[] playerArray = new bool[4] {false, false, false, false};
    public static int joinPlayerCount = 0;

    //Y軸固定
    Vector3 initialLocalPos;
    Vector3 parentPos;

    // Start is called before the first frame update
    void Start()
    {
        cam = this.GetComponent<Camera>();
        PlayerAssign();

        //ローカル座標取得
        initialLocalPos = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //親オブジェクトの座標 - ローカル座標
        parentPos.x = this.transform.parent.position.x;
        this.transform.position = parentPos + initialLocalPos;

        CameraRect();
    }

    //カメラの位置をプレイヤーの人数によって変更
    void CameraRect()
    {
        if (SceneManager.GetActiveScene().name == "PlayerJoinScene")
        {
            if(joinPlayerCount < 2) //1人プレイ時
            {
                cam.rect = new Rect(0.0f, 0.105f, 1.0f, rectH * 2);
            }
            else if (2 <= joinPlayerCount && joinPlayerCount < 3) //2人プレイ時
            {
                if (playerArray[1])
                    cam.rect = new Rect(0.0f, 0.0f, 1.0f, rectH);
                else if (playerArray[0])
                    cam.rect = new Rect(0.0f, rectPosY, 1.0f, rectH);
            }
            else //3, 4人プレイ時
            {
                if (playerArray[3])
                    cam.rect = new Rect(rectPosX, 0.0f, rectW, rectH);
                else if (playerArray[2])
                    cam.rect = new Rect(0.0f, 0.0f, rectW, rectH);
                else if (playerArray[1])
                    cam.rect = new Rect(rectPosX, rectPosY, rectW, rectH);
                else if (playerArray[0])
                    cam.rect = new Rect(0.0f, rectPosY, rectW, rectH);
            }
        }
    }

    //プレイヤー人数取得
    void PlayerAssign()
    {
        //プレイヤー割り当て
        if (!GetPlayer.isJoinPlayer4)
        {
            playerArray[3] = true;
            joinPlayerCount++;
            cam.LayerCullingToggle("Background4"); //見えるレイヤーON
        }
        else if (!GetPlayer.isJoinPlayer3)
        {
            playerArray[2] = true;
            joinPlayerCount++;
            cam.LayerCullingToggle("Background3");
        }
        else if (!GetPlayer.isJoinPlayer2)
        {
            playerArray[1] = true;
            joinPlayerCount++;
            cam.LayerCullingToggle("Background2");
        }
        else if (!GetPlayer.isJoinPlayer1)
        {
            playerArray[0] = true;
            joinPlayerCount++;
            cam.LayerCullingToggle("Background1");
        }
    }
}