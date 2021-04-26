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
    public Transform pivotPos;
    public Canvas pause;

    [HideInInspector] public bool hasFlare;
    [Range(1,100)] public float runSpeed = 40f;
    [Range(1, 5)] public float throwForce = 2f;
    [Range(1, 50)] public float flareDespawnTime = 10f;

    public ItemObject flare;

    float moveX = 0f;
    bool jump = false;
    float pi = 3.1415f;

    void Start(){
        hasFlare = true;
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
        if(pickPos.position.x > headPos.position.x && !controller.m_FacingRight){
            controller.Flip();
        }
        if(pickPos.position.x < headPos.position.x && controller.m_FacingRight){
            controller.Flip();
        }
        if((headAngle > -pi/6 && headAngle < pi/4) && headDirection.x > 0){
            headPos.right = headDirection;
        }
        if((headAngle > -pi/6 && headAngle < pi/4) && headDirection.x < 0){
            headPos.right = -headDirection;
        }

        if(Input.GetButtonDown("Jump")){
            jump = true;
        }     

        //flare on right mouse button
        if(Input.GetMouseButtonDown(1) && gameObject.GetComponent<InventoryComponent>().RemoveItem(flare)){
            FindObjectOfType<AudioManager>().Play("Flare");
            GameObject flare = Instantiate(flarePrefab, firePoint.position, firePoint.rotation*Quaternion.Euler(0, 0, -90));
            Rigidbody2D rb = flare.GetComponent<Rigidbody2D>();
            Vector3 direction = pickPos.position - transform.position;
            rb.AddForce(direction*throwForce, ForceMode2D.Impulse);
            rb.AddTorque(-direction.normalized.x*5);
            StartCoroutine(DespawnFlare(flareDespawnTime, flare));
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
			if (pause.enabled)
			{
                Time.timeScale = 1;
			}
			else
			{
                Time.timeScale = 0;
			}
            pause.enabled = !pause.enabled;
		}

    }

    IEnumerator DespawnFlare(float flareDespawnTime, GameObject flare){
        yield return new WaitForSeconds(flareDespawnTime);
        Destroy(flare);
    }
}
