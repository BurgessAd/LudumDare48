using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    
    public CircleCollider2D pickaxeCheck;
    public Transform firePoint;
    public GameObject flarePrefab;
    public Transform pickPos;
    public Transform headPos;

    public ItemObject flare;
    public float runSpeed = 40f;
    public float throwForce = 10f;
    public float flareDespawnTime = 10f;
    float moveX = 0f;
    bool jump = false;
    float pi = 3.14f;

    void start(){

    }

    void Update(){

        ProcessInputs();
    }

    void FixedUpdate(){
        controller.Move(moveX*Time.fixedDeltaTime, false, jump);
        jump = false;
    }
    //Finding the move direction and handling player inputs
    void ProcessInputs(){

        moveX = Input.GetAxisRaw("Horizontal")*runSpeed;
        Vector2 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 headDirection = new Vector2(mousePos.x - headPos.position.x, mousePos.y - headPos.position.y);
        float headAngle = Mathf.Atan(headDirection.y/headDirection.x);
        //adding a buffer to stop spin bot jittering
        if(headDirection.x >= 1 && !controller.m_FacingRight){
            controller.Flip();
        }
        if(headDirection.x < -1 && controller.m_FacingRight){
            controller.Flip();
        }
        if((headAngle > -pi/4 && headAngle < pi/4) && headDirection.x >= 0){
            headPos.right = headDirection;
        }
        if((headAngle > -pi/4 && headAngle < pi/4) && headDirection.x < 0){
            headPos.right = -headDirection;
        }

        if(Input.GetButtonDown("Jump")){
            jump = true;
        }     

        //flare on right mouse button
        if(Input.GetMouseButtonDown(1) && gameObject.GetComponent<InventoryComponent>().RemoveItem(flare)){
            GameObject flare = Instantiate(flarePrefab, firePoint.position, firePoint.rotation*Quaternion.Euler(0, 0, -90));
            Rigidbody2D rb = flare.GetComponent<Rigidbody2D>();
            Vector3 direction = pickPos.position - transform.position;
            rb.AddForce(direction*throwForce, ForceMode2D.Impulse);
            rb.AddTorque(-direction.normalized.x*5);
            StartCoroutine(DespawnFlare(flareDespawnTime, flare));
        }
    }

    IEnumerator DespawnFlare(float flareDespawnTime, GameObject flare){
        yield return new WaitForSeconds(flareDespawnTime);
        Destroy(flare);
    }
}
