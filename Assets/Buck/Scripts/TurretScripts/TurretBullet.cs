//--------------------------------------------------------------------------------------
//Purpose: This inherits from the projectile manager so that way all of
//The different turret ammo types and functions may be kept
//Seperate from one another 
//--------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : TurretProjectileManagerV3
{
	void Update()
	{
		FireBullet();
	}

}
