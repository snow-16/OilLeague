using System.Collections.Generic;
using Fusion;
using UnityEngine;

/// <summary>
/// シーンが読み込まれたことを検知するクラス
/// </summary>
public class SceneLoadedAnker : NetworkBehaviour
{
    /// <summary> 読み込み時に生成されるネットワークオブジェクト群 </summary>
    [SerializeField]
    private List<NetworkBehaviour> _networkedPrefabs = new();

    public override void Spawned()
    {
        
    }
}
