using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceTesting : MonoBehaviour
{
    private KeywordRecognizer keywordRecog;
    [SerializeField]
    public List<string> keywordList;

    void Start()
    {
        keywordRecog = new KeywordRecognizer(keywordList.ToArray());
        keywordRecog.OnPhraseRecognized += OnPhraseRecognized;
        keywordRecog.Start();
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log(args.text + " " + "("+ args.confidence + ")");
        //StringBuilder builder = new StringBuilder();
        //builder.AppendFormat("{0} ({1}){2}", args.text, args.confidence, Environment.NewLine);
        //builder.AppendFormat("\tTimestamp: {0}{1}", args.phraseStartTime, Environment.NewLine);
        //builder.AppendFormat("\tDuration: {0} seconds{1}", args.phraseDuration.TotalSeconds, Environment.NewLine);
        //Debug.Log(builder.ToString());
    }
}
