using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChange : MonoBehaviour
{
    Fade fade;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fade = FindAnyObjectByType<Fade>();
    }

    public void StartGame()
    {
        StartCoroutine(LoadScene());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator LoadScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            fade.FadeIn();
            yield return new WaitForSeconds(fade.fadeTime);
            SceneManager.LoadScene(nextIndex);
        }
    }

}
