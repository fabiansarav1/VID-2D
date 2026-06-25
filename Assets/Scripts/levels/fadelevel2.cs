using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInLevel2 : MonoBehaviour
{
    public Image fadePanel;
    public float fadeDuration = 1f;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float timer = 0f;

        Color color = fadePanel.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            color.a = Mathf.Lerp(1f, 0f, timer / fadeDuration);

            fadePanel.color = color;

            yield return null;
        }
    }
}