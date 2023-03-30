using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public abstract class Bullet : MonoBehaviour, IBullet
{
    //public int bulletDamage;
    public bool isActive = true;
    public bool destroyOnCollision;
    public bool isPlayerBullet;
    public float bulletLifetime;
    public float explosionRadius;
    public Gun gunParent;

    private float bulletSpeed;

    public Rigidbody rigidBody;



    public float lifeTimer;


    public int bulletDamage { get; set; }

    public List<BulletModifier> bulletModifiers;

    // public WeaponItem weapon;

//    public Bullet(int _damage)
//    {
//        bulletDamage = _damage;
//    }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        for (int i = 0; i < bulletModifiers.Count; i++)
        {
            bulletModifiers[i] = Instantiate(bulletModifiers[i]);
            bulletModifiers[i].tempBullet = this;

        }

    }

    void Update()
    {
        foreach (BulletModifier b in bulletModifiers)
        {
            b.BulletUpdate();
        }

        transform.position += transform.forward * bulletSpeed;

        DestroyOnLifetimeExpire();

    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        foreach (BulletModifier b in bulletModifiers)
        {
            b.OnBulletHit(collision);
        }

        //enemy collision
        if (collision.gameObject.GetComponent<IDamageable>() != null)
        {
            Debug.Log("bullet collide with damageable");
            IDamageable hitobj = collision.gameObject.GetComponent<IDamageable>();
            hitobj.TakeDamage(CalculateDamage());
        
            hitobj.LastDamageTaken = CalculateDamage();
           // Debug.Log("bulletdamage is: " + CalculateDamage());
        }

        if (destroyOnCollision)
        {
            Destroy(this.gameObject);
        }


    }

   

    public virtual void DisableBullet()
    {
        isActive = false;
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        //gameObject.GetComponent<MeshRenderer>().enabled = false;
        StartCoroutine(DeleteTimer());
    }

    public virtual IEnumerator DeleteTimer()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);

    }

    public virtual void DestroyOnLifetimeExpire()
    {
        lifeTimer -= Time.deltaTime;


        if (lifeTimer <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    

    public void BulletShoot(Gun _gunParent, Quaternion _bulletRotation, float _shootPower)
    {
        bulletSpeed = _shootPower;
        gunParent = _gunParent;
        bulletDamage = _gunParent.damage;
        bulletLifetime = _gunParent.bulletLifetime;
        explosionRadius = _gunParent.bulletExplosionRadius;
        transform.rotation = _bulletRotation;
       // GetComponent<Rigidbody>().AddForce(transform.forward * _shootPower, ForceMode.Impulse);
        lifeTimer = bulletLifetime;
        DestroyOnLifetimeExpire();
        // Debug.Log("Bulletshoot method called on " + this.name + " shot by " + gunParent.name);
        foreach (BulletModifier b in bulletModifiers)
        {
            b.OnBulletShot(gunParent);
        }


    }

    public int CalculateDamage()
    {
        int tempDamage = bulletDamage;
        foreach(BulletModifier b in bulletModifiers)
        {
            tempDamage += b.CalculateDamage();
        }

        return tempDamage;
    }
}
