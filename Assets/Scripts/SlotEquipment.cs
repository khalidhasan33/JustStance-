using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotEquipment : MonoBehaviour {


    private Equipment equipment;
    private GameObject sword;
    private PlayerController player;
    public int index;

    private void Start()
    {

        equipment = GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (transform.childCount <= 0) {
            equipment.items[index] = 0;
        }
    }

    public void Cross() {

        sword = GameObject.Find("Weapon(onHand)");
        GameObject.Destroy(sword);
        foreach (Transform child in transform) {
            child.GetComponent<Spawn>().SpawnItem();
            GameObject.Destroy(child.gameObject);
        }
    }

}
