using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapHolder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(DungeonGeneration.DungeonGenerator.Instance.width / 2, 0 , DungeonGeneration.DungeonGenerator.Instance.height / 2);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
