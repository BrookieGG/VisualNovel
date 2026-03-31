using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoEnd : MonoBehaviour
{

    public VideoPlayer videoPlayer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (videoPlayer == null)
        {
            Debug.Log("assign in inspector");
        }
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnVideoEnd(VideoPlayer videoPlayer)
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
    }
}
