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
        Data = InputListLocalData.Access(this);
    }

    /// <summary>
    /// インスタンスの取得。
    /// ゲームマネージャー・入力受け取りクラスからのみアクセス可能
    /// </summary>
    /// <returns>インスタンス</returns>
    public static InputListDataWriter Access(object accessed)
    {
        if(accessed is IReceivePress || accessed is IReceiveTap || accessed is IReceiveFlick || accessed is IReceiveHold || accessed is GameManager)
        {
            return new InputListDataWriter();
        }

        Debug.LogError($"{nameof(InputListDataWriter)}へのアクセス権限がありません。");
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
    public InputListDataWriter AddPressList(IReceivePress addItem)
    {
        var list = InputListLocalData.CanPresses;
        list.Add(addItem);
        Data.SetPressList(list);
        return this;
    }

    /// <summary>
    /// タップ受け取り可能リスト追加
    /// </summary>
    /// <param name="addItem">追加インスタンス</param>
    public InputListDataWriter AddTapList(IReceiveTap addItem)
    {
        var list = InputListLocalData.CanTaps;
        list.Add(addItem);
        Data.SetTapList(list);
        return this;
    }

    /// <summary>
    /// フリック受け取り可能リスト追加
    /// </summary>
    /// <param name="addItem">追加インスタンス</param>
    public InputListDataWriter AddFlickList(IReceiveFlick addItem)
    {
        var list = InputListLocalData.CanFlicks;
        list.Add(addItem);
        Data.SetFlickList(list);
        return this;
    }

    /// <summary>
    /// 長押し受け取り可能リスト追加
    /// </summary>
    /// <param name="addItem">追加インスタンス</param>
    public InputListDataWriter AddHoldList(IReceiveHold addItem)
    {
        var list = InputListLocalData.CanHolds;
        list.Add(addItem);
        Data.SetHoldList(list);
        return this;
    }
}
