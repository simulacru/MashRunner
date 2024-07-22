using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : MonoBehaviour
{
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = this.GetComponent<Camera>();
        SetUp();
    }

    void SetUp()
    {
        if (PlayerCameraScript.joinPlayerCount < 2) //1人プレイ時
            cam.rect = new Rect(0.0f, 0.0f, 1.0f, 0.1f);
        else //プレイヤーが2人以上の時
            cam.rect = new Rect(0.0f, 0.45f, 1.0f, 0.1f);
    }
}
