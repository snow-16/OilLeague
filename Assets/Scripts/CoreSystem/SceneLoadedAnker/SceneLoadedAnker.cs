using System.Collections.Generic;
using Fusion;
using UnityEngine;

/// <summary>
/// シーンが読み込まれたことを検知するクラス
/// </summary>
public class SceneLoadedAnker : MonoBehaviour
{
    /// <summary> 読み込み時に生成されるネットワークオブジェクト群 </summary>
    [SerializeField]
    protected List<NetworkBehaviour> _networkedPrefabs = new();

    public int Generated { get; private set; }

    public void Awake()
    {
        if(NetworkingLocalData.PlayerNumber == 1)
        {
            _networkedPrefabs.ForEach(async prefab => await NetworkingProcessor.SpawnObject(prefab.gameObject));
        }
        
        WhenLoaded();
    }

    protected virtual void WhenLoaded()
    {
        
    }

    public void OnGenerated()
    {
        Generated++;
    }
}
