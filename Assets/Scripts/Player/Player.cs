using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;

    void Awake(){
        Cursor.visible = false;
    }
    
    void Start(){
        health = maxHealth;
    }

    public void TakeDamage(int damage, GameObject player){
        health -= damage;
        if(health <= 0){
            Die(player);
        }
    }

    void Die(GameObject player){
    }
}