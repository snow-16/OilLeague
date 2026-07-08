using System.Diagnostics;

using Debug = UnityEngine.Debug;

/// <summary>
/// InputListLocalDataの書き換えクラス
/// </summary>
public class InputListDataWriter
{
    /// <summary> データクラスのインスタンス </summary>
    public InputListLocalData Data { get; }

    private InputListDataWriter()
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
        if(typeof(IReceivePress).IsAssignableFrom(accessedClass) || typeof(IReceiveTap).IsAssignableFrom(accessedClass) || typeof(IReceiveFlick).IsAssignableFrom(accessedClass) || typeof(IReceiveHold).IsAssignableFrom(accessedClass) || accessedClass == typeof(GameManager))
        {
            return new InputListDataWriter();
        }

        Debug.LogError("アクセス権限がありません。");
        return null;
    }

    /// <summary>
    /// データの初期化
    /// </summary>
    public void Reset()
    {
        Data.SetPressList(new());
        Data.SetTapList(new());
        Data.SetFlickList(new());
        Data.SetHoldList(new());
    }

    /// <summary>
    /// 押下受け取り可能リスト追加
    /// </summary>
    /// <param name="addItem">追加インスタンス</param>
    public void AddPressList(IReceivePress addItem)
    {
        var list = InputListLocalData.CanPresses;
        list.Add(addItem);
        Data.SetPressList(list);
    }

    /// <summary>
    /// タップ受け取り可能リスト追加
    /// </summary>
    /// <param name="addItem">追加インスタンス</param>
    public void AddTapList(IReceiveTap addItem)
    {
        var list = InputListLocalData.CanTaps;
        list.Add(addItem);
        Data.SetTapList(list);
    }

    /// <summary>
    /// フリック受け取り可能リスト追加
    /// </summary>
    /// <param name="addItem">追加インスタンス</param>
    public void AddFlickList(IReceiveFlick addItem)
    {
        var list = InputListLocalData.CanFlicks;
        list.Add(addItem);
        Data.SetFlickList(list);
    }

    /// <summary>
    /// 長押し受け取り可能リスト追加
    /// </summary>
    /// <param name="addItem">追加インスタンス</param>
    public void AddHoldList(IReceiveHold addItem)
    {
        var list = InputListLocalData.CanHolds;
        list.Add(addItem);
        Data.SetHoldList(list);
    }
}
