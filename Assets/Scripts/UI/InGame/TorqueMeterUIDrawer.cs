using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// トルク表示UI
/// </summary>
public class TorqueMeterUIDrawer : MonoBehaviour
{
    /// <summary> チャージトルク用ゲージ </summary>
    private Image _chargeMeterImage;
    /// <summary> 保持トルク用ゲージ </summary>
    private Image _storeMeterImage;
    /// <summary> 外枠のイメージ </summary>
    private Image _frameImage;
    /// <summary> 保持トルク表記テキスト </summary>
    private TextMeshProUGUI _storeText;
    /// <summary> チャージトルク表記テキスト </summary>
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

        //チャージトルクが危険域ならメーターを震わせる
        if(dangerProgress > 0)
        {
            ((RectTransform)transform).anchoredPosition = Vector2.zero + new Vector2(dangerProgress * Random.Range(-1.00f, 1.01f) * 5, dangerProgress * Random.Range(-1.00f, 1.01f) * 5);
        }
        else if(((RectTransform)transform).anchoredPosition != Vector2.zero)
        {
            ((RectTransform)transform).anchoredPosition = Vector2.zero;
        }

        _storeText.text = SpinnerLocalData.Torque.ToString("000");
        _chargeText.text = SpinnerLocalData.ChargeTorque.ToString("000");
    }
}
