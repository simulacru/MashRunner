using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ChangeSceneScript : MonoBehaviour 
{
    //タイトル
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

    //次のシーンへ
    public void OnGo(InputAction.CallbackContext context)
    {
        //SE
        menuSEScript.NextSceneSE();
        //フェードアウト
        StartCoroutine(fadeOutScript.FadeOut());

        SceneManager.LoadScene(nextScene);
    }
}
