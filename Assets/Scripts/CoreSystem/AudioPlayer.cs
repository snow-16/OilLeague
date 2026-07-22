using System;
using UniRx;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer Instance { get; private set; }

    private AudioSource _bgmSource;
    private AudioSource _seSource;

    void Awake()
    {
        Instance = this;
        var audioSources = GetComponents<AudioSource>();
        _bgmSource = audioSources[0];
        _seSource = audioSources[1];

        PlayBGM(AudioBGMType.Battle);
    }

    public void PlayBGM(AudioBGMType bgmType)
    {
        _bgmSource.clip = AudioDataBase.Data.AllBGMData[bgmType].audioFile;
        _bgmSource.Play();
    }

    public void StopBGM()
    {
        _bgmSource.Stop();
    }

    public void PlaySE(AudioSEType seType, Func<bool> seStopTrigger = null)
    {
        var sePlayer = new GameObject("SE").AddComponent<AudioSource>();
        sePlayer.outputAudioMixerGroup = _seSource.outputAudioMixerGroup;
        sePlayer.clip = AudioDataBase.Data.AllSEData[seType].audioFile;
        sePlayer.Play();

        if(seStopTrigger != null)
        {
            Observable.EveryUpdate().Where(_ => seStopTrigger != null ? seStopTrigger() : !sePlayer.isPlaying).First().Subscribe(_ => Destroy(sePlayer.gameObject));
        }
    }
}
