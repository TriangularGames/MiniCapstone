using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip[] audioClips;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClip(string name)
    {
        foreach (AudioClip clip in audioClips)
        {
            if (clip.name == name)
            {
                audioSource.PlayOneShot(clip);
            }
        }
    }
}
