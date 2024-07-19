using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCameraScript : MonoBehaviour
{
    //�����J����
    Camera cam;
    float rectPosX = 0.5015f;
    float rectPosY = 0.555f;
    float rectW = 0.4985f;
    float rectH = 0.445f;

    bool[] playerArray = new bool[4] {false, false, false, false};
    public static int joinPlayerCount = 0;

    //Y���Œ�
    Vector3 initialLocalPos;
    Vector3 parentPos;

    // Start is called before the first frame update
    void Start()
    {
        cam = this.GetComponent<Camera>();
        PlayerAssign();

        //���[�J�����W�擾
        initialLocalPos = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //�e�I�u�W�F�N�g�̍��W - ���[�J�����W
        parentPos.x = this.transform.parent.position.x;
        this.transform.position = parentPos + initialLocalPos;

        CameraRect();
    }

    //�J�����̈ʒu���v���C���[�̐l���ɂ���ĕύX
    void CameraRect()
    {
        if (SceneManager.GetActiveScene().name == "PlayerJoinScene")
        {
            if(joinPlayerCount < 2) //1�l�v���C��
            {
                cam.rect = new Rect(0.0f, 0.105f, 1.0f, rectH * 2);
            }
            else if (2 <= joinPlayerCount && joinPlayerCount < 3) //2�l�v���C��
            {
                if (playerArray[1])
                    cam.rect = new Rect(0.0f, 0.0f, 1.0f, rectH);
                else if (playerArray[0])
                    cam.rect = new Rect(0.0f, rectPosY, 1.0f, rectH);
            }
            else //3, 4�l�v���C��
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

    //�v���C���[�l���擾
    void PlayerAssign()
    {
        //�v���C���[���蓖��
        if (!GetPlayer.isJoinPlayer4)
        {
            playerArray[3] = true;
            joinPlayerCount++;
            cam.LayerCullingToggle("Background4"); //�����郌�C���[ON
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