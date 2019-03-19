using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechPartStats : MechCommon
{
    public int health = 0;

    public int armor = 0;

    //Make a check for custom editor
    //If chassis show this variable
    //If not hide it
    public float moveSpeed = 0;

    //Make a check for custom editor
    //If weapon show this variable
    //If not hide it
    public int damage = 0;
}
