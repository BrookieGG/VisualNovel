using TMPro;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;
using System.Collections;

public class DialogueTrigger : MonoBehaviour
{
    //[SerializeField]
    //public Button startButton;
    [SerializeField]
    public TextAsset inkJSON;
    [SerializeField]
    private InkManager inkManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (inkManager != null || inkJSON != null)
        {
            StartCoroutine(Story());
        }
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Story()
    {
        yield return null;
        inkManager.StartStory(inkJSON);
    }
}
