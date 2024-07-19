using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class OverviewChange : MonoBehaviour
{
    //�X���C�_�[�֌W
    public Slider[] selectSlider;
    public static Slider findObj;
    public EventSystem eventSystem;

    //�e�L�X�g�֌W
    [SerializeField] TextMeshProUGUI BGMText;
    [SerializeField] TextMeshProUGUI SEText;

    //�V�[���֌W
    [SerializeField] private string prevScene = null;

    //SE
    public MenuSEScript menuSEScript;

    // Start is called before the first frame update
    void Start()
    {
        //���j���[�{�^���I����Ԏ擾
        MenuControll.isSound = true;
    }

    // Update is called once per frame
    void Update()
    {
        findObj = eventSystem.currentSelectedGameObject.gameObject.GetComponentInParent<Slider>();

        //�I�𒆂̃X���C�_�[�̕����̐F��ς���
        for (int i = 0; i < selectSlider.Length; i++)
        {
            if (findObj == selectSlider[0])
            {
                BGMText.color = new Color(1.0f, 0.0f, 0.0f, 1.0f); //��
                SEText.color = new Color(1.0f, 1.0f, 1.0f, 1.0f); //��
            }
            else if(findObj == selectSlider[1])
            {
                BGMText.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                SEText.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            }
            else
            {
                BGMText.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                SEText.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
        }
    }

    //���j���[��
    public void BackScene()
    {
        menuSEScript.BackSceneSE();
        SceneManager.LoadScene(prevScene);
    }
}
