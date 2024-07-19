using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ChangeSceneScript : MonoBehaviour 
{
    //�^�C�g��
    [SerializeField] private string nextScene = null;

    public FadeOutScript fadeOutScript;
    public MenuSEScript menuSEScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //���̃V�[����
    public void OnGo(InputAction.CallbackContext context)
    {
        //SE
        menuSEScript.NextSceneSE();
        //�t�F�[�h�A�E�g
        StartCoroutine(fadeOutScript.FadeOut());

        SceneManager.LoadScene(nextScene);
    }
}
