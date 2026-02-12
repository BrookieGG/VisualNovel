using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    public CanvasGroup canvasgroup;
    public bool fadeIn = false;
    public bool fadeOut = false;

    public float fadeTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        fadeIn = false;
        fadeOut = false;
        FadeOut();
    }
    // Update is called once per frame
    void Update()
    {
        if(fadeIn == true)
        {
            if(canvasgroup.alpha < 1)
            {
                canvasgroup.alpha += fadeTime * Time.deltaTime;
                if(canvasgroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }

        if (fadeOut == true)
        {
            canvasgroup.alpha -= fadeTime * Time.deltaTime;

            if (canvasgroup.alpha<= 0f)
            {
                canvasgroup.alpha = 0f;
                fadeOut = false;
                
            }
        }
    }

    public void FadeIn()
    {
        fadeIn = true;
    }

    public void FadeOut()
    {
        fadeOut = true;
    }
}
