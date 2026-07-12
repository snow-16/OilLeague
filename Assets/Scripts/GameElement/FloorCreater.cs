using UnityEngine;

public class FloorCreater : MonoBehaviour
{
    void Awake()
    {
        GetComponent<SpriteRenderer>().size = GeneralDataBase.Data.FieldRadius * Vector2.one / 10;
        transform.GetChild(0).localScale = GeneralDataBase.Data.FieldRadius * Vector3.one / 10;
    }
}
