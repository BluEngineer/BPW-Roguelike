using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyMountainSpawnpoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.playerObject.transform.position = gameObject.transform.position;  
    }


}
