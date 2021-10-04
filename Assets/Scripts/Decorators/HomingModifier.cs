using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/HomingModifier", order = 1)]

public class HomingModifier : BulletModifier
{
    public float homingPower;
    public float homingRange = 30;

    private float shortestDistance = 200;
    private Transform target;

    // Angular speed in radians per sec.


    public override void OnBulletShot(Gun _gunParent)
    {
        Collider[] hitColliders = Physics.OverlapSphere(tempBullet.transform.position, homingRange);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.CompareTag("Enemy") || hitColliders[i].gameObject.CompareTag("EnemyBullet"))
            {
                if (Vector3.Distance(tempBullet.transform.position, hitColliders[i].transform.position) < shortestDistance)
                {
                    shortestDistance = Vector3.Distance(tempBullet.transform.position, hitColliders[i].transform.position);
                    target = hitColliders[i].transform;
                   
                }

            }

            //target = hitColliders[Random.Range(0,hitColliders.Length)].transform;
        }

        if (target != null)
        {
            Debug.Log(target.name + " is the homing target");

        }

        base.OnBulletShot(_gunParent);
    }

    public override void BulletUpdate()
    {
        if (target != null)
        {
            Vector3 targetDirection = target.position - tempBullet.transform.position;
            float singleStep = homingPower * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(tempBullet.transform.forward, targetDirection, singleStep, 0.0f);
            //Debug.DrawRay(tempBullet.transform.position, newDirection, Color.red);
            // Calculate a rotation a step closer to the target and applies rotation to this object
            tempBullet.transform.rotation = Quaternion.LookRotation(newDirection);
        }

        base.BulletUpdate();
    }

}
