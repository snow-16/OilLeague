using UniRx;
using UnityEngine;

/// <summary>
/// シンプルなボタンUIの基底クラス
/// </summary>
public abstract class BasicButton : ButtonBase
{
    [SerializeField]
    private Color[] _targetedColor = new Color[2];
    [SerializeField]
    private Vector2 _pressingOffset;

    private Color _baseColor;

    protected override void Awake()
    {
        base.Awake();
        _baseColor = _buttonImage.color;

        this.ObserveEveryValueChanged(_ => _isPressing).Subscribe(isPressing =>
            {
                ((RectTransform)transform).anchoredPosition = _basePos + (isPressing ? _pressingOffset : Vector2.zero);
            }
        );

        this.ObserveEveryValueChanged(_ => _isTargeting).Subscribe(isTargeting =>
            {
                _buttonImage.color = _baseColor - (Color.white - _targetedColor[isTargeting ? 1 : 0]);
            }
        );

        this.ObserveEveryValueChanged(_ => _canPush).Subscribe(canPush =>
            {
                _buttonImage.color = _baseColor - (Color.white - _targetedColor[canPush ? 0 : 1]);
            }
        );
    }
}
