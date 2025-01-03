using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Image fadeImage;  // 黑色图像
    public float fadeDuration = 1f;  // 渐变持续时间
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Show());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Fade()
    {
        fadeImage.color = new Color(0, 0, 0, 0);
        fadeImage.gameObject.SetActive(true);

        float startAlpha = fadeImage.color.a;
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 1f, timeElapsed / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // 确保最终 alpha 值正确
        fadeImage.color = new Color(0, 0, 0, 1f);
    }

    private IEnumerator Show()
    {
        fadeImage.color = new Color(0, 0, 0, 1f);
        fadeImage.gameObject.SetActive(true);

        float startAlpha = fadeImage.color.a;
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0f, timeElapsed / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // 确保最终 alpha 值正确
        fadeImage.color = new Color(0, 0, 0, 0f);
        fadeImage.gameObject.SetActive(false);
    }
}
