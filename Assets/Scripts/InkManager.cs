
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class InkManager : MonoBehaviour { 

    [SerializeField]
    public Story story;
    public TextAsset inkJSON;

    [SerializeField]
    private Canvas canvas = null;

    // UI Prefabs
    [SerializeField]
    private TMP_Text dialogueText = null;
    [SerializeField]
    private GameObject dialoguePanel;
    private CharacterManager cm;
    private GameManager gm;
    [SerializeField]
    private Button nextButton;
    [SerializeField]
    private TextMeshProUGUI displayNameText;
    [SerializeField]
    private GameObject speakerNamePanel;
    Fade fade;
    [SerializeField]
    private Transform centerObjectPlaceholder;


    [SerializeField]
    private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;
    //private bool dialogueIsPlaying = false;

    [SerializeField]
    private float typingSpeed = 0.05f;

    private const string SPEAKER_TAG = "speaker";
    //private const string PORTRAIT_TAG = "portrait";
    //private const string LAYOUT_TAG = "layout";
    private string currentSpeaker = "";
    private bool isTyping = false;
    private bool isWaiting = false;



    void Start()
    {
        fade = FindAnyObjectByType<Fade>();
        cm = GetComponent<CharacterManager>();
        gm = GetComponent<GameManager>();

        dialoguePanel.SetActive(false);
        speakerNamePanel.SetActive(false);

        nextButton.onClick.AddListener(ContinueStory);

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;

        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }

        if (inkJSON == null)
        {
            Debug.LogError("No Ink JSON assigned in scene!");
            return;
        }

        StartStory(inkJSON);
    }

    // Creates a new Story object with the compiled story which we can then play!
    public void StartStory(TextAsset inkJSON)
    {
  
        if (fade != null)
        {
            fade.canvasgroup.alpha = 0f; // make text visible immediately
        }

        story = new Story(inkJSON.text);

        if (ClueManager.Instance != null)
        {
            if (ClueManager.Instance.HasClue("ash_found"))
                story.variablesState["ash_collected"] = true;

            if (ClueManager.Instance.HasClue("orangehair_found"))
                story.variablesState["hair_collected"] = true;
        }

        story.BindExternalFunction("place_characters", (string leftName, string rightName) =>
        {
            if(cm != null) cm.PlaceCharacters(leftName, rightName);
        });
        story.BindExternalFunction("change_emotion", (string emotion, int ID) =>
        {
            cm.ChangeCharacterEmotion(emotion, ID);
        });
        story.BindExternalFunction("remove_character", (int ID) =>
        {
            if (cm != null)
                cm.RemoveCharacter(ID);
        });
        story.BindExternalFunction("place_center_object", (string objectName) =>
        {
            GameObject prefab = Resources.Load<GameObject>($"CenterObjects/{objectName}");
            if (prefab != null)
                PlaceObject(prefab);
        });
        story.BindExternalFunction("wait", (float seconds) =>
        {
            isWaiting = true;
            StartCoroutine(WaitAndContinue(seconds));
        });
        story.BindExternalFunction("remove_center_object", () =>
        {
            RemoveCenterObject();
        });
        if (ClueManager.Instance != null)
        {
            story.variablesState["ash_collected"] = ClueManager.Instance.HasClue("ash_found");
            story.variablesState["hair_collected"] = ClueManager.Instance.HasClue("orangehair_found");
        }

        ContinueStory();
    }
    public void NextLine()
    {
        ContinueStory();
    }

    public void ContinueStory()
    {
        StartCoroutine(ContinueStoryCoroutine());
    }
    public IEnumerator ContinueStoryCoroutine()
    { 
        //Debug.Log("Entering ContinueStory");

        dialoguePanel.SetActive(true);
        canvas.gameObject.SetActive(true);

        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = story.currentText;
            isTyping = false;
            yield break;
        }

        HideChoices();

        // Clear previous dialogue
        dialogueText.text = "";

        bool textDisplayed = false;

        // Keep continuing until we get a line with actual text OR reach choices
        while (story.canContinue)
        {
            while (isWaiting)
                yield return null;

            string text = story.Continue().Trim();

            // Handle any tags attached to this line
            HandleTags(story.currentTags);

            if (!string.IsNullOrEmpty(text))
            {
                // Display this line
                yield return StartCoroutine(DisplayLine(text));
                //CreateContentView(text);
                nextButton.gameObject.SetActive(true);
                textDisplayed = true;
                break;
            }
        }

        // If we can't continue but there are choices, show them

        Debug.Log("Choices count: " + story.currentChoices.Count);
        if (!textDisplayed && story.currentChoices.Count > 0)
        {
            DisplayChoices();
            nextButton.gameObject.SetActive(false);
        }

        // If story is fully done
        if (!textDisplayed && story.currentChoices.Count == 0)
        {
            NextScene();
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        // empty the dialogue text
        isTyping = true;
        dialogueText.text = "";

        //display one letter at a time
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    private void HandleTags(List<string> currentTags)
    {

        displayNameText.text = "";
        displayNameText.text = "";
        speakerNamePanel.SetActive(false);

        //bool speakerSetThisLine = false;

        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2) continue;

            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();
            switch (tagKey)
            {
                case SPEAKER_TAG:

                    if (!string.IsNullOrEmpty(tagValue) && tagValue != "NONE")
                    {
                        currentSpeaker = tagValue;
                        displayNameText.text = currentSpeaker;
                        speakerNamePanel.SetActive(true);
                    }
                    break;

                case "sfx":
                    AudioManager.Instance.PlaySFX(tagValue);
                    break;

                default:
                    Debug.Log("tag came in but its not currently being handled: " + tag);
                    break;
            }
        
    

            //handle the tag

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    //speakerSetThisLine = true;

                    if (string.IsNullOrEmpty(tagValue) || tagValue == "NONE")
                    {
                        speakerNamePanel.SetActive(false);
                    }
                    else
                    {
                        speakerNamePanel.SetActive(true);
                        //Debug.Log("speaker=" + tagValue);
                        displayNameText.text = tagValue;
                    }
                    break;

                case "sfx":
                    AudioManager.Instance.PlaySFX(tagValue);
                    break;
                //case "place_center_object":
                    //GameObject prefab = Resources.Load<GameObject>($"CenterObjects/{tagValue}");
                    //break;
                //case LAYOUT_TAG:
                    //Debug.Log("layout=" + tagValue);
                    //break;
                //case PORTRAIT_TAG:
                    //Debug.Log("portrait=" + tagValue);
                    //break;
                default:
                    Debug.Log("tag came in but its not currently being handled: " + tag);
                    break;
            }
        }
       
    }

    private TMP_Text currentStoryText;
    void CreateContentView(string text)
    {

        //storyText.transform.SetParent(canvas.transform, false);
        if (currentStoryText != null)
        {
            Destroy(currentStoryText.gameObject);
        }

        currentStoryText = Instantiate(dialogueText, dialoguePanel.transform, false);
        currentStoryText.text = text;
    }

    // Destroys all the children of this gameobject (all the UI)
    void RemoveChildren()
    {
        int childCount = canvas.transform.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            Destroy(canvas.transform.GetChild(i).gameObject);
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = story.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogWarning("More choices than UI can show");
        }

        // disable all first
        for (int i = 0; i < choices.Length; i++)
        {
            choices[i].SetActive(false);
        }

        int index = 0;

        foreach (Choice choice in currentChoices)
        {
            if (index >= choices.Length) break;

            choices[index].SetActive(true);
            choicesText[index].text = choice.text;

            index++;
        }

    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();

        // extra frame prevents Unity "sticky selection"
        //yield return null;

        if (choices.Length > 0 && choices[0].activeInHierarchy)
        {
            EventSystem.current.SetSelectedGameObject(choices[0]);
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        story.ChooseChoiceIndex(choiceIndex);

        HideChoices();
        ContinueStory();
    }

    private void HideChoices()
    {
        foreach (GameObject choice in choices)
        {
            choice.SetActive(false);
        }
    }
    private void NextScene()
    {
       StartCoroutine(LoadNextScene());
    }
    private IEnumerator LoadNextScene()
    {
        dialoguePanel.SetActive(false);

        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            fade.FadeIn();
            yield return new WaitForSeconds(fade.fadeTime);
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            Debug.LogWarning("Next scene name not set on InkManager.");
        }
    }
    public void PlaceObject(GameObject objPrefab)
    {
        foreach (Transform child in centerObjectPlaceholder)
        {
            Destroy(child.gameObject);
        }
        GameObject obj = Instantiate(objPrefab, centerObjectPlaceholder);
        obj.transform.localPosition = Vector3.zero;
    }
    private IEnumerator WaitAndContinue(float seconds)
    {
        isWaiting = true;
        yield return new WaitForSeconds(seconds);
        isWaiting = false;
    }
    public void RemoveCenterObject()
    {
        foreach (Transform child in centerObjectPlaceholder)
        {
            Destroy(child.gameObject);
        }
    }
   
}
