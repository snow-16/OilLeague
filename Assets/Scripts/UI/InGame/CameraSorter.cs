using Unity.Cinemachine;
using UnityEngine;

public class CameraSorter : MonoBehaviour
{
    [SerializeField]
    private Transform _gameViewAnkerTop;
    [SerializeField]
    private Transform _gameViewAnkerLeft;
    [SerializeField]
    private Transform _gameViewAnkerCorner;
    [SerializeField]
    void Awake()
    {
        var offset = (_gameViewAnkerCorner.position - new Vector3(_gameViewAnkerLeft.position.x , _gameViewAnkerTop.position.y)) / 2;
        offset.z = -10;
        GetComponent<CinemachineFollow>().FollowOffset = offset;
    }
}
