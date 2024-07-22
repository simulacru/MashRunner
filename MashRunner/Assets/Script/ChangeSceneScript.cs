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
