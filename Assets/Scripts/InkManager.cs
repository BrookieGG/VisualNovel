using System;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;
using TMPro.EditorUtilities;
using System.Collections;
using System.Collections.Generic;


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
    CharacterManager cm;
    GameManager gm;
    private bool dialogueIsPlaying = false;

    private static InkManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            Debug.Log("Found more than one manager");
        }
        instance = this;
    }
    void Start()
    {
        // Remove the default message
        cm = GetComponent<CharacterManager>();
        gm = GetComponent<GameManager>();
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        RemoveChildren();
        //StartStory();
    }

    public static InkManager GetInstance()
    {
        return instance;
    }
    // Creates a new Story object with the compiled story which we can then play!
    public void StartStory(TextAsset inkJSON)
    {
        story = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        while (story.canContinue)
        {
            // Continue gets the next line of the story
            string text = story.Continue();
            // This removes any white space from the text.
            text = text.Trim();
            // Display the text on screen!
            CreateContentView(text);
        }

        if (story.currentChoices.Count > 0)
        {
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                Choice choice = story.currentChoices[i];
                Button button = CreateChoiceView(choice.text.Trim());
                // Tell the button what to do when we press it
                button.onClick.AddListener(delegate {
                    OnClickChoiceButton(choice);
                });
            }
        }
        else
        {
            Button choice = CreateChoiceView("End of story.\nRestart?");
            choice.onClick.AddListener(delegate {
                StartStory();
            });
        }
        story.BindExternalFunction("place_characters", (string leftName, string rightName) =>
        {
            cm.PlaceCharacters(leftName, rightName);
        });
        story.BindExternalFunction("change_emotion", (string emotion, int ID) =>
        {
            cm.ChangeCharacterEmotion(emotion, ID);
        });
        //RefreshView();
    }

    // This is the main function called every time the story changes. It does a few things:
    // Destroys all the old content and choices.
    // Continues over all the lines of text, then displays all the choices. If there are no choices, the story is finished!
    void RefreshView()
    {
        // Remove all the UI on screen
        RemoveChildren();

        // Read all the content until we can't continue any more
        

        // Display all the choices, if there are any!
      
        // If we've read all the content and there's no choices, the story is finished!
       
    }

    // When we click the choice button, tell the story to choose that choice!
    void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        RefreshView();
    }

    // Creates a textbox showing the the line of text
    void CreateContentView(string text)
    {
        TMP_Text storyText = Instantiate(dialogueText) as TMP_Text;
        storyText.text = text;
        storyText.transform.SetParent(canvas.transform, false);
    }

    // Creates a button showing the choice text
    Button CreateChoiceView(string text)
    {
        // Creates the button from a prefab
        Button choice = Instantiate(buttonPrefab) as Button;
        choice.transform.SetParent(canvas.transform, false);

        // Gets the text from the button prefab
        Text choiceText = choice.GetComponentInChildren<Text>();
        choiceText.text = text;

        // Make the button expand to fit the text
        HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
        layoutGroup.childForceExpandHeight = false;

        return choice;
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
}
