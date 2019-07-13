using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;
using UnityEngine.Networking;

public class VoiceTesting : NetworkBehaviour
{
    private KeywordRecognizer keywordRecog;
    [SerializeField]
    public List<string> keywordList;
    public Text text;
    public AudioSource playerAudioSource;
    PlayerMove playerMove;

    public float speedMultiplyer;
    public float speedMultiplyerFactor;
    public float maxSpeedMultiplyer;
    [Range(0,1)]
    public float decayMultiplyer;

    public override void OnStartClient()
    {
        if (hasAuthority)
        {
            playerMove = GetComponent<PlayerMove>();
            //playerAudioSource = GetComponent<AudioSource>();
            //playerAudioSource.clip = Microphone.Start(null, true, 10, 44100);
            //playerAudioSource.mute = false;
            ////playerAudioSource.loop = true;
            //while (!(Microphone.GetPosition(null) > 0)){ Debug.Log("Nada"); }
            //Debug.Log("Here");
            //playerAudioSource.Play();
            keywordRecog = new KeywordRecognizer(keywordList.ToArray());
            keywordRecog.OnPhraseRecognized += OnPhraseRecognized;
            keywordRecog.Start();
        }
    }
    //void Start()
    //{
    //    if(hasAuthority)
    //    {
    //        playerMove = GetComponent<PlayerMove>();
    //        //playerAudioSource = GetComponent<AudioSource>();
    //        //playerAudioSource.clip = Microphone.Start(null, true, 10, 44100);
    //        //playerAudioSource.mute = false;
    //        ////playerAudioSource.loop = true;
    //        //while (!(Microphone.GetPosition(null) > 0)){ Debug.Log("Nada"); }
    //        //Debug.Log("Here");
    //        //playerAudioSource.Play();
    //        keywordRecog = new KeywordRecognizer(keywordList.ToArray());
    //        keywordRecog.OnPhraseRecognized += OnPhraseRecognized;
    //        keywordRecog.Start();
    //    }
    //}

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        //Debug.Log(args.text + " " + "("+ args.confidence + ")");
        text.text = args.text;
        if(speedMultiplyer < maxSpeedMultiplyer)
        {
            speedMultiplyer += speedMultiplyerFactor;
            playerMove.CmdTextBubbles();
        }
        //StringBuilder builder = new StringBuilder();
        //builder.AppendFormat("{0} ({1}){2}", args.text, args.confidence, Environment.NewLine);
        //builder.AppendFormat("\tTimestamp: {0}{1}", args.phraseStartTime, Environment.NewLine);
        //builder.AppendFormat("\tDuration: {0} seconds{1}", args.phraseDuration.TotalSeconds, Environment.NewLine);
        //Debug.Log(builder.ToString());
    }

    private void Update()
    {
        //Debug.Log(speedMultiplyer);
        if(hasAuthority)
        {
            if (speedMultiplyer > 1)
            {
                playerMove.speedMultiplyer = speedMultiplyer;
                speedMultiplyer -= Time.deltaTime * decayMultiplyer;
            }
        }       
    }
}
