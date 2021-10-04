using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDungeonGeneration : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DungeonGeneration.DungeonGenerator.Instance.GenerateDungeon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
