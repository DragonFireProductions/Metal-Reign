using UnityEngine;

[System.Serializable]
public class MechPartDatabase
{
    public string name;
    //This is the parts numeric identifier
    public int id;
    //This is the 3d model the object is associated with
    public GameObject prefab;
    //This is referncing the Enum script 
    public MechPartType partType;
    //This will be used for reference to set the players health later
    public int health;
    //This will be used for reference to set the players armor later
    public int armor;

    public MechPartDatabase()
    {

    }

    public MechPartDatabase(MechPartDatabase part) : this(part.id, part.name, part.prefab, part.partType, part.health, part.armor)
    {

    }

    //Set the parts components
    public MechPartDatabase(int id, string name, GameObject prefab, MechPartType partType, int health, int armor)
    {
        this.id = id;
        this.name = name;
        this.prefab = prefab;
        this.partType = partType;
        this.health = health;
        this.armor = armor;
    }
}
