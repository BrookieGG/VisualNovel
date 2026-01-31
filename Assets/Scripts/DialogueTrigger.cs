using UnityEngine;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject startButton;
    [SerializeField]
    private TextAsset inkJSON; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startButton.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Story()
    {
        startButton.SetActive(false);
        InkManager.GetInstance().StartStory(inkJSON);
    }
}
