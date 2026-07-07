using System.Linq;
using UnityEngine;

public class SpinnerInstanceData : MonoBehaviour
{
    [SerializeField]
    private SpinnerType _type;
    public SpinnerType Type => _type;

    void Awake()
    {
        GetComponents<IWriteSpinnerInstance>().ToList().ForEach(writeable => writeable.GiveWriter(new SpinnerInstanceWriter(ref _type)));
    }

    public class SpinnerInstanceWriter
    {
        public SpinnerType Type { get; set; }

        public SpinnerInstanceWriter(ref SpinnerType type)
        {
            Type = type;
        }
    }
}
