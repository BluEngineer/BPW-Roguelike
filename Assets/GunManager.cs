using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    private static GunManager _instance;
    public static GunManager Instance { get { return _instance; } }

    public bool hasGun;
    public Gun currentGun;

    public GameObject starterGunPrefab;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }



    }
    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q) && currentGun != null)
        {
            currentGun.DropWeapon();
        }
    }
}
