using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rhythmGameCode : MonoBehaviour
{
    // Song beats per minute
    //This is determined by the song you're trying to sync up to
    public float songBpm;
    //The number of seconds for each song beat
    public float secPerbeat;
    //keep all the position-in-beats of notes in the song
    float[] notes;
    //The number of seconds for each song beat
    public float secPerBeat;

    //the index of the next note to be spawned
    int nextIndex = 0;
    //Current song position, in seconds

    public float songPosition;

    //Current song position, in beats
    public float songPositionInBeats;

    //How many seconds have passed since the song started
    public float dspSongTime;

    //an AudioSource attached to this GameObject that will play the music
    public AudioSource musicSource;
    // Start is called before the first frame update
    void Start()
    {
        //Load the AudioSource attached to the Conductor GameObject
        musicSource = GetComponent<AudioSource>();

        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;

        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        //Start the music
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);

    
        //determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;
       
}
