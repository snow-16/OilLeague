using System.Diagnostics;

using Debug = UnityEngine.Debug;

/// <summary>
/// InputListLocalDataの書き換えクラス
/// </summary>
public class InputListDataWriter
{
    /// <summary> データクラスのインスタンス </summary>
    public InputListLocalData Data { get; }

    public InputListDataWriter()
    {
        Data = InputListLocalData.Access();
    }

    /// <summary>
    /// インスタンスの取得。
    /// ゲームマネージャー・入力受け取りクラスからのみアクセス可能
    /// </summary>
    /// <returns>インスタンス</returns>
    public static InputListDataWriter Access()
    {
        var accessedClass = new StackFrame(1).GetMethod()?.ReflectedType;
        if(typeof(IReceiveTap).IsAssignableFrom(accessedClass) || typeof(IReceiveFlick).IsAssignableFrom(accessedClass) || typeof(IReceiveHold).IsAssignableFrom(accessedClass) || accessedClass == typeof(GameManager))
        {
            return new InputListDataWriter();
        }

        Debug.LogError("アクセス権限がありません。");
        return null;
    }

    public void Reset()
    {
        Data.SetTapList(new());
        Data.SetFlickList(new());
        Data.SetHoldList(new());
    }

    public void AddTapList(IReceiveTap addItem)
    {
        var list = InputListLocalData.CanTaps;
        list.Add(addItem);
        Data.SetTapList(list);
    }

    public void AddFlickList(IReceiveFlick addItem)
    {
        var list = InputListLocalData.CanFlicks;
        list.Add(addItem);
        Data.SetFlickList(list);
    }

    public void AddHoldList(IReceiveHold addItem)
    {
        var list = InputListLocalData.CanHolds;
        list.Add(addItem);
        Data.SetHoldList(list);
    }
}
