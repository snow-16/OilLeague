using System.Linq;
using System.Threading.Tasks;
using Fusion;
using UniRx;

/// <summary>
/// ネットワーク関連の処理クラス
/// </summary>
public class NetworkingProcessor : IWriteNetworkingLocal
{
    /// <summary>
    /// <para>ネットワークランナー生成</para>
    /// <para>既に存在するなら消した後再生成する</para>
    /// </summary>
    public static void CreateNetworkRunner()
    {
        if(NetworkingLocalData.NetworkRunner != null)
        {
            ObjectSpawner.Instance.DestroySpawned(NetworkingLocalData.NetworkRunner.gameObject);
        }

        new NetworkingProcessor().SetRunner();
    }

    /// <summary>
    /// セッションを開始・作成する
    /// </summary>
    /// <param name="sessionCode">セッションの識別コード</param>
    /// <param name="playerCount">プレイヤーのキャパシティ</param>
    private static async Task StartSession(string sessionCode, int playerCount)
    {
        await NetworkingLocalData.NetworkRunner.StartGame(new StartGameArgs
            {
                GameMode = GameMode.Shared,
                SessionName = sessionCode,
                PlayerCount = playerCount,
                SceneManager = NetworkingLocalData.NetworkRunner.GetComponent<NetworkSceneManagerDefault>()
            }
        );
    }

    /// <summary>
    /// サーバー上に存在する全セッションの情報を取得する
    /// </summary>
    public static void GetSessionList()
    {
        SceneProcessor.TransitionToLobby();
        Observable.EveryUpdate().Where(_ => SceneProcessor.State == SceneState.Loading).First().Subscribe(async _ =>
            {
                CreateNetworkRunner();
                await NetworkingLocalData.NetworkRunner.JoinSessionLobby(SessionLobby.Shared);
            }
        );
    }

    /// <summary>
    /// セッション情報の取得後、ロビーに入室する
    /// </summary>
    public static async Task JoinLobby()
    {
        //接続プレイヤー数が規定値以上なら強制切断する
        if(NetworkingLocalData.AllSessions.Sum(session => session.PlayerCount) > GeneralDataBase.Data.MaxConnectablePlayerCount)
        {
            await NetworkingLocalData.NetworkRunner.Shutdown();
            return;
        }

        await StartSession("Lobby", GeneralDataBase.Data.MaxConnectablePlayerCount);
    }

    /// <summary>
    /// 部屋に合流・作成する
    /// </summary>
    /// <param name="sessionCode">部屋の識別コード</param>
    public static void CreateRoom(string sessionCode)
    {
        SceneProcessor.ChangeState(SceneState.TransitionStart);
        Observable.EveryUpdate().Where(_ => SceneProcessor.State == SceneState.Loading).First().Subscribe(async _ =>
            {
                //ロビーセッションを抜けて入室準備をする
                await NetworkingLocalData.NetworkRunner.Shutdown();
                CreateNetworkRunner();

                try
                {
                    await StartSession(sessionCode, GeneralDataBase.Data.RoomCapacity);
                    new NetworkingProcessor().SetPlayerNumber();
                    await SceneProcessor.TransitionScene("WaitingRoom");
                }
                catch
                {
                    //入室に失敗したらタイトルに戻る
                    SceneProcessor.TransitionToTitle();
                }
            }
        );
    }

    /// <summary>
    /// インゲームシーンへ移動しゲームを開始する
    /// </summary>
    public static void StartGame()
    {
        RPCSendSystem.Instance.RPC_PlayTransition();
    }

    /// <summary>
    /// 生成したネットワークランナーをデータに保存する
    /// </summary>
    private void SetRunner()
    {
        NetworkingDataWriter.Access(this).Data.SetRunner(ObjectSpawner.Instance.SpawnDontDestroy(GeneralDataBase.Data.NetworkRunnerPrefab).GetComponent<NetworkRunner>());
    }

    /// <summary>
    /// プレイヤー番号を設定させる
    /// </summary>
    private void SetPlayerNumber()
    {
        NetworkingDataWriter.Access(this).AssignPlayerNumber();
    }
}
