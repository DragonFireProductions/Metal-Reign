//--------------------------------------------------------------------------------------------------------------------
//Purpose: This custom inspector is build to pass on the turretprojectilemanagers settings to the
//seperate turret ammo scripts to help clean up some of the mess I have made previously
//
//By: Michael Buck II
//
//
//
//
//--------------------------------------------------------------------------------------------------------------------
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TurretProjectileManagerV3))]
public class CustomTurretProjectileManagerInspector : Editor
{
    TurretProjectileManagerV3 projectileRef;

    void OnEnable()
    {
        projectileRef = (TurretProjectileManagerV3)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        projectileRef.useBullet = EditorGUILayout.Toggle("Use Bullet", projectileRef.useBullet);

        projectileRef.useExplosive = EditorGUILayout.Toggle("Use Explosive", projectileRef.useExplosive);

        projectileRef.useLaser = EditorGUILayout.Toggle("Use Laser", projectileRef.useLaser);

        if (projectileRef.useBullet)
        {
            projectileRef.useExplosive = false;

            projectileRef.useLaser = false;

            projectileRef.bulletImpactEffect = EditorGUILayout.ObjectField("Impact Effect", projectileRef.bulletImpactEffect, typeof(GameObject), true) as GameObject;

            projectileRef.damage = EditorGUILayout.FloatField("Damage", projectileRef.damage);

            projectileRef.projectileSpeed = EditorGUILayout.FloatField("Projectile Speed", projectileRef.projectileSpeed);
        }

        if (projectileRef.useExplosive)
        {
            projectileRef.useBullet = false;

            projectileRef.useLaser = false;

            projectileRef.explosionRadius = EditorGUILayout.FloatField("Explosion Radius", projectileRef.explosionRadius);

            projectileRef.explosionEffect = EditorGUILayout.ObjectField("Explosion Effect", projectileRef.explosionEffect, typeof(GameObject), true) as GameObject;

            projectileRef.missile = EditorGUILayout.ObjectField("Explosive Prefab", projectileRef.missile, typeof(GameObject), true) as GameObject;

            projectileRef.smokeTrail = EditorGUILayout.ObjectField("Smoke Trail", projectileRef.smokeTrail, typeof(ParticleSystem), true) as ParticleSystem;

            projectileRef.waitTime = EditorGUILayout.FloatField("Wait Time", projectileRef.waitTime);

            projectileRef.damage = EditorGUILayout.FloatField("Damage", projectileRef.damage);

            projectileRef.projectileSpeed = EditorGUILayout.FloatField("Projectile Speed", projectileRef.projectileSpeed);
        }

        if (projectileRef.useLaser)
        {
            projectileRef.useBullet = false;

            projectileRef.useExplosive = false;

            projectileRef.damageOverTime = EditorGUILayout.FloatField("Damage Per Second", projectileRef.damageOverTime);
        }

        projectileRef.bulletTarget = EditorGUILayout.ObjectField("Enemy", projectileRef.bulletTarget, typeof(Transform), true) as Transform;

        serializedObject.ApplyModifiedProperties();

        base.OnInspectorGUI();
    }
}

[CustomEditor(typeof(TurretBullet))]
public class TurretBulletEditor : CustomTurretProjectileManagerInspector
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}

[CustomEditor(typeof(TurretMissile))]
public class TurretMissileEditor : CustomTurretProjectileManagerInspector
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}

[CustomEditor(typeof(TurretLaser))]
public class TurretLaserEditor : CustomTurretProjectileManagerInspector
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}

