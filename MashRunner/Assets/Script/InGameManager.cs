using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour
{
    [SerializeField] private string menuScene = null;

    //�t�F�[�h�A�E�g
    public FadeOutScript fadeOutScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            //�t�F�[�h�A�E�g
            StartCoroutine(fadeOutScript.FadeOut());

            SceneManager.LoadScene(menuScene);
        }
    }
}
