using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    BuildManager buildManager;

    //The color the node will turn when the mouse enters it
    [SerializeField]
    Color hoverColor;

    [SerializeField]
    Color notEnoughMoneyColor;

    //private color because this will never need to be changed
    Color startColor;

    //This will track the current turret on the node
    [Header ("Optional")]
    public GameObject turret;

    public Vector3 positionOffSet;

    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }

    //This is just a quick helper function for quick access
    //With the build manager
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffSet;
    }

    void OnMouseEnter()
    {
        //If the mouse is hovering  over an icon, don't allow it to click through the icon
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        //If this function is equal to noting then return
        if (!buildManager.CanBuild)
        {
            return;
        }

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    void OnMouseDown()
    {
        //If the mouse is hovering  over an icon, don't allow it to click through the icon
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        //If this function is equal to noting then return
        if (!buildManager.CanBuild)
        {
            return;
        }

        if (turret != null)
        {
            Debug.Log("Cant build here -  TODO: Display on screen");
            return;
        }

        buildManager.BuildTurret(this);
    }
}
