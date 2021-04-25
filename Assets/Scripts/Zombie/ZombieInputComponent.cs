using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieInputComponent : InputComponent
{

    public MovementComponent movementComponent;
    //public AimComponent lookdir;
    // Start is called before the first frame update
    private float timer;
    private float wait;
    private Vector2 dir;
    public float visionDist = 10;
    public Animator animator;
    private float attackTimer;
    private float attackDelay = 0.5f;
    private bool attacking = false;
    public GameObject target;
    public float readjustTimer = 0;
    public bool readjusting = false;
    private float hitTimer = 0;
    private bool hit = false;

    int count = 0;
    public override Vector2 GetLookDirection()
    {
        return getRandomDir();
    }

    public override Vector2 GetMoveDirection()
    {
            return getRandomDir();
    }



    public void readjust()
    {
        readjusting = true;
    }

    void Start()
    {

        
        attackTimer = Time.time;
        SharedProperties speed = ScriptableObject.CreateInstance("SharedProperties") as SharedProperties;
        speed.Value = 5f;
        SharedProperties acc = ScriptableObject.CreateInstance("SharedProperties") as SharedProperties;
        acc.Value = 1f;
        movementComponent.SetMovementSpeed(speed,acc);
        SharedProperties rot = ScriptableObject.CreateInstance("SharedProperties") as SharedProperties;
        rot.Value = 6f;
       // lookdir.SetLookSpeed(rot);
        gameObject.GetComponent<HealthComponent>().OnObjectDied += Die;
        gameObject.GetComponent<HealthComponent>().OnCurrentHealthReduced += Hit;



        timer = Time.time;
        wait = Random.Range(2, 5);
    }

    public void Hit()
	{
        animator.SetBool("Hit", true);
        attacking = false;
        hit = true;
        hitTimer = Time.time;
    }



    public void Die()
    {
        Destroy(gameObject);  
    }

    void FixedUpdate()
    {

        count++;
        if (count % 40 == 0)
        {
            
        }

        if (hit && Time.time - hitTimer > 0.5f)
        {
            hit = false;
            animator.SetBool("Hit", false);
        }
        else
        {



            if (readjusting && Time.time - readjustTimer > 1)
            {
                readjustTimer = Time.time;
                dir = transform.right;
                readjusting = false;

            }


            animator.SetFloat("Speed", dir.magnitude);
            if (target != null && (target.transform.position - gameObject.transform.position).magnitude < visionDist)
            {

              
                    dir = Vector2.right * (target.transform.position.x - gameObject.transform.position.x);

               


                animator.SetBool("Attacking", attacking);

                

            }

            else if (Time.time - timer > wait)
            {
                //target = null;

                wait = Random.Range(0, 3);
                timer = Time.time;
                if (dir == Vector2.zero)
                {

                    if (Random.Range(0, 2) == 1)
                    {

                        dir = Vector2.right;
                        //GetComponent<SpriteRenderer>().flipX = false;

                    }
                    else
                    {
                        dir = Vector2.left;

                        //GetComponent<SpriteRenderer>().flipX = true;
                    }

                }
                else
                {
                    dir = Vector2.zero;
                }
            }
            if (attacking)
            {
                if (Time.time - attackTimer > attackDelay)
                {
                    attackTimer = Time.time;
                    HealthComponent health = target.GetComponent<HealthComponent>();
                    if (health)
                    {
                        health.ProcessHit(5.0f);
                    }
                }
                if (readjusting)
                {
                    movementComponent.SetDesiredSpeed(dir);
                }
                else
                {
                    movementComponent.SetDesiredSpeed(Vector2.zero);
                }

            }
            else
            {
                movementComponent.SetDesiredSpeed(dir);
            }
            //lookdir.SetDesiredLookDirection(-dir);
        }
    }
    public void OnCollisionEnter2D(Collision2D c)
    {
       

        if (c.gameObject == target)
        {
            
            attacking = true;
            animator.SetBool("Attacking", attacking);
        }
    }

    public void OnCollisionExit2D(Collision2D c)
    {
        if (c.gameObject == target)
        {
            
            attacking = false;
            animator.SetBool("Attacking", attacking);
        }
    }

    
    public void getTarget()
    {
        //for (int i = 0; i < TerrainGenerator.entities.Count; i++)
        //{
        //    GameObject go = TerrainGenerator.entities[i];
        //    if (go.GetComponent<EntityTag>().entityType == EntityTag.EntityType.Robot)
        //    {
        //        if ((go.transform.position - gameObject.transform.position).magnitude <= visionDist)
        //        {

        //            target = go;
        //            break;
        //        }
        //    }
            
        //}
    }

    void OnDestroy()
    {
        //TerrainGenerator.entities.Remove(gameObject);
    }



    private Vector2 getRandomDir()
    {
        return dir;
    }

    public override bool GetPickUpState()
    {
        return false;
    }

    public override bool GetShootState()
    {
        return false;
    }

}
