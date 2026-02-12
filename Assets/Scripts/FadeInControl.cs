using UnityEngine;

public class FadeInControl : MonoBehaviour
{

    Fade fade;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fade = FindAnyObjectByType<Fade>();

        fade.FadeOut();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
