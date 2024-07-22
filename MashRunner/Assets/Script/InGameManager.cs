using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour
{
    [SerializeField] private string menuScene = null;

    //フェードアウト
    public FadeOutScript fadeOutScript;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            //フェードアウト
            StartCoroutine(fadeOutScript.FadeOut());

            SceneManager.LoadScene(menuScene);
        }
    }
}
