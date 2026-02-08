using Ink.Parsed;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;

public class CharacterManager : MonoBehaviour
{
    //in Ink do
    //EXTERNAL place_characters(left_character_name, right_character_name)
    //EXTERNAL change_emotion(emotion, ID)
    //{place_characters("Charcter", "Character 1")}
    //{change_emotion("Angry", 0)} changes the character on the left to be angry
 
    //left = 0, right = 1


    public GameObject[] characters;
    private List<GameObject> charactersList = new List<GameObject>();
    private List<Character> activeCharacters = new List<Character>();

    [SerializeField]
    Vector3 leftPosition;
    [SerializeField]
    Vector3 rightPosition;
    [SerializeField]
    private Transform characterRoot;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (GameObject prefab in characters)
        {
            GameObject newCharacter = Instantiate(prefab, characterRoot);

            newCharacter.SetActive(false);

            //remove clones
            newCharacter.name = prefab.name;

            charactersList.Add(newCharacter);
        }
    }
    public void PlaceCharacters(string leftCharacterName, string rightCharacterName)
    {
        foreach (GameObject character in charactersList)
        {
            character.SetActive(false);
        }

        activeCharacters.Clear();

        //spawn left character
        if(!string.IsNullOrEmpty(leftCharacterName))
        SpawnCharacter(leftCharacterName, 0, leftPosition);
        //spawn right character
        if (!string.IsNullOrEmpty(rightCharacterName))
            SpawnCharacter(rightCharacterName, 1, rightPosition);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void SpawnCharacter(string name, int id, Vector3 position)
    {

        if (string.IsNullOrEmpty(name)) return;

        GameObject characterObject = charactersList.Find(c => c.name == name);
        characterObject.transform.localPosition = new Vector3(position.x, position.y, 0f);

        if (characterObject == null)
        {
            Debug.LogError("Character not found: " + name);
            return;
        }
        characterObject.SetActive(true);
        characterObject.transform.localPosition = position;

        Character character = characterObject.GetComponent<Character>();
        character.ID = id;

        activeCharacters.Add(character);
    }

    public void ChangeCharacterEmotion(string emotion, int ID)
    {
        foreach (Character character in activeCharacters)
        {
            
            if (character.ID == ID)
            {
                character.ChangeEmotion(emotion);
            }
            
        }
    }
    public void RemoveCharacter(int ID)
    {
        foreach (Character character in activeCharacters)
        {
            if (character.ID == ID && character.gameObject.activeInHierarchy)
            {
                character.gameObject.SetActive(false);
            }
        }
    }
}
