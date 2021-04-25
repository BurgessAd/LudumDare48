using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
   
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
