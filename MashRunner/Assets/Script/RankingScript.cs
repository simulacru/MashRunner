using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingScript : MonoBehaviour
{
    int resultRank = GoalScript.playerCount;
    public GameObject[] rankArray = new GameObject[4];

    //�X���C�h�ړ�
    public float speed;
    public Vector3[] startPos = new Vector3[4];
    Vector3[] rankPos = new Vector3[4];

    int slideNumber;
    bool[] slideBool = new bool[4];

    //SE
    AudioSource audioSource;
    public AudioClip firstRankSE;
    public AudioClip secondRankSE;
    bool[] hasPlayedRankSE = new bool[4] {false, false, false, false};


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        slideNumber = resultRank;

        slideBool[0] = false;
        slideBool[1] = false;
        slideBool[2] = false;
        slideBool[3] = false;

        //�ŏI�ʒu�idefault�j
        rankPos[0] = rankArray[0].transform.localPosition;
        rankPos[1] = rankArray[1].transform.localPosition;
        rankPos[2] = rankArray[2].transform.localPosition;
        rankPos[3] = rankArray[3].transform.localPosition;

        //�ŏ��̈ʒu
        rankArray[0].transform.localPosition = startPos[0];
        rankArray[1].transform.localPosition = startPos[1];
        rankArray[2].transform.localPosition = startPos[2];
        rankArray[3].transform.localPosition = startPos[3];

        //�v���C�l���̕����ʂ�\������
        rankArray[0].SetActive(1 <= resultRank);
        rankArray[1].SetActive(2 <= resultRank);
        rankArray[2].SetActive(3 <= resultRank);
        rankArray[3].SetActive(4 <= resultRank);

        //rankSlide��1.0�b��ɌĂяo���A�ȍ~��0.375�b���Ɏ��s
        InvokeRepeating("rankSlide", 1.0f, 0.375f); 
    }

    // Update is called once per frame
    void Update()
    {
        //���ʂ��珇�ɃX���C�h�ړ�������
        for (int i = 0; i < resultRank; i++)
        {
            if (slideBool[i])
            {
                rankArray[i].transform.localPosition = Vector3.MoveTowards(rankArray[i].transform.localPosition, rankPos[i], speed * Time.deltaTime);
            }
        }

        //���ʂɂ���Ė炷SE��ς���
        if(slideBool[0] && firstRankSE != null && !hasPlayedRankSE[0]) //1��
        {
            audioSource.PlayOneShot(firstRankSE);
            hasPlayedRankSE[0] = true;
        }
        else if (slideBool[1] && secondRankSE != null && !hasPlayedRankSE[1]) //2��
        {
            audioSource.PlayOneShot(secondRankSE);
            hasPlayedRankSE[1] = true;
        }
        else if (slideBool[2] && secondRankSE != null && !hasPlayedRankSE[2]) //3��
        {
            audioSource.PlayOneShot(secondRankSE);
            hasPlayedRankSE[2] = true;
        } 
        else if (slideBool[3] && secondRankSE != null && !hasPlayedRankSE[3]) //4��
        {
            audioSource.PlayOneShot(secondRankSE);
            hasPlayedRankSE[3] = true;
        }
            
    }

    void rankSlide()
    {
        slideNumber--;
        slideBool[slideNumber] = true;
        if (slideNumber <= 0)
        {
            CancelInvoke();
        }
    }
}
