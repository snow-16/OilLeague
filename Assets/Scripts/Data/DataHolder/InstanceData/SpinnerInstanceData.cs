using System.Linq;
using Fusion;

public class SpinnerInstanceData : NetworkBehaviour
{
    public SpinnerType Type { get; private set; }

    void Awake()
    {
        GetComponents<IWriteSpinnerInstance>().ToList().ForEach(writeable => writeable.GiveWriter(new()));
    }
    
    public void SetType(SpinnerType type)
    {
        Type = type;
    }
}
