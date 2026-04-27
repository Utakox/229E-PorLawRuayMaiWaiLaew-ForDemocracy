using UnityEngine;

public class MusicStopOnClick : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClicked ()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Stop();
        }

        else if (audioSource == null)
        {
            audioSource.Play();
        }
    }
}
