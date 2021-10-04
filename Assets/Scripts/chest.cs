using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour, IInteractable
{
    public GameObject[] gunPool;
    public AudioSource audioSource;
    public AudioClip openChestAudio;
    public GameObject openChestFx;

    public void Action(GameObject sender)
    {
       GameObject Gun = Instantiate(gunPool[Random.Range(0, gunPool.Length)], transform.position, Quaternion.identity);
        Instantiate(openChestFx, transform.position, Quaternion.identity);
        audioSource.PlayOneShot(openChestAudio);
        Gun.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-2, 2), Random.Range(0, 3), Random.Range(-2, 2)) , ForceMode.Impulse);

        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
