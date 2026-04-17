using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Character : MonoBehaviour
{

    public Sprite[] emotions; //4 sprites
    SpriteRenderer spriteRend;
    public int ID; //Left = 0, right = 1
    public enum CharacterEmotion{
        happy, sad, normal, angry
    }
    [SerializeField]
    CharacterEmotion myState; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        myState = CharacterEmotion.normal;
        spriteRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeEmotion(string emotionName)
    {
        StopAllCoroutines();

        switch (emotionName.ToLower())
        {
            case "happy":
                SetEmotion(0, CharacterEmotion.happy);
                break;

            case "sad":
                SetEmotion(1, CharacterEmotion.sad);
                break;

            case "normal":
            case "neutral":
                SetEmotion(2, CharacterEmotion.normal);
                break;

            case "angry":
                SetEmotion(3, CharacterEmotion.angry);
                break;

            default:
                Debug.LogWarning("Unknown emotion: " + emotionName);
                break;
        }
    }
    private void SetEmotion(int index, CharacterEmotion state)
    {
        if (index < 0 || index >= emotions.Length)
        {
            Debug.LogError("Emotion index out of range!");
            return;
        }

        spriteRend.sprite = emotions[index];
        myState = state;
    }
    IEnumerator HappyState()
    {
        spriteRend.sprite = emotions[0];
        myState = CharacterEmotion.happy;
        yield return null;
    }
    IEnumerator SadState()
    {
        spriteRend.sprite = emotions[1];
        myState = CharacterEmotion.sad;
        yield return null;
    }
    IEnumerator NormalState()
    {
        spriteRend.sprite = emotions[2];
        myState = CharacterEmotion.normal;
        yield return null;
    }
    IEnumerator AngryState()
    {
        spriteRend.sprite = emotions[3];
        myState = CharacterEmotion.angry;
        yield return null;
    }
}
