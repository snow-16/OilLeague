using System.Collections.Generic;
using Fusion;
using Debug = UnityEngine.Debug;

/// <summary>
/// シーンに存在するネットワークシングルトンクラスのリストデータ
/// </summary>
public class SingletonsLocalData
{
    /// <summary> シングルトンリスト </summary>
    public static List<NetworkBehaviour> Singletons { get; private set; }

    private SingletonsLocalData()
    {
        
    }

    /// <summary>
    /// インスタンスの取得。
    /// ライタークラスからのみアクセス可能
    /// </summary>
    /// <returns>インスタンス</returns>
    public static SingletonsLocalData Access(object accessed)
    {
        if(accessed is SingletonsDataWriter)
        {
            return new SingletonsLocalData();
        }

        Debug.LogError($"{nameof(SingletonsLocalData)}へのアクセス権限がありません。");
        return null;
    }

    /// <summary>
    /// シングルトンリスト書き換え
    /// </summary>
    /// <param name="list">シングルトンリスト</param>
    public void SetSingletons(List<NetworkBehaviour> list)
    {
        Singletons = list;
    }
}
