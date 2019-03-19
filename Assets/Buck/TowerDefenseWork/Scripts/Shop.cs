using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    public TurretBlueprint machineGunTurret;
    public TurretBlueprint missileTurret;
    public TurretBlueprint laserTurret;

    public Text mgCost;
    public Text missileCost;
    public Text laserCost;

    void Start()
    {
        buildManager = BuildManager.instance;
        mgCost.text = "$" + Mathf.Round(machineGunTurret.cost).ToString();
        missileCost.text = "$" + Mathf.Round(missileTurret.cost).ToString();
        laserCost.text = "$" + Mathf.Round(laserTurret.cost).ToString();
    }



    public void SelectMachineGunTurret()
    {
        buildManager.SelectTurretToBuild(machineGunTurret);
    }

    public void SelectMissileTurret()
    {
        buildManager.SelectTurretToBuild(missileTurret);
    }

    public void SelectLaserTurret()
    {
        buildManager.SelectTurretToBuild(laserTurret);
    }
}
