using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Bullet
{

    public GameObject explosionFX;
    Collider[] enemiesInRange;

    public LayerMask enemyLayer;
    public AudioSource audioSource;

    private bool explosionStarted;

    public int extraRocketDamage = 10;

    // public WeaponItem weapon;

    private void Start()
    {

    }

    public override void OnCollisionEnter(Collision collision)
    {
        foreach (BulletModifier b in bulletModifiers)
        {
            b.OnBulletHit(collision);
        }

        if (destroyOnCollision)
        {

            StartCoroutine(DeleteTimer());
        }
    }
    
    public override void DestroyOnLifetimeExpire()
    {
        if (lifeTimer > bulletLifetime)
        {
           // explosionStarted = false;

            StartCoroutine(DeleteTimer());

            
        }
    }

    public override IEnumerator DeleteTimer()
    {

        if (!explosionStarted)
        {
            explosionStarted = true;
            GameObject explosion = Instantiate(explosionFX, transform.position, Quaternion.identity);
            explosion.transform.localScale = new Vector3(explosionRadius, explosionRadius, explosionRadius);
            enemiesInRange = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (var obj in enemiesInRange)
            {
                if (obj.gameObject.GetComponent<IDamageable>() != null)
                {
                    Debug.Log("Rocket collide with damageable");
                    IDamageable hitobj = obj.gameObject.GetComponent<IDamageable>();
                    hitobj.TakeDamage(bulletDamage + extraRocketDamage);

                }
            }



        }

        yield return new WaitForSeconds(0.1f);
       // Destroy(this.gameObject);
    }

}
