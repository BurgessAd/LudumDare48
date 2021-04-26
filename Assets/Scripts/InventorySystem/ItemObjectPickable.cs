﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// in-game representation of a dropped piece of carbon/uranium (necessitates adding in pressing space for picking up the item)
[RequireComponent(typeof(SpriteRenderer))]
public class ItemObjectPickable : MonoBehaviour
{
    private ItemObject objectType;
    private int objectNum;
    private SpriteRenderer spriteRenderer;
    //private SoundComponent soundComponent;
    private AudioSource audioSource;
    private BoxCollider2D collider2D;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<BoxCollider2D>();
    }

    public void SetItemObject(ItemObject objectType, in int number)
    {
        this.objectType = objectType;
        spriteRenderer.sprite = objectType.objectImage;
        spriteRenderer.drawMode = SpriteDrawMode.Simple;
        objectNum = number;
    }
    IEnumerator IIII()
    {
        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.name == "Player")
        {
            InventoryComponent inv = other.gameObject.GetComponent<InventoryComponent>();
            inv.AddItem(objectType, objectNum);
            FindObjectOfType<AudioManager>().Play("Pickup");
            spriteRenderer.enabled = false;
            collider2D.enabled = false;
            StartCoroutine(IIII());
        }


    }
}
