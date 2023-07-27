using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    [SerializeField] private GameObject menu;
    public static event Action onStartGame;
    [SerializeField] private CanvasGroup lose;
    [SerializeField] private CanvasGroup win;
    [SerializeField] private GameObject newGameBtn;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            DontDestroyOnLoad(this);
        }
    }
    public void WinState()
    {
        StartCoroutine(Fade(win, 1f, 0.2f));
    }
    public void LoseState()
    {
        StartCoroutine(Fade(lose, 1f, 0.2f));
    }

    public void StartGameBtn()
    {
        menu.gameObject.SetActive(false);
        newGameBtn.gameObject.SetActive(false);
        win.alpha = 0;
        lose.alpha = 0;
        onStartGame?.Invoke();
    }
    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay)
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;
        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = to;
        newGameBtn.gameObject.SetActive(true);
    }
}
