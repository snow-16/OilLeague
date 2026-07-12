using UnityEngine;

public class FloorCreater : MonoBehaviour
{
    void Awake()
    {
        transform.localScale = transform.GetChild(0).localScale = GeneralDataBase.Data.FieldRadius * 2 * Vector3.one;
    }
}
