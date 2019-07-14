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

    Sequence TextBubble;
    Ease easeType;
    public Color TextColour;

    [SerializeField]
    bool isRealPlayer;

    public override void OnStartClient()
    {
        if (hasAuthority)
        {
            playerMove = GetComponent<PlayerMove>();
            keywordRecog = new KeywordRecognizer(keywordList.ToArray());
            keywordRecog.OnPhraseRecognized += OnPhraseRecognized;
            keywordRecog.Start();
        }
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log(args.text + " " + "("+ args.confidence + ")");
        text.text = args.text;

        if(speedMultiplyer < maxSpeedMultiplyer)
        {
            speedMultiplyer += speedMultiplyerFactor;
            CmdTextBubbles(GetComponent<NetworkIdentity>().netId.ToString(), args.text.ToString());
            //Debug.Log("SENT IDENTITY" + GetComponent<NetworkIdentity>().netId.ToString());
        }
    }

    private void Update()
    {
        if(hasAuthority)
        {
            if (!spawnedCam)
            {
                
                playerMove = GetComponent<PlayerMove>();
                keywordRecog = new KeywordRecognizer(keywordList.ToArray());
                keywordRecog.OnPhraseRecognized += OnPhraseRecognized;
                keywordRecog.Start();
                spawnedCam = !spawnedCam;
            }

            if(Input.GetKeyDown(KeyCode.P))
            {
                //CmdRealPlayerCall(GetComponent<NetworkIdentity>().netId.ToString());
                RealPlayerCall(connectionToClient, GetComponent<NetworkIdentity>().netId.ToString());
                //StartSync = true;
            }

            if (speedMultiplyer > 1)
            {
                playerMove.speedMultiplyer = speedMultiplyer;
                speedMultiplyer -= Time.deltaTime * decayMultiplyer;
            }
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

