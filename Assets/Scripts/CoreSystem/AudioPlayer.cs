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
}
