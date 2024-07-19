using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundScript : MonoBehaviour
{
    //スライドが止まる or 始まる座標
    [SerializeField] public float[] startPos = new float[3];
    [SerializeField] public float[] moveSpeed = new float[3];
    float posZ = 0.0f;

    //座標取得用
    Vector3 defaultLocalPos; //スタート時の背景のローカル座標
    Vector3 startMove; //動かす用

    SpriteRenderer mainSpriteRenderer;
    public Sprite[] backgroundSprite = new Sprite[4];

    bool isStage1;
    bool isStage2;

    // Start is called before the first frame update
    void Start()
    {
        mainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        //ローカル座標取得
        defaultLocalPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Background();
    }

    //背景座標
    void Background()
    {
        BackgroundChange();

        startMove.y = (defaultLocalPos.y - this.transform.parent.position.y) * 2;

        if (PlayerCameraScript.joinPlayerCount > 2) //3,4人
        {
            //プレイヤーの座標 / 動かすスピード + スタート時の座標調整
            startMove.x = -(this.transform.parent.position.x - defaultLocalPos.x) / moveSpeed[2] + startPos[2];
            startMove.z = defaultLocalPos.z;
        }
        else if (PlayerCameraScript.joinPlayerCount > 1) //2人
        {
            startMove.x = -(this.transform.parent.position.x - defaultLocalPos.x) / moveSpeed[1] + startPos[1];
            startMove.z = posZ;
        }
        else if (PlayerCameraScript.joinPlayerCount > 0) //1人
        {
            startMove.x = -(this.transform.parent.position.x - defaultLocalPos.x) / moveSpeed[2] + startPos[2];
            startMove.z = defaultLocalPos.z;
        }

        //親オブジェクトの座標 - ローカル座標
        this.transform.localPosition = startMove;
    }

    //人数、ステージによって変える
    void BackgroundChange()
    {
        isStage1 = StageSlectControll.isStage1;
        isStage2 = StageSlectControll.isStage2;

        if (PlayerCameraScript.joinPlayerCount > 2 && isStage1) //4人、ステージ1
            mainSpriteRenderer.sprite = backgroundSprite[0];
        else if(PlayerCameraScript.joinPlayerCount > 2 && isStage2) //4人、ステージ2
            mainSpriteRenderer.sprite = backgroundSprite[2];
        else if(PlayerCameraScript.joinPlayerCount > 1 && isStage1) //2人、ステージ1
            mainSpriteRenderer.sprite = backgroundSprite[1];
        else if(PlayerCameraScript.joinPlayerCount > 1 && isStage2) //2人、ステージ2
            mainSpriteRenderer.sprite = backgroundSprite[3];
        else if (PlayerCameraScript.joinPlayerCount > 0 && isStage1) //1人、ステージ1
            mainSpriteRenderer.sprite = backgroundSprite[0];
        else if (PlayerCameraScript.joinPlayerCount > 0 && isStage2) //1人、ステージ2
            mainSpriteRenderer.sprite = backgroundSprite[2];
    }
}
