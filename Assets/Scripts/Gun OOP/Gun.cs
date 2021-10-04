using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemState { Dropped, Equiped }

public abstract class Gun : MonoBehaviour, IInteractable
{
    public string weaponName;
    public int damage;
    public float range;
    public int ammo;
    public int magSize;
    public float fireRate;
    public float reloadTime;
    public GameObject bulletType;
    public float bulletLifetime;
    public float bulletExplosionRadius;
    public Vector3 bulletScale;


    protected float nextTimeToFire = 0;
    public bool isReloading;
    public float shootPower;

    public PlayerController player;

    public AudioSource audioSource;
    public AudioClip shotSound;
    public AudioClip reloadSound;
    public AudioClip clipEmptySound;
    public AudioClip pickupSound;

    public Vector3 offset;


    public ItemState gunState = ItemState.Dropped;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
 
    }

    public abstract void Shoot();
    public abstract void Reload();

    public void Action(GameObject sender)
    {
        if (gunState == ItemState.Dropped)
        {
            EquipWeapon(sender);
        }

    }

    protected virtual void EquipWeapon(GameObject sender)
    {
        if (GunManager.Instance.currentGun == null)
        {
            player = sender.GetComponent<PlayerController>();
            transform.parent = GunManager.Instance.transform;
            transform.localPosition = offset;
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            gunState = ItemState.Equiped;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Collider>().enabled = false;
            GunManager.Instance.hasGun = true;
            GunManager.Instance.currentGun = this;
            audioSource.PlayOneShot(pickupSound);
            EventManager<int>.Invoke(EventType.AMMO_CHANGED, ammo);

        }

    }

    public virtual void DropWeapon()
    {
        transform.parent = null;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Collider>().enabled = true;
        gunState = ItemState.Dropped;
        GunManager.Instance.currentGun = null;
        EventManager<int>.Invoke(EventType.AMMO_CHANGED, 0);

        GunManager.Instance.hasGun = false;

    }



}
