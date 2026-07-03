using UniRx;
using UnityEngine.InputSystem;
using UnityEngine;
using System;
using System.Linq;

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
                        .Select(newPosition => newPosition - latePosition))
                    .Where(pointerMoveVector => pointerMoveVector.magnitude > 1 / GeneralDataBase.Data.FlickSensitivity);

                //フリック
                _pointerFlick
                    .TakeUntil(_pointerRelease)
                    .Take(1)
                    .Subscribe(pointerMoveVector => SubscribeFlick(pointerMoveVector));

                //タップ
                _pointerRelease
                    .TakeUntil(_pointerFlick)
                    .Take(1)
                    .Subscribe(tapPosition => SubscribeTap(tapPosition));

                //長押し
                Observable.EveryFixedUpdate()
                    .TakeUntil(_pointerFlick)
                    .TakeUntil(_pointerRelease)
                    .Subscribe(_ => SubscribeHold());
            }
        );
    }

    private static void SubscribeHold()
    {
        InputListLocalData.CanHolds.ForEach(receiveHold => receiveHold.OnHold());
    }

    private static void SubscribeTap(Vector2 tapPosition)
    {
        InputListLocalData.CanTaps.ForEach(receiveTap => receiveTap.OnTap(tapPosition));
    }

    private static void SubscribeFlick(Vector2 pointerMoveVector)
    {
        InputListLocalData.CanFlicks.ForEach(receiveFlick => receiveFlick.OnFlick(pointerMoveVector));
    }
}
