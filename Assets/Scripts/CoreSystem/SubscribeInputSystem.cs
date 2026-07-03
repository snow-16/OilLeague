using UniRx;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

/// <summary>
/// UniRxストリーム設定用クラス
/// </summary>
public class SubscribeInputSystem
{
    private static IObservable<Vector2> _pointerPressed;
    private static IObservable<Vector2> _pointerPressing;
    private static IObservable<Vector2> _pointerRelease;


    /// <summary>
    /// 各入力処理をUniRxストリーム設定
    /// </summary>
    public static void SubscribeInputs()
    {
        //操作状態取得
        _pointerPressed = Observable.EveryUpdate()
            .Where(_ => InputSystem.actions["Tap"].WasPressedThisFrame())
            .Select(_ => Pointer.current.position.ReadValue());
        _pointerPressing = Observable.EveryUpdate()
            .Where(_ => InputSystem.actions["Tap"].IsPressed())
            .Select(_ => Pointer.current.position.ReadValue());
        _pointerRelease = Observable.EveryUpdate()
            .Where(_ => InputSystem.actions["Tap"].WasReleasedThisFrame())
            .Select(_ => Pointer.current.position.ReadValue());

        //画面タップ時
        _pointerPressed.Subscribe(_ =>
            {
                //フリック取得
                var _pointerFlick = _pointerPressing.SelectMany(latePosition => _pointerPressing
                        .First()
                        .Select(newPosition => (newPosition - latePosition).magnitude))
                    .Where(pointerMoveDistance => pointerMoveDistance > 5f);

                //フリック
                _pointerFlick
                    .TakeUntil(_pointerRelease)
                    .Take(1)
                    .Subscribe(pointerMoveDistance => SubscribeFlick(pointerMoveDistance));

                //タップ
                _pointerRelease
                    .TakeUntil(_pointerFlick)
                    .Take(1)
                    .Subscribe(_ => SubscribeTap());

                //長押し
                Observable.EveryFixedUpdate()
                    .TakeUntil(_pointerFlick)
                    .TakeUntil(_pointerRelease)
                    .Subscribe(_ => SubscribeCharge());
            }
        );
    }

    private static void SubscribeCharge()
    {
        
    }

    private static void SubscribeTap()
    {
        
    }

    private static void SubscribeFlick(float pointerMoveDistance)
    {
        
    }
}
