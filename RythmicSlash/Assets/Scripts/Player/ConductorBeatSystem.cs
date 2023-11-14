using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSystem : MonoBehaviour
{    
    //an AudioSource attached to this GameObject that will play the music.
    [SerializeField] private AudioSource musicSource;
    //Song beats per minute
    //Determiner par la chanson 
    [SerializeField] public float songBpm;
    //Nombre de secondes pour chaque beat
    [SerializeField] public float secPerBeat;

    //musique en cours, en secondes
    public float songPosition;
    //musique en cours, en beats
    public float songPositionInBeats;
    //Combien de secondes sont passées depuis le début de la chanson
    public float dspSongTime;

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
        //determine combien de secondes sont passées depuis le début de la chanson
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);

        //determine combien de beat depuis le début de la chanson
        songPositionInBeats = songPosition / secPerBeat;
    }
}
