using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestGun : MonoBehaviour
{
    public GameObject bulletObject;
    public GameObject player;
    public Vector3 mousePos;

    public Camera CastCam;

    public float bulletSpeed;
    public int bulletDamage;
    public float shootDelay;
    bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        CastCam = GameObject.Find("CastCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           StartCoroutine(TestShoot());
        }
    }

    public IEnumerator TestShoot()
    {
        if (canShoot)
        {
            Ray ray = CastCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                mousePos = new Vector3(hit.point.x, 0.0f, hit.point.z);
                Debug.DrawLine(transform.position, hit.point, Color.blue);

                Vector3 pos = mousePos;
                Vector3 dir = (player.transform.position - mousePos).normalized;
                Debug.DrawRay(pos, dir , Color.green, Mathf.Infinity);

                GameObject bullet = (GameObject)Instantiate(bulletObject);
                bullet.transform.position = gameObject.transform.position;
                bullet.GetComponent<Bullet>().bulletDamage = bulletDamage;
                // bullet.GetComponent<Rigidbody>().AddForce(new Vector3((pos.x + dir.x * 10) * bulletSpeed, transform.position.y, (pos.z + dir.z * 10) * bulletSpeed));
                bullet.GetComponent<Rigidbody>().velocity = ((new Vector3(dir.x * 10, 0.9f, dir.z * 10)) * -1) * bulletSpeed;
                canShoot = false;
                StartCoroutine(ShootDelay());

                yield return new WaitForSeconds(2);
                Destroy(bullet.gameObject);
            }
        }
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }
}
