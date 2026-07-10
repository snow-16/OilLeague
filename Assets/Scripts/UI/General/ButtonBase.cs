using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// ボタンUIの基底クラス
/// </summary>
public abstract class ButtonBase : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary> ボタンのImageコンポーネント </summary>
    protected Image _buttonImage;
    /// <summary> 現在押されているか </summary>
    protected bool _isPressing = false;
    /// <summary> 現在マウスオーバーされているか </summary>
    protected bool _isTargeting = false;

    protected virtual void Awake()
    {
        _buttonImage = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ClickAction();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressing = true;
        PointerUpDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressing = false;
        PointerUpDown();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isTargeting = true;
        PointerInOut();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isTargeting = false;
        _isPressing = false;
        PointerInOut();
    }

    protected virtual void ClickAction()
    {
        
    }

    protected virtual void PointerInOut()
    {
        
    }

    protected virtual void PointerUpDown()
    {
        
    }
}
