using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// スピナーの種類ごとの設定データベース
/// </summary>
public class SpinnerTypeDataBase : MonoBehaviour
{
    /// <summary> スピナーのプレハブ </summary>
    [SerializeField]
    private GameObject _spinnerPrefab;
    /// <summary> スピナータイプごとのデータ </summary>
    [SerializeField]
    private List<SpinnerTypeData> _allTypesData;

    /// <summary> スピナーのプレハブ </summary>
    public GameObject SpinnerPrefab => _spinnerPrefab;
    /// <summary> スピナータイプごとのデータ </summary>
    public Dictionary<SpinnerType, SpinnerTypeData> AllTypesData => _allTypesData.ToDictionary(data => data.type);

    /// <summary> データのインスタンス </summary>
    public static SpinnerTypeDataBase Data { get; private set; }

    public void SetData()
    {
        Data = this;
    }
    
    [Serializable]
    public struct SpinnerTypeData
    {
        public SpinnerType type;
        public string name;
        public Color color;
        public List<Sprite> sprites;
    }
}
