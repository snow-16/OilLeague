using System;
using UniRx;
using UnityEngine;

/// <summary>
/// 音響再生用コンポーネント
/// </summary>
public class AudioPlayer : MonoBehaviour
{
    /// <summary> インスタンス </summary>
    public static AudioPlayer Instance { get; private set; }

    /// <summary> BGM用オーディオソース </summary>
    private AudioSource _bgmSource;
    /// <summary> SE用オーディオソース </summary>
    private AudioSource _seSource;

    void Awake()
    {
        Instance = this;
        var audioSources = GetComponents<AudioSource>();
        _bgmSource = audioSources[0];
        _seSource = audioSources[1];

        PlayBGM(AudioBGMType.Battle);
    }

    /// <summary>
    /// 任意のBGMを再生する
    /// </summary>
    /// <param name="bgmType">BGMの種類</param>
    public void PlayBGM(AudioBGMType bgmType)
    {
        _bgmSource.clip = AudioDataBase.Data.AllBGMData[bgmType].audioFile;
        _bgmSource.Play();
    }

    /// <summary>
    /// BGMを止める
    /// </summary>
    public void StopBGM()
    {
        _bgmSource.Stop();
    }

    /// <summary>
    /// 任意のSEを再生する
    /// </summary>
    /// <param name="seType">SEの種類</param>
    /// <param name="seStopTrigger">SEを強制停止する条件</param>
    public void PlaySE(AudioSEType seType, Func<bool> seStopTrigger = null)
    {
        //個別に停止処理を働かせるため、新たにSE専用のオブジェクトを生成
        var sePlayer = new GameObject("SE").AddComponent<AudioSource>();
        sePlayer.outputAudioMixerGroup = _seSource.outputAudioMixerGroup;
        sePlayer.clip = AudioDataBase.Data.AllSEData[seType].audioFile;
        sePlayer.Play();

        if(seStopTrigger != null)
        {
            //条件を満たした場合、対象のSEのみを停止する
            Observable.EveryUpdate().Where(_ => seStopTrigger != null ? seStopTrigger() : !sePlayer.isPlaying).First().Subscribe(_ => Destroy(sePlayer.gameObject));
        }
    }
}
