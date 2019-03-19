using System.Collections.Generic;
using UnityEngine;

public class MechManager : MonoBehaviour
{
    public List<MechPartDatabase> mechParts = new List<MechPartDatabase>();

    public static MechManager instance { get; private set; }

    void Awake()
    {
        instance = this;
    }

    public MechPartDatabase GetPartID(int id)
    {
        foreach (MechPartDatabase part in mechParts)
        {
            if (part.id == id)
            {
                return new MechPartDatabase(part);
            }
        }

        return null;
    }

    public MechPartDatabase GetPartName(string name)
    {
        foreach (MechPartDatabase part in mechParts)
        {
            if (part.name.ToLower().Equals(name.ToLower()))
            {
                return new MechPartDatabase(part);
            }
        }

        return null;
    }

    public GameObject SpawnPart(GameObject prefab)
    {
        return Instantiate(prefab, Vector3.zero, Quaternion.identity);
    }
}
