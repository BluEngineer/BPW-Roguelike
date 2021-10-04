using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaRifle : Gun
{
    public GameObject impactEffect;
    public GameObject muzzleFlash;
    public Transform gunBarrel;

    public ShakeableTransform screenShake;
    public float screenshakeAmount = 0.8f;

    private bool charging;
    public bool firstShot;

    public AudioClip chargeAudio;

    public override void Shoot()
    {
        switch (gunState)
        {
            case ItemState.Equiped:
                nextTimeToFire = Time.time + fireRate;

                if (ammo <= 0)
                {
                    audioSource.pitch = 1;
                    audioSource.PlayOneShot(clipEmptySound, 1f);
                    return;
                }
                if (isReloading) return;

                if (charging) return;


                StartCoroutine(ShakeableTransform.Instance.ShakeScreen(screenshakeAmount));


                //if (hit.collider.tag != "player") GameObject.Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                GameObject flash = Instantiate(muzzleFlash, gunBarrel.position, gunBarrel.rotation);
                GameObject bullet = Instantiate(bulletType, gunBarrel.position, gunBarrel.rotation);
                bullet.transform.localScale = bulletScale;            
     
                audioSource.pitch = Random.Range(0.85f, 1.15f);
                audioSource.PlayOneShot(shotSound, 0.4f);
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                if (bullet.GetComponent<Bullet>() != null)
                {
                    bulletScript.BulletShoot(this, gunBarrel.rotation, shootPower);

                }




                ammo -= 1;
                break;
        }

        EventManager<int>.Invoke(EventType.AMMO_CHANGED, ammo);

    }

    public void Update()
    {

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            Shoot();
        }

        if(Input.GetKey(KeyCode.R))
        {
            Reload();
        }
    }


    public override void Reload()
    {
        if (!isReloading && GunManager.Instance.currentGun == this) StartCoroutine(ReloadWeapon());
    }

    IEnumerator ChargeGun()
    {
         charging = true;
        audioSource.PlayOneShot(chargeAudio);
        yield return new WaitForSeconds(0.9f);

         charging = false;

    }

    IEnumerator ReloadWeapon()
    {
        audioSource.PlayOneShot(reloadSound);
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        ammo += magSize - ammo;
        EventManager<int>.Invoke(EventType.AMMO_CHANGED, ammo);
        firstShot = true;
        isReloading = false;
    }


}
