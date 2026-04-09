using UnityEngine;
using System.Collections.Generic;
using JetBrains.Annotations;

public class ClueManager : MonoBehaviour
{
    public static ClueManager Instance;

    public InkManager inkManager;

    private HashSet<string> collectedClues = new HashSet<string>();

    public GameObject notebookPanel;
    [SerializeField] private GameObject[] allClues;
    [SerializeField] private GameObject continueButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
        notebookPanel.SetActive(false);
    }
    public void AddClue(string clueID)
    {
        collectedClues.Add(clueID);

        if (!collectedClues.Contains(clueID))
        {
            collectedClues.Add(clueID);

            // Update Ink variable immediately
            if (inkManager != null && inkManager.story != null)
            {
                switch (clueID)
                {
                    case "ash_found":
                        inkManager.story.variablesState["ash_collected"] = true;
                        break;
                    case "orangehair_found":
                        inkManager.story.variablesState["hair_collected"] = true;
                        break;
                }
            }
        }

        if (collectedClues.Count >= 2)
        {
            continueButton.SetActive(true);
        }
        if (collectedClues.Count < 2)
        {
            continueButton.SetActive(false);
        }

    }
    
    public bool HasClue(string clueID)
    {
        return collectedClues.Contains(clueID);
    }
    public void OnAddClueButton(string clueID)
    {
        ClueManager.Instance.AddClue(clueID);
    }
    public void OpenNotebook()
    {
        notebookPanel.SetActive(true);

        foreach (GameObject clue in allClues)
        {
            clue.SetActive(false);
        }

        // Then, enable only collected clues
        foreach (GameObject clue in allClues)
        {
            if (collectedClues.Contains(clue.name))
            {
                clue.SetActive(true);
            }
        }
    }
          public void CloseNotebook()
    {
        notebookPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
