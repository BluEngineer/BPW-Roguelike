using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/splitModifier", order = 1)]
public class SplitModifier : BulletModifier
{
    public float splitMoment = 0.5f;
    private float lifetimeTimer = 0;
    public float spreadAngle;
    public int bulletAmount;
    public int splitCount;

    public override void BulletUpdate()
    {
        lifetimeTimer += Time.deltaTime;
        if (lifetimeTimer >= splitMoment * tempBullet.bulletLifetime)
        {
            SplitBullet();
        }

        base.BulletUpdate();

    }

    public override void OnBulletShot(Gun _gunParent)
    {
        lifetimeTimer = 0;
        base.OnBulletShot(_gunParent);
    }

    public void SplitBullet()
    {     
        if (splitCount < 1) return;

        Vector3 tempVelocity = tempBullet.GetComponent<Rigidbody>().velocity;
        splitCount--;
        for (int i = 0; i < bulletAmount; i++)
        {
            Bullet newBullet = Instantiate(tempBullet, tempBullet.transform.position, Quaternion.Euler(new Vector3(0, tempBullet.transform.rotation.eulerAngles.y - (spreadAngle * i - bulletAmount * 0.5f), 0)));
            newBullet.BulletShoot(tempBullet.gunParent, newBullet.transform.rotation, tempBullet.gunParent.shootPower);
        }

       // Debug.Log("Done a nice bulletsplit");
        Destroy(tempBullet.gameObject);
    }


}
