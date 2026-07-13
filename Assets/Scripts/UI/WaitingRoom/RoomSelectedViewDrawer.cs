using UnityEngine;
using UnityEngine.UI;

public class RoomSelectedViewDrawer : MonoBehaviour
{
    private Image _selectedImage;
    
    void Awake()
    {
        _selectedImage = transform.GetChild(0).GetComponent<Image>();
    }

    void Update()
    {
        if(SceneProcessor.State == SceneState.Exist)
        {
            var ownType = RoomServerData.Instance.Players[NetworkingLocalData.PlayerNumber - 1].type;
            if(ownType != SpinnerType.None)
            {
                _selectedImage.gameObject.SetActive(true);
                _selectedImage.sprite = SpinnerTypeDataBase.Data.AllTypesData[ownType].sprites[(int)SpinnerState.Stop];
            }
            else
            {
                _selectedImage.gameObject.SetActive(false);
            }
        }
    }
}
