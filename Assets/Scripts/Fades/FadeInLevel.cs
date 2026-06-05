using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInLevel : MonoBehaviour
{
    public Image fadePanel;
    public float fadeDuration = 1.5f;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float time = 0f;

        Color color = fadePanel.color;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            color.a = Mathf.Lerp(1f, 0f, time / fadeDuration);
            fadePanel.color = color;

            yield return null;
        }

        color.a = 0f;
        fadePanel.color = color;
    }
}