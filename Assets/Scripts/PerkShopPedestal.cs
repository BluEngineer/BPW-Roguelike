using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkShopPedestal : MonoBehaviour, IInteractable
{
    public Perk currentPerk;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Action(GameObject sender)
    {
        BuyPerk();
    }

    public void BuyPerk()
    {
        GameManager.Instance.money -= currentPerk.price;
        Destroy(this.gameObject);
    }
}
