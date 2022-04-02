/*
Audio.cs
Audrey Bernier Larose *** Based on Greg Eads's video tutorial at: https://www.youtube.com/watch?v=9ROolmPSC70
301166198
Last Modified: 2/13/2022
Description: Manages audio sound in the scene based on audio settings
Revision: 
*/

using UnityEngine;

public class Audio : MonoBehaviour
{
    //Private
    private static readonly string MusicPref = "MusicPref";
    private static readonly string SoundEffectsPref = "SoundEffectsPref";
    private float musicFloat, soundEffectsFloat;

    //Public
    public AudioSource musicAudio;
    //public AudioSource[] soundEffectsAudio;

    void Awake() {
        ContinueSettings();
    }

    ///Gets and sets the player preferences of the sounds based on player preferences
    private void ContinueSettings() {
        musicFloat = PlayerPrefs.GetFloat(MusicPref);
        soundEffectsFloat = PlayerPrefs.GetFloat(SoundEffectsPref);
        
        musicAudio.volume = musicFloat;

        /*for (int i = 0; i < soundEffectsAudio.Length; i++) {
            soundEffectsAudio[i].volume = soundEffectsFloat;
        }*/
    }
}
