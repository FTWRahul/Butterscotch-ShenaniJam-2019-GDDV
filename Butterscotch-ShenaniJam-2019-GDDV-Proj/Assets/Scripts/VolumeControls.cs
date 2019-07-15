using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeControls : MonoBehaviour
{
    public AudioMixer masterMixer;


    public void SetSfxLv(float sfxLvl)
    {
        masterMixer.SetFloat("sfxVol", sfxLvl);
    }

    public void SetMusicLv(float musicLvl)
    {
        masterMixer.SetFloat("musicVol", musicLvl);
    }

    public void SetVoiceContLv(float voiceContLvl)
    {
        masterMixer.SetFloat("voiceContVol", voiceContLvl);
    }

}
