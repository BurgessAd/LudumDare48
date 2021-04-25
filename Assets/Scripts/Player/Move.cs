using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    private float forwardSpeed = 0f;
    private float sideSpeed = 0f;
    private float speed = 0.05f;
    public Canvas inventoryUI;
    private InventoryComponent inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<InventoryComponent>();
        inventory.OnInventoryChanged += updateInventory;
        updateInventory();
    }

    // Update is called once per frame
    void Update()
    {
        //movement for the player
        forwardSpeed = Input.GetAxis("Vertical");
        sideSpeed = Input.GetAxis("Horizontal");


// Transform camTransform = Camera.main.gameObject.transform;

        Vector3 velocity = Vector3.up * forwardSpeed + Vector3.right * sideSpeed;
        velocity = velocity.normalized;
        //velocity = Vector3.ProjectOnPlane(velocity, Vector3.up);
        //velocity *= speed;
        ///Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        //rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
        //rb.AddForce(-9 * Vector3.up);
        //jump with space key

        //if (Input.GetKeyDown("space") && rb.velocity.y == 0)
        //{
        // rb.velocity += Vector3.up * 15;
        //}

        gameObject.transform.position += speed*velocity;

		if (Input.GetKeyDown("i"))
		{
			if (inventoryUI.enabled)
			{
                inventoryUI.enabled = false;
                
			}
			else
			{
                inventoryUI.enabled = true;
                
                
			}
		}
    }

    public void Upgrade(int pickaxeBuff,int lightlifeBuff)
	{

	}

    public void updateInventory()
	{

        foreach (Transform child in inventoryUI.transform.GetChild(0).GetChild(0))
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < inventory.Container.Count; i++)
        {
            for (int j = 0; j < inventory.Container[i].currentAmount; j++)
            {
                GameObject temp = new GameObject();
                temp.AddComponent<Image>();
                temp.transform.SetParent(inventoryUI.transform.GetChild(0).GetChild(0));
                temp.GetComponent<Image>().sprite = inventory.Container[i].item.UIImage;
                temp.GetComponent<Image>().transform.localScale = Vector3.one;
            }
        }
    }

}
