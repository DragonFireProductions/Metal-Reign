using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TurretV3))]
public class CutsomTurretInspector : Editor
{
    TurretV3 turretRef;

    void OnEnable()
    {
        turretRef = (TurretV3)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        //References the turret useLaser bool and creates a box toggle button
        //Named Use Laser
        turretRef.useLaser = EditorGUILayout.Toggle("Use Laser", turretRef.useLaser);

        turretRef.useBullet = EditorGUILayout.Toggle("Use Bullet", turretRef.useBullet);

        turretRef.useExplosive = EditorGUILayout.Toggle("Use Explosive", turretRef.useExplosive);

        if (turretRef.useLaser)
        {

            turretRef.useBullet = false;

            turretRef.useExplosive = false;

            turretRef.laser = EditorGUILayout.ObjectField("Laser", turretRef.laser, typeof(LineRenderer), true) as LineRenderer;

            turretRef.laserImpactEffect = EditorGUILayout.ObjectField("Laser Impact Effect", turretRef.laserImpactEffect, typeof(ParticleSystem), true) as ParticleSystem;

            //This would work if HideInInspector wasnt used to hide the array in the turret script
            //CustomEditorList.Show(serializedObject.FindProperty("laserImpactLights"), true);

            turretRef.damageOverTime = EditorGUILayout.FloatField("Damage Over Time", turretRef.damageOverTime);

            //Need to figure out how to create an array in the editor for multiple lights and other arrays down the road
        }

        if (turretRef.useBullet)
        {
            turretRef.useLaser = false;

            turretRef.useExplosive = false;

            turretRef.bulletFire = EditorGUILayout.ObjectField("Fire Sound", turretRef.bulletFire, typeof(AudioClip), true) as AudioClip;

            turretRef.bulletPrefab = EditorGUILayout.ObjectField("Projectile", turretRef.bulletPrefab, typeof(GameObject), true) as GameObject;

            turretRef.muzzleBlast = EditorGUILayout.ObjectField("Muzzle Blast", turretRef.muzzleBlast, typeof(GameObject), true) as GameObject;

            turretRef.fireRate = EditorGUILayout.FloatField("Fire Rate", turretRef.fireRate);
        }

        if (turretRef.useExplosive)
        {
            turretRef.useLaser = false;

            turretRef.useBullet = false;

            turretRef.explosiveFire = EditorGUILayout.ObjectField("Fire Sound", turretRef.explosiveFire, typeof(AudioClip), true) as AudioClip;

            turretRef.explosivePrefab = EditorGUILayout.ObjectField("Explosive", turretRef.explosivePrefab, typeof(GameObject), true) as GameObject;

            turretRef.muzzleBlast = EditorGUILayout.ObjectField("Muzzle Blast", turretRef.muzzleBlast, typeof(GameObject), true) as GameObject;

            turretRef.fireRate = EditorGUILayout.FloatField("Fire Rate", turretRef.fireRate);
        }

        turretRef.turretFirePoint = EditorGUILayout.ObjectField("Turret Fire Point", turretRef.turretFirePoint, typeof(Transform), true) as Transform;

        turretRef.partToRotate = EditorGUILayout.ObjectField("Part to Rotate", turretRef.partToRotate, typeof(Transform), true) as Transform;

        turretRef.turretTarget = EditorGUILayout.ObjectField("Turret Target", turretRef.turretTarget, typeof(Transform), true) as Transform;

        turretRef.rotateSpeed = EditorGUILayout.FloatField("Rotate Speed", turretRef.rotateSpeed);

        turretRef.range = EditorGUILayout.FloatField("Range", turretRef.range);

        turretRef.turretRangeObj = EditorGUILayout.ObjectField("Turret Range Obj", turretRef.turretRangeObj, typeof(GameObject), true) as GameObject;

        serializedObject.ApplyModifiedProperties();

        base.OnInspectorGUI();
    }
}
