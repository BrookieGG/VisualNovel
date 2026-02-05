using TMPro;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    public Button startButton;
    [SerializeField]
    public TextAsset inkJSON;
    [SerializeField]
    private InkManager inkManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startButton.onClick.AddListener(Story);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Story()
    {
        startButton.gameObject.SetActive(false);
        inkManager.StartStory(inkJSON);
    }
}
