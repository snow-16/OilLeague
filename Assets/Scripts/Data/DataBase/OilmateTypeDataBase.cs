using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// オイルメイトの種類ごとの設定データベース
/// </summary>
public class OilmateTypeDataBase : MonoBehaviour
{
    /// <summary> オイルメイトのプレハブ </summary>
    [SerializeField]
    private GameObject _oilmatePrefab;
    /// <summary> オイルメイトタイプごとのデータ </summary>
    [SerializeField]
    private List<OilmateTypeData> _allTypesData;

    /// <summary> オイルメイトのプレハブ </summary>
    public GameObject OilmatePrefab => _oilmatePrefab;
    /// <summary> オイルメイトタイプごとのデータ </summary>
    public Dictionary<SpinnerType, OilmateTypeData> AllTypesData => _allTypesData.ToDictionary(data => data.type);

    /// <summary> データのインスタンス </summary>
    public static OilmateTypeDataBase Data { get; private set; }

    public void SetData()
    {
        Data = this;
    }
    
    [Serializable]
    public struct OilmateTypeData
    {
        public SpinnerType type;
        public List<Sprite> sprites;
    }
}
