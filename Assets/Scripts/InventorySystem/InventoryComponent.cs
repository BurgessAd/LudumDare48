using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// contains all items that the player or the enemy currently has
// and drops them all on death if it receives a health component callback
public class InventoryComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject itemObjRef;


    public event Action OnInventoryChanged;


    void Awake()
    {
        if (GetComponent<HealthComponent>())
        {
            GetComponent<HealthComponent>().OnObjectDied += EmptyInventoryOntoFloor;
        }
    }
    private void EmptyInventoryOntoFloor()
    {

        
        for (int i = 0; i < Container.Count; i++)
        {
            
            GameObject newItem = Instantiate(itemObjRef);
            
            newItem.transform.localScale = Vector3Int.one * 3;
            Transform thisTransform = newItem.GetComponent<Transform>();
            ItemObjectPickable pickableObject = newItem.GetComponent<ItemObjectPickable>();

            pickableObject.SetItemObject(Container[i].item, Container[i].currentAmount);
            thisTransform.position = gameObject.transform.position;
        }
    }

    public List<InventorySlot> Container = new List<InventorySlot>();

    public void AddItem(ItemObject _item, int _amount)
    {
        bool hasItem = false;
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item == _item)
            {
                Container[i].AddAmount(_amount);
                hasItem = true;
                break;
            }
        }
        if (!hasItem)
        {
            Container.Add(new InventorySlot(_item, _amount));
        }
        OnInventoryChanged?.Invoke();
    }

    public bool RemoveItems(List<InventorySlot> _cost)
	{
        int count = 0;

        for(int j = 0; j < _cost.Count; j++)
		{
            for (int i = 0; i < Container.Count; i++)
            {
                if (Container[i].item == _cost[j].item && Container[i].currentAmount > _cost[j].currentAmount)
                {
                    //Container[i].AddAmount(-_amount);
                    count++;
                    break;
                }
            }
        }
		if (count == _cost.Count)
		{
            for (int j = 0; j < _cost.Count; j++)
            {
                for (int i = 0; i < Container.Count; i++)
                {
                    if (Container[i].item == _cost[j].item && Container[i].currentAmount > _cost[j].currentAmount)
                    {
                        Container[i].AddAmount(-_cost[j].currentAmount);
                        
                        break;
                    }
                }
            }
            OnInventoryChanged?.Invoke();
            return true;
        }
		else
		{
            return false;
		}

        
        
    }


}

[Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int currentAmount;
    public InventorySlot(ItemObject _item, int _amount)
    {
        item = _item;
        currentAmount = _amount;
    }
    public void AddAmount(in int value)
    {
        currentAmount += value;
        
        OnItemSlotAmountChanged?.Invoke(currentAmount);
    }
    public void EmptySlot()
    {
        currentAmount = 0;
        OnItemSlotAmountChanged?.Invoke(currentAmount);
    }

    public event Action<int> OnItemSlotAmountChanged;
}
