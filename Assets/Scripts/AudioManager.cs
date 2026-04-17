using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource sfxSource;

    public AudioClip bed;
    public AudioClip doorbell;
    public AudioClip chatting;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySFX(string name)
    {
        switch(name)
        {
            case "bed":
                sfxSource.PlayOneShot(bed);
                break;

            case "doorbell":
                sfxSource.PlayOneShot(doorbell);
                break;

            case "chatting":
                sfxSource.PlayOneShot(chatting);
                break;

            default:
                Debug.Log("No SFX sound for: " + name);
                break;
        }
    }
}
