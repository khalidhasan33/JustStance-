using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour {

    private Transform playerPos;
    private Equipment equipment;
    public GameObject sword;
    private GameObject itemButton;

    private void Start()
    {
        itemButton = gameObject;
        equipment = GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public void Use() {
        for (int i = 0; i < equipment.items.Length; i++)
        {
            if (equipment.items[i] == 0) 
            {
                GameObject weaponClone = Instantiate(sword, playerPos.position, sword.transform.rotation, playerPos.transform);
                weaponClone.name = "Weapon(onHand)";
                equipment.items[i] = 1; // makes sure that the slot is now considered FULL
                Instantiate(itemButton, equipment.slots[i].transform, false);
                Destroy(gameObject);
                break;
            }
        }
    }
}
