using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mining : MonoBehaviour
{

    [SerializeField] private Transform playerPos;
    [SerializeField] private SpriteRenderer crosshair;
    [Range(1,20)] [SerializeField] private float pickRange = 10f;
    [SerializeField] private Transform pickPos;
    [SerializeField] private TrailRenderer swingTrail;
    [SerializeField] private Transform swingEnd;
    private bool inRange = true;
    
    public float mineCooldown = 1f;
    public float mineStrength;
    public float attackCooldown = 1f;
    public float attackStrength;
    private bool Attackable = false;
    private bool Minable = false;
    private bool inAttackCooldown = false;
    private bool inMineCooldown = false;
    private GameObject target;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Attack: " + Attackable + ", Mine: " + Minable);
        //Debug.Log(inMineCooldown);
        //on left mouse button
        if(Input.GetMouseButtonDown(0)){
            //Debug.Log("Mouse click");
            //if no target just swing
            if(!Attackable && !Minable){
                //just have a swing animation
            }
            if(Attackable && !inAttackCooldown){
                //Debug.Log("should attack");
                Attack();
            }
            if(Minable && !inMineCooldown){
                Mine();
            }
        }
        PickPos();
    }

    void FixedUpdate(){
        
    }

    void PickPos(){
        
        Vector2 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 playerPos2D = new Vector2(playerPos.position.x, playerPos.position.y);
        pickPos.position = mousePos;
        //if within pick range
        if((mousePos - playerPos2D).magnitude <= pickRange){
            crosshair.color = Color.green;
            inRange = true;
        }
        else{
            crosshair.color = Color.red;
            inRange = false;
        }
        
    }

    void Attack(){
        //Debug.Log("I'm hittin summit");
        //Destroy(target);
        StartCoroutine(AttackCooldown(attackCooldown));
        HealthComponent health = target.GetComponent<HealthComponent>();
        if (health)
        {
            health.ProcessHit(5.0f);
        }

    }

    IEnumerator AttackCooldown(float attackCooldown){
        inAttackCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        inAttackCooldown = false;
    }

    void Mine(){
        Debug.Log("I'm minin summit");
        StartCoroutine(MineCooldown(mineCooldown));
    }

    IEnumerator MineCooldown(float mineCooldown){
        Debug.Log("Cooldown Starts");
        inMineCooldown = true;
        yield return new WaitForSeconds(mineCooldown);
        inMineCooldown = false;
        Debug.Log("Cooldown Finishes");
    }

    void OnTriggerStay2D(Collider2D col){
        crosshair.color = Color.blue;
        if(!inRange){
            Minable = false;
            Attackable = false;
        }
        if(col.name == "Floor" && inRange && !inMineCooldown){
            Minable = true;
            Attackable = false;
        }
        if(col.name == "Enemy" && inRange && !inAttackCooldown){
            Minable = false;
            Attackable = true;
        }
        target = col.gameObject;
        //Debug.Log(col.name);
    }

    void OnTriggerExit2D(Collider2D col){
        Minable = false;
        Attackable = false;
        target = null;
    }
}
