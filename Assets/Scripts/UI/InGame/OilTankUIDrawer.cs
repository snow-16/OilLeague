using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// オイル貯蔵タンクUI
/// </summary>
public class OilTankUIDrawer : MonoBehaviour
{
    /// <summary> データを表示するプレイヤーの番号 </summary>
    [SerializeField]
    private int _showingPlayersNumber;

    /// <summary> ゲージのイメージ </summary>
    private Image _oilGaugeImage;
    /// <summary> リットル表示のテキスト </summary>
    private TextMeshProUGUI _oilCountText;

    /// <summary> データ表示を行うか </summary>
    private bool _isShow = false;

    void Awake()
    {
        var spinnerViewer = transform.GetChild(4).GetComponent<Image>();

        if(NetworkingLocalData.NetworkRunner.SessionInfo.PlayerCount >= _showingPlayersNumber)
        {
            _isShow = true;
            _oilGaugeImage = transform.GetChild(1).GetComponent<Image>();
            _oilCountText = transform.GetChild(3).GetComponent<TextMeshProUGUI>();

            spinnerViewer.sprite = SpinnerTypeDataBase.Data.AllTypesData[PlayerSettingClientData.Players[_showingPlayersNumber - 1].type].sprites[(int)SpinnerState.Stop];
        }
        else
        {
            spinnerViewer.gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        if(SceneProcessor.State == SceneState.Exist && _isShow)
        {
            var amount = InGameServerData.Instance.OilTanks[_showingPlayersNumber - 1].oilAmount;
            var surplus = amount % 1000;
            _oilGaugeImage.fillAmount = surplus / 1000f;
            _oilCountText.text = $"{(amount - surplus) / 1000:000}L";
        }
    }
}
