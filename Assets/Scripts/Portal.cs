using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip enterPortal;
    public AudioClip exitPortal;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            EventManager.Invoke(EventType.ENTERED_PORTAL);
            GameManager.Instance.audioSource.PlayOneShot(enterPortal);
            GameManager.Instance.LoadScene(2);
        }
    }
}
