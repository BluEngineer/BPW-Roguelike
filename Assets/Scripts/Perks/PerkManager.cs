using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkManager : MonoBehaviour
{
    public List<Perk> perkList = new List<Perk>();

    public Perk[] shopPerks = new Perk[] { };

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < shopPerks.Length; i++)
        {
            shopPerks[i] = shopPerks[Random.Range(0, shopPerks.Length - 1)];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
