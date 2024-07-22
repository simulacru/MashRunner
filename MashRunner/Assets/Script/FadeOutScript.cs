using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeOutScript : MonoBehaviour
{
    public Image fadeImage;
    public float fadeSpeed = 1.5f;

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
