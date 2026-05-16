using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public float speed = 20f;

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        rectTransform.anchoredPosition += Vector2.right * speed * Time.deltaTime;

        // Reinicio simple
        if (rectTransform.anchoredPosition.x > 100f)
        {
            rectTransform.anchoredPosition = new Vector2(0, rectTransform.anchoredPosition.y);
        }
    }
}