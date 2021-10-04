using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Gun
{
    public GameObject muzzleFlash;
    public Transform gunBarrel;


    public int spread;
    private float spreadInterval;

    public float knockback;

    public ShakeableTransform screenShake;
    public float screenshakeAmount = 2f;

    public int burstCount;

    //private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>() ;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    public override void Shoot()
    {
        switch (gunState)
        {
            case ItemState.Equiped:
                if (ammo <= 0)
                {
                    audioSource.pitch = 1;
                    audioSource.PlayOneShot(clipEmptySound, 1f);
                    return;
                }

                if (isReloading) return;

                nextTimeToFire = Time.time + fireRate;

                //if (hit.collider.tag != "player") GameObject.Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                GameObject flash = Instantiate(muzzleFlash, gunBarrel.position, gunBarrel.rotation);
                StartCoroutine(ShakeableTransform.Instance.ShakeScreen(screenshakeAmount));

                audioSource.pitch = Random.Range(0.85f, 1.15f);
                audioSource.PlayOneShot(shotSound, 0.4f);

                spreadInterval = (spread * 2) / burstCount;
                player.GetComponent<Rigidbody>().AddForce(-gunBarrel.transform.forward * knockback, ForceMode.Impulse);

                for (int i = 0; i < burstCount; i++)
                {

                    GameObject bullet = Instantiate(bulletType, gunBarrel.position, Quaternion.Euler(new Vector3(0, gunBarrel.transform.rotation.eulerAngles.y -spread + (spreadInterval * i) ,0)));
                    bullet.transform.localScale = bulletScale;
                    Debug.Log(90 - spread + (spreadInterval * i));
                    Bullet bulletScript = bullet.GetComponent<Bullet>();
                    if (bullet.GetComponent<Bullet>() != null)
                    {
                        bulletScript.BulletShoot(this, bullet.transform.rotation, shootPower);
                    }


                    bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * shootPower, ForceMode.Impulse);

                }


                ammo -= 1;
                break;
        }
        EventManager<int>.Invoke(EventType.AMMO_CHANGED, ammo);

    }



    public override void Reload()
    {
        if (!isReloading && GunManager.Instance.currentGun == this) StartCoroutine(ReloadWeapon());
    }

    IEnumerator ReloadWeapon()
    {
        audioSource.PlayOneShot(reloadSound);
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        ammo += magSize - ammo;
        EventManager<int>.Invoke(EventType.AMMO_CHANGED, ammo);

        isReloading = false;
    }
}
