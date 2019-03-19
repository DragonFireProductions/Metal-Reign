using UnityEngine;

public class BuildManager : MonoBehaviour
{
    //This stores the BuildManager within itself
    //To make for easy referenceing
    //NOTETOSELF: Look up Singleton Patterns if your confused
    public static BuildManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("more than one buildmanager in scene");
            return;
        }
        //This references The BuildManager itself
        instance = this;
    }

    public GameObject turretPrefabOne;

    public GameObject turretPrefabTwo;

    public GameObject turretPrefabThree;

    public GameObject buildEffect;

    //This will capture the specific turret that is needed to
    //Be built
    TurretBlueprint turretToBuild;

    //This is called a property if you need to reference this material at a later date
    //This is only allowed to get something, nothing else
    public bool CanBuild { get { return turretToBuild != null; } }

    //This checks to see if the player has exactly enough or more than the cost of the turret selected to be built
    public bool HasMoney { get { return PlayerStats.money >= turretToBuild.cost; } }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }

    public void BuildTurret(Node node)
    {
        //If the player can't afford the turret return
        if (PlayerStats.money < turretToBuild.cost)
        {
            return;
        }

        //If the player can afford the turret, 
        //Subtract the cost of the turret from players money
        PlayerStats.money -= turretToBuild.cost;

        GameObject turret = Instantiate(turretToBuild.turretPrefab, node.GetBuildPosition(), Quaternion.identity);

        node.turret = turret;

        GameObject effect = Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);

        Destroy(effect, 1.5f);
    }
}
