using System;
using System.Collections.Generic;
using Fusion;
using UniRx;
using UnityEngine;

/// <summary>
/// シーンが読み込まれたことを検知するクラス
/// </summary>
public abstract class SceneLoadedAnker : MonoBehaviour
{
    /// <summary> 読み込み時に生成されるネットワークオブジェクト群 </summary>
    [SerializeField]
    protected List<NetworkBehaviour> _networkedPrefabs = new();
    /// <summary> プレイヤー退室検知用プレハブ </summary>
    [SerializeField]
    private GameObject _playerExistDataPrefab;

    /// <summary> オブジェクトの生成数 </summary>
    public int GeneratedCount { get; private set; }

    public void Awake()
    {
        //ホストのみがネットワーク同期させるオブジェクトをスポーンさせる
        if(NetworkingLocalData.PlayerNumber == 1)
        {
            _networkedPrefabs.ForEach(async prefab => await ObjectSpawner.Instance.SpawnNetwork(prefab.gameObject));
        }
    }

    /// <summary>
    /// <para>オブジェクト生成完了条件の設定、</para>
    /// <para>およびその後の追加処理の設定</para>
    /// </summary>
    /// <param name="trigger"></param>
    /// <param name="whenGenerated"></param>
    protected void SetGenerateEndTrigger(Func<bool> trigger, Action whenGenerated = null)
    {
        Observable.EveryUpdate().Where(_ => trigger()).First().Subscribe(async _ =>
            {
                //全てのオブジェクトの生成が完了したら暗転を明けさせる
                SceneProcessor.ChangeState(SceneState.TransitionEnd);

                //プレイヤー退室検知用オブジェクト生成
                await ObjectSpawner.Instance.SpawnNetwork(_playerExistDataPrefab, (runner, obj) => obj.GetComponent<PlayerExistServerData>().SetNumber(NetworkingLocalData.PlayerNumber));

                whenGenerated();
            }
        );
    }

    /// <summary>
    /// シーン読み込み後の処理
    /// </summary>
    protected abstract void WhenLoaded();

    /// <summary>
    /// ネットワークオブジェクト生成完了時に呼ばれる関数
    /// </summary>
    public void OnGenerated()
    {
        GeneratedCount++;
    }
}
