using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Debug = UnityEngine.Debug;

/// <summary>
/// 各種入力を受け付けるインスタンスのリストデータ
/// </summary>
public class InputListLocalData
{
    /// <summary> タップ受け取り可能なインスタンス </summary>
    private static List<IReceivePress> _canPresses;
    /// <summary> タップ受け取り可能なインスタンス </summary>
    public static List<IReceivePress> CanPresses => new(_canPresses = _canPresses.Where(item => item != null).ToList());

    /// <summary> タップ受け取り可能なインスタンス </summary>
    private static List<IReceiveTap> _canTaps;
    /// <summary> タップ受け取り可能なインスタンス </summary>
    public static List<IReceiveTap> CanTaps => new(_canTaps = _canTaps.Where(item => item != null).ToList());

    /// <summary> フリック受け取り可能なインスタンス </summary>
    private static List<IReceiveFlick> _canFlicks;
    /// <summary> フリック受け取り可能なインスタンス </summary>
    public static List<IReceiveFlick> CanFlicks => new(_canFlicks = _canFlicks.Where(item => item != null).ToList());

    /// <summary> 長押し受け取り可能なインスタンス </summary>
    private static List<IReceiveHold> _canHolds;
    /// <summary> 長押し受け取り可能なインスタンス </summary>
    public static List<IReceiveHold> CanHolds => new(_canHolds = _canHolds.Where(item => item != null).ToList());

    /// <summary>
    /// インスタンスの取得。
    /// ライタークラスからのみアクセス可能
    /// </summary>
    /// <returns>インスタンス</returns>
    public static InputListLocalData Access()
    {
        var accessedClass = new StackFrame(1).GetMethod()?.ReflectedType;
        if(accessedClass == typeof(InputListDataWriter))
        {
            return new InputListLocalData();
        }

        Debug.LogError("アクセス権限がありません。");
        return null;
    }

    /// <summary>
    /// 押下リスト書き換え
    /// </summary>
    /// <param name="list">押下受け取り可能なインスタンス</param>
    public void SetPressList(List<IReceivePress> list)
    {
        _canPresses = list;
    }

    /// <summary>
    /// タップリスト書き換え
    /// </summary>
    /// <param name="list">タップ受け取り可能なインスタンスリスト</param>
    public void SetTapList(List<IReceiveTap> list)
    {
        _canTaps = list;
    }

    /// <summary>
    /// フリックリスト書き換え
    /// </summary>
    /// <param name="list">フリック受け取り可能なインスタンスリスト</param>
    public void SetFlickList(List<IReceiveFlick> list)
    {
        _canFlicks = list;
    }

    /// <summary>
    /// 長押しリスト書き換え
    /// </summary>
    /// <param name="list">長押し受け取り可能なインスタンス</param>
    public void SetHoldList(List<IReceiveHold> list)
    {
        _canHolds = list;
    }
}
