using System;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;
using TMPro.EditorUtilities;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.EventSystems;
using Unity.VisualScripting;


public class InkManager : MonoBehaviour { 

    [SerializeField]
    //private TextAsset inkJSONAsset = null;
    public Story story;

    [SerializeField]
    private Canvas canvas = null;

    // UI Prefabs
    [SerializeField]
    private TMP_Text dialogueText = null;
    [SerializeField]
    private Button buttonPrefab = null;
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


    [SerializeField]
    private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;
    //private bool dialogueIsPlaying = false;

    private const string SPEAKER_TAG = "speaker";
    //private const string PORTRAIT_TAG = "portrait";
    //private const string LAYOUT_TAG = "layout";


    void Start()
    {
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
    }

    // Creates a new Story object with the compiled story which we can then play!
    public void StartStory(TextAsset inkJSON)
    {
        story = new Story(inkJSON.text);

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

        dialoguePanel.SetActive(true);

        ContinueStory();
    }
    public void NextLine()
    {
        ContinueStory();
    }

    private void ContinueStory()
    {
        HideChoices();

        // Clear previous dialogue
        dialogueText.text = "";

        bool textDisplayed = false;

        // Keep continuing until we get a line with actual text OR reach choices
        while (story.canContinue)
        {
            string text = story.Continue().Trim();

            // Handle any tags attached to this line
            HandleTags(story.currentTags);

            if (!string.IsNullOrEmpty(text))
            {
                // Display this line
                CreateContentView(text);
                nextButton.gameObject.SetActive(true);
                textDisplayed = true;
                break;
            }
        }

        // If we can't continue but there are choices, show them
        if (!textDisplayed && story.currentChoices.Count > 0)
        {
            DisplayChoices();
            nextButton.gameObject.SetActive(false);
        }

        // If story is fully done
        if (!textDisplayed && story.currentChoices.Count == 0)
        {
            ExitDialogue();
        }
    }

    private void HandleTags(List<string> currentTags)
    {

        displayNameText.text = "";

        bool speakerSetThisLine = false;

        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2) continue;
           
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            if (tag.StartsWith("speaker:"))
            {
                displayNameText.text = tag.Substring(2);
            }

            //handle the tag

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    speakerSetThisLine = true;

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
        if (!speakerSetThisLine)
        {
            speakerNamePanel.SetActive(false);
        }
    }
    public void ExitDialogue()
    {
        dialoguePanel.SetActive(false);
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
            Debug.Log("more choices were given than UI can show");
        }

        int index = 0;
        //enable and initialize the choices up to the amount of choices in ink
        foreach(Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        // go through the remaining choices
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
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
        
}
