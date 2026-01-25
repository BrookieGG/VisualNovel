using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{

    public Sprite[] emotions; //4 sprites
    SpriteRenderer spriteRend;
    public int ID; //Left = 0, right = 1
    public enum CharacterEmotion
        {
        happy, sad, normal, angry
    }
    CharacterEmotion myState; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
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
        StartCoroutine(emotionName + "State");
    }
    IEnumerator HappyState()
    {
        spriteRend.sprite = emotions[0];
        yield return null;
    }
    IEnumerator SadState()
    {
        spriteRend.sprite = emotions[1];
        yield return null;
    }
    IEnumerator NormalState()
    {
        spriteRend.sprite = emotions[2];
        yield return null;
    }
    IEnumerator AngryState()
    {
        spriteRend.sprite = emotions[3];
        yield return null;
    }
}
