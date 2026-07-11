using UnityEngine;

/// <summary>
/// プレハブオブジェクトの生成・破棄を管理するクラス
/// </summary>
public class ObjectSpawner : MonoBehaviour
{
    /// <summary> アクセス用インスタンス </summary>
    public static ObjectSpawner Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 通常の生成処理
    /// </summary>
    /// <param name="spawnObject">プレハブ</param>
    /// <returns></returns>
    public GameObject DefaultSpawn(GameObject spawnObject)
    {
        return Instantiate(spawnObject);
    }

    /// <summary>
    /// 生成したオブジェクトをDontDestroyOnLoadに入れて返す
    /// </summary>
    /// <param name="spawnObject">プレハブ</param>
    /// <returns>生成オブジェクト</returns>
    public GameObject SpawnDontDestroy(GameObject spawnObject)
    {
        var spawned = DefaultSpawn(spawnObject);
        DontDestroyOnLoad(spawned);
        return spawned;
    }

    /// <summary>
    /// オブジェクトの破棄
    /// </summary>
    /// <param name="destroyObject">破棄するオブジェクト</param>
    public void DestroySpawned(GameObject destroyObject)
    {
        Destroy(destroyObject);
    }
}
