using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value;
    public GameObject pickupFX;
    public AudioSource audioSource;
    public AudioClip[] goldSound;

    private void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.money += value;
            EventManager<int>.Invoke(EventType.MONEY_CHANGED, GameManager.Instance.money);
            Instantiate(pickupFX, transform.position, Quaternion.identity);
            GetComponent<MeshRenderer>().enabled = false;
            StartCoroutine(PlaySoundAndDestroy());
            //Destroy(this.gameObject);


        }
    }

    private IEnumerator PlaySoundAndDestroy()
    {

        audioSource.PlayOneShot(goldSound[Random.Range(0,goldSound.Length)]);

        while (audioSource.isPlaying)
        {
            yield return null;
        }

        Destroy(gameObject);
    }

}
