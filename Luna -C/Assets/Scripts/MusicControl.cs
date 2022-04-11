using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicControl : MonoBehaviour
{
    public static MusicControl instance;

    public bool playing;
    public AudioSource musicSource;
    public AudioClip[] musicClips;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        playing = true;
        StartCoroutine(PlayMusicLoop());
     }

    private void Update()
    {
        checkMainMenu();
    }

    IEnumerator PlayMusicLoop()
    {
        yield return null;

        while(playing)
        {
            for(int i = 0; i< musicClips.Length; i++)
            {
                musicSource.clip = musicClips[i];
                musicSource.Play();

                while(musicSource.isPlaying)
                {
                    yield return null;
                }
            }
        }
    }

    public void checkMainMenu()
    {
        
        int x = SceneManager.GetActiveScene().buildIndex;
        if(x == 0 )
        {
            Destroy(gameObject);
        }    
    }
}
