using Ink.Parsed;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour
{
    //in Ink do
    //EXTERNAL place_characters(left_character_name, right_character_name)
    //EXTERNAL change_emotion(emotion, ID)
    //{place_characters("Charcter", "Character 1")}
    //{change_emotion("Angry", 0)} changes the character on the left to be angry
 
    //left = 0, right = 1


    public GameObject[] characters;
    public List<GameObject> charactersList = new List<GameObject>();
    [SerializeField]
    Vector3 leftPosition, rightPosition;

    List<Character> activeCharacters = new List<Character>();

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < characters.Length; i++)
        {
            GameObject newCharacter = Instantiate(characters[i]);
            newCharacter.SetActive(false);
            newCharacter.name = characters[i].name;
            charactersList.Add(newCharacter);
        }
    }
    public void PlaceCharacters(string leftCharacterName, string rightCharacterName)
    {
        //activeCharacters.Clear();
        foreach (GameObject gO in charactersList)
        {
            if(gO.name == leftCharacterName)
            {
                gO.SetActive(true);
                gO.GetComponent<Character>().ID = 0;
                activeCharacters.Add(gO.GetComponent<Character>());
                gO.transform.position = leftPosition;
            } else if(gO.name == rightCharacterName)
            {
                gO.SetActive(true);
                gO.GetComponent<Character>().ID = 1;
                activeCharacters.Add(gO.GetComponent<Character>());
                gO.transform.position = rightPosition;
            }
        
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeCharacterEmotion(string emotion, int ID)
    {
        foreach (Character character in activeCharacters)
        {
            if(character.gameObject.activeInHierarchy)
            {
                if (character.ID == ID)
                {
                    character.ChangeEmotion(emotion);
                }
            }
        }
    }
}
