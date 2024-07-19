using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundScript : MonoBehaviour
{
    //�X���C�h���~�܂� or �n�܂���W
    [SerializeField] public float[] startPos = new float[3];
    [SerializeField] public float[] moveSpeed = new float[3];
    float posZ = 0.0f;

    //���W�擾�p
    Vector3 defaultLocalPos; //�X�^�[�g���̔w�i�̃��[�J�����W
    Vector3 startMove; //�������p

    SpriteRenderer mainSpriteRenderer;
    public Sprite[] backgroundSprite = new Sprite[4];

    bool isStage1;
    bool isStage2;

    // Start is called before the first frame update
    void Start()
    {
        mainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        //���[�J�����W�擾
        defaultLocalPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Background();
    }

    //�w�i���W
    void Background()
    {
        BackgroundChange();

        startMove.y = (defaultLocalPos.y - this.transform.parent.position.y) * 2;

        if (PlayerCameraScript.joinPlayerCount > 2) //3,4�l
        {
            //�v���C���[�̍��W / �������X�s�[�h + �X�^�[�g���̍��W����
            startMove.x = -(this.transform.parent.position.x - defaultLocalPos.x) / moveSpeed[2] + startPos[2];
            startMove.z = defaultLocalPos.z;
        }
        else if (PlayerCameraScript.joinPlayerCount > 1) //2�l
        {
            startMove.x = -(this.transform.parent.position.x - defaultLocalPos.x) / moveSpeed[1] + startPos[1];
            startMove.z = posZ;
        }
        else if (PlayerCameraScript.joinPlayerCount > 0) //1�l
        {
            startMove.x = -(this.transform.parent.position.x - defaultLocalPos.x) / moveSpeed[2] + startPos[2];
            startMove.z = defaultLocalPos.z;
        }

        //�e�I�u�W�F�N�g�̍��W - ���[�J�����W
        this.transform.localPosition = startMove;
    }

    //�l���A�X�e�[�W�ɂ���ĕς���
    void BackgroundChange()
    {
        isStage1 = StageSlectControll.isStage1;
        isStage2 = StageSlectControll.isStage2;

        if (PlayerCameraScript.joinPlayerCount > 2 && isStage1) //4�l�A�X�e�[�W1
            mainSpriteRenderer.sprite = backgroundSprite[0];
        else if(PlayerCameraScript.joinPlayerCount > 2 && isStage2) //4�l�A�X�e�[�W2
            mainSpriteRenderer.sprite = backgroundSprite[2];
        else if(PlayerCameraScript.joinPlayerCount > 1 && isStage1) //2�l�A�X�e�[�W1
            mainSpriteRenderer.sprite = backgroundSprite[1];
        else if(PlayerCameraScript.joinPlayerCount > 1 && isStage2) //2�l�A�X�e�[�W2
            mainSpriteRenderer.sprite = backgroundSprite[3];
        else if (PlayerCameraScript.joinPlayerCount > 0 && isStage1) //1�l�A�X�e�[�W1
            mainSpriteRenderer.sprite = backgroundSprite[0];
        else if (PlayerCameraScript.joinPlayerCount > 0 && isStage2) //1�l�A�X�e�[�W2
            mainSpriteRenderer.sprite = backgroundSprite[2];
    }
}
