using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeOutScript : MonoBehaviour
{
    public Image fadeImage;
    public float fadeSpeed = 1.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator FadeOut()
    {
        Color color = fadeImage.color;
        while (fadeImage.color.a < 1)
        {
            color.a += fadeSpeed * Time.deltaTime;
            fadeImage.color = color;
            yield return null;
        }
    }
}
