using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BounceModifier", order = 1)]
public class BounceModifier : BulletModifier
{
    [SerializeField]
    private int bounceAmount = 1;
    private Vector3 lastDir;
    public float extraLifeTime;

    private void Awake()
    {

    }



    //not sure if this is very efficient
    public override void BulletUpdate()
    {
        lastDir = tempBullet.rigidBody.velocity;

        base.BulletUpdate();
    }

    public override void OnBulletHit(Collision col)
    {
        if (bounceAmount >= 1)
        {
            tempBullet.destroyOnCollision = false;
            bounceAmount--;
            lastDir = tempBullet.transform.forward;
            tempBullet.lifeTimer += extraLifeTime;
            //do something to reflect bullet (vector3.reflect)
            ContactPoint cp = col.contacts[0];
            Vector3 reflectedDir = Vector3.Reflect(lastDir, cp.normal);
            tempBullet.transform.rotation = Quaternion.LookRotation(reflectedDir);
        } else
        {
            tempBullet.destroyOnCollision = false;

            Destroy(tempBullet.gameObject);
        }

        base.OnBulletHit(col);
    }

}
