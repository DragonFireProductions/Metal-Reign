using UnityEngine;

public class MechCommon : MonoBehaviour
{
    protected MechPartStats partStats;

    public MechPartStats Stats { get { return partStats; } }

    protected virtual void Awake()
    {
        partStats = GetComponent<MechPartStats>();
    }
}
