using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 音楽ファイルデータベース
/// </summary>
public class AudioDataBase : MonoBehaviour
{
    /// <summary> BGMデータ </summary>
    [SerializeField]
    private List<BGMData> _allBGMData;
    /// <summary> SEデータ </summary>
    [SerializeField]
    private List<SEData> _allSEData;

    /// <summary> BGMデータ </summary>
    public Dictionary<AudioBGMType, BGMData> AllBGMData => _allBGMData.ToDictionary(data => data.type);
    /// <summary> SEデータ </summary>
    public Dictionary<AudioSEType, SEData> AllSEData => _allSEData.ToDictionary(data => data.type);

    /// <summary> インスタンス </summary>
    public static AudioDataBase Data { get; private set; }

    public void SetData()
    {
        Data = this;
    }
    
    [Serializable]
    public struct BGMData
    {
        public AudioBGMType type;
        public AudioClip audioFile;
    }

    [Serializable]
    public struct SEData
    {
        public AudioSEType type;
        public AudioClip audioFile;
    }
}
