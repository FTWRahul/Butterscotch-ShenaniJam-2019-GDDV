using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;
using UnityEngine.Networking;
using DG.Tweening;

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
    bool spawnedCam;

    public Vector3 localPlayer;
    [SerializeField]
    bool isRealPlayer;
    bool StartSync;

    Sequence TextBubble;
    Ease easeType;
    public Color TextColour;
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
        Debug.Log(args.text + " " + "("+ args.confidence + ")");
        text.text = args.text;

        if(speedMultiplyer < maxSpeedMultiplyer)
        {
            speedMultiplyer += speedMultiplyerFactor;
            CmdTextBubbles(GetComponent<NetworkIdentity>().netId.ToString(), args.text.ToString());
            Debug.Log("SENT IDENTITY" + GetComponent<NetworkIdentity>().netId.ToString());
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
            if(Input.GetKeyDown(KeyCode.P))
            {
                //CmdRealPlayerCall(GetComponent<NetworkIdentity>().netId.ToString());
                RealPlayerCall(connectionToClient, GetComponent<NetworkIdentity>().netId.ToString());
                //StartSync = true;
            }
            if (Input.GetButtonDown("Jump"))
            {
                CmdTextBubbles(GetComponent<NetworkIdentity>().netId.ToString(), "KeyPressTest");
            }
            if (!spawnedCam)
            {
                
                playerMove = GetComponent<PlayerMove>();
                keywordRecog = new KeywordRecognizer(keywordList.ToArray());
                keywordRecog.OnPhraseRecognized += OnPhraseRecognized;
                keywordRecog.Start();
                spawnedCam = !spawnedCam;
            }
            if (speedMultiplyer > 1)
            {
                playerMove.speedMultiplyer = speedMultiplyer;
                speedMultiplyer -= Time.deltaTime * decayMultiplyer;
            }

            if(StartSync)
            {
                //CmdFindLocalPlayer(GetComponent<NetworkIdentity>().netId.ToString());
                //text.transform.LookAt(localPlayer);
            }
        }
        else
        {

        }
    }

    [Command]
    void CmdRealPlayerCall(string id)
    {
        RealPlayerCall(connectionToClient, id);
    }

    [Client]
    void RealPlayerCall(NetworkConnection target, string id)
    {
        if (GetComponent<NetworkIdentity>().netId.ToString() == id)
        {
            TextLookAt text = transform.GetComponentInChildren<TextLookAt>();
            text.target = this.transform;
            text.isRealPlyaer = true;
            TextLookAt[] textArray = FindObjectsOfType<TextLookAt>();
            foreach (TextLookAt textScript in textArray)
            {
                if(!isRealPlayer)
                {
                    textScript.target = this.transform;
                    textScript.StartLookAt = true;
                }
            }
        }
    }




    [Command]
    void CmdFindLocalPlayer(string identity)
    {
      RpcFindLocalPlayer(identity);
    }

    [ClientRpc]
    void RpcFindLocalPlayer(string identity)
    {
        if(isRealPlayer)
        {
            NetworkTransform sendTransform = this.GetComponent<NetworkTransform>();
            CmdSendPlayerTransform(sendTransform.targetSyncPosition, identity);
            //NetworkIdentity[] netarray = FindObjectsOfType<NetworkIdentity>();
            // for (int i = 0; i < netarray.Length; i++)
            // {
            //     if(GetComponent<NetworkIdentity>().netId.ToString() == identity)
            //     {
            //         Transform sendTransform = this.transform;
            //         CmdSendPlayerTransform(sendTransform.position, identity);
            //     }
            // }
            // //foreach (NetworkIdentity netId in netarray)
            //{
            //    if (GetComponent<NetworkIdentity>().netId.ToString() == identity)
            //    {
            //      //netId.gameObject.GetComponent<VoiceTesting>().localPlayer = GetComponent<Transform>();
            //    }
            //}

            //BroadcastMessage("SetLocalPlayer", this.transform);
        }
    }

    [Command]
    void CmdSendPlayerTransform(Vector3 inPosition, string id)
    {
        RpcSendPlayerTransform(inPosition, id);
    }

    [ClientRpc]
    void RpcSendPlayerTransform(Vector3 inPosition, string id)
    {
        if(GetComponent<NetworkIdentity>().netId.ToString() == id)
        {
            localPlayer = inPosition;
        }
    }

    [Command]
    public void CmdTextBubbles(string identity, string inText)
    {
        Debug.Log("CMD Fired!");
      
        Debug.Log("CHECKING AGAINST THS ID " + GetComponent<NetworkIdentity>().netId.ToString());

        RpcTextBubble(identity, inText);
        
    }

    [ClientRpc]
    public void RpcTextBubble(string identity, string inText)
    {
        if (GetComponent<NetworkIdentity>().netId.ToString() == identity)
        {
            Debug.Log("CHECKING AGAINST THS ID " + GetComponent<NetworkIdentity>().netId.ToString());
            text.text = inText;
            TextBubble.Complete();
            text.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
            text.GetComponent<Text>().color = Color.white;
            text.transform.localPosition = Vector3.zero;
            TextBubble = DOTween.Sequence();
            TextBubble.Prepend(text.transform.DOLocalMove(Vector3.up * 2f, 1f).SetEase(easeType));
            TextBubble.Join(text.transform.DOScale(0.01f, 1f).SetEase(easeType));
            TextBubble.Join(text.GetComponent<Text>().DOColor(TextColour, 1f).SetEase(Ease.Linear));
        }
        else
        {
            Debug.Log(GetComponent<NetworkIdentity>().netId.ToString() + " Is not the player you are looking for");
        }
    }

}

