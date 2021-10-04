using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealHeart : MonoBehaviour
{
    public int value;
    public GameObject pickupFX;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EventManager<int>.Invoke(EventType.MAXHEALTH_CHANGED, 20);
            GameManager.Instance.playerHealth = (int)UImanager.Instance.playerHealthBar.maxValue;

            //verangen met dedicated audiomanager met events
            GameManager.Instance.audioSource.PlayOneShot(GameManager.Instance.heartPickupSound);
            Instantiate(pickupFX, collision.transform.position, collision.transform.rotation);
            Destroy(this.gameObject);


        }
    }
}
