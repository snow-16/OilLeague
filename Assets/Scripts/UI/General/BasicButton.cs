using UniRx;
using UnityEngine;

/// <summary>
/// シンプルなボタンUIの基底クラス
/// </summary>
public abstract class BasicButton : ButtonBase
{
    [SerializeField]
    private Color[] targetedColor = new Color[2];
    [SerializeField]
    private Vector2 pressingOffset;

    protected override void Awake()
    {
        base.Awake();

        this.ObserveEveryValueChanged(_ => _isPressing).Subscribe(isPressing =>
            {
                ((RectTransform)transform).anchoredPosition = _basePos + (isPressing ? pressingOffset : Vector2.zero);
            }
        );

        this.ObserveEveryValueChanged(_ => _isTargeting).Subscribe(isTargeting =>
            {
                _buttonImage.color = targetedColor[isTargeting ? 1 : 0];
            }
        );
    }
}
