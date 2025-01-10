using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1.0f;
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        EventHandler.OnBattleWithEnermy += SwitchToBattleScene;
        EventHandler.OnEnermyDead += SwitchToMainScene;
    }
    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        EventHandler.OnBattleWithEnermy -= SwitchToBattleScene;
        EventHandler.OnEnermyDead -= SwitchToMainScene;
    }

    private void SwitchToMainScene()
    {
        Debug.Log("SwitchToMainScene");
        StartCoroutine(FadeAndSwitchScene("MainScene"));
    }

    private void SwitchToBattleScene()
    {
        EventHandler.CallOnCloseMapCanvas();
        StartCoroutine(FadeAndSwitchScene("NewBattleScene"));
    }
    public void SwitchScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    private IEnumerator FadeAndSwitchScene(string sceneName)
    {
        yield return StartCoroutine(Fade(1f)); // 先淡出黑色
        SceneManager.LoadScene(sceneName); // 开始切换场景
        yield return null; // 等待一帧
        yield return StartCoroutine(Fade(0f)); // 再淡入
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeImage.color.a;
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, timeElapsed / fadeDuration);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // 确保最后一帧是目标透明度
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, targetAlpha);
    }

    public void SwitchToMainSceneButton()
    {
        EventHandler.CallOnCloseMapCanvas();
        StartCoroutine(FadeAndSwitchScene("Qiongdao"));
    }
}
