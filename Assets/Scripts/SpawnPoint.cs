using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject player;
    public GameObject spawnEffect;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
       StartCoroutine(SpawnPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SpawnPlayer()
    {
        player.transform.position = transform.position;

        Instantiate(spawnEffect, player.transform.position,Quaternion.Euler( new Vector3(-90, 0, 0)));
        yield return new WaitForSeconds(0.6f);
        GameManager.Instance.audioSource.PlayOneShot(GameManager.Instance.exitPortalSound);
    }
}
