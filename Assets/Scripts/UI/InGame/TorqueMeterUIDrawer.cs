using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TorqueMeterUIDrawer : MonoBehaviour
{
    private Image _chargeMeterImage;
    private Image _storeMeterImage;
    private Image _frameImage;
    private TextMeshProUGUI _storeText;
    private TextMeshProUGUI _chargeText;

    void Awake()
    {
        _chargeMeterImage = transform.GetChild(1).GetComponent<Image>();
        _storeMeterImage = transform.GetChild(2).GetComponent<Image>();
        _frameImage = transform.GetChild(3).GetComponent<Image>();
        _storeText = transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        _chargeText = transform.GetChild(5).GetComponent<TextMeshProUGUI>();
    }

    void FixedUpdate()
    {
        _chargeMeterImage.fillAmount = SpinnerLocalData.ChargeTorque / SpinnerParameterDataBase.Data.MaxTorque;
        _storeMeterImage.fillAmount = SpinnerLocalData.Torque / SpinnerParameterDataBase.Data.MaxTorque;
        var dangerProgress = Mathf.Max(SpinnerLocalData.ChargeTorque - SpinnerParameterDataBase.Data.MaxTorque, 0) / (SpinnerParameterDataBase.Data.TorqueCriticalLimit - SpinnerParameterDataBase.Data.MaxTorque);
        _frameImage.color = new Color(1f, 1 - dangerProgress, 1 - dangerProgress, 1);

        if(dangerProgress > 0)
        {
            transform.localPosition = Vector2.zero + new Vector2(dangerProgress * Random.Range(-1.00f, 1.01f) * 5, dangerProgress * Random.Range(-1.00f, 1.01f) * 5);
        }
        else if(transform.localPosition != Vector3.zero)
        {
            transform.localPosition = Vector2.zero;
        }
    }
}
