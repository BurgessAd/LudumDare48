using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeBase : MonoBehaviour
{
    public GameObject player;
    public Canvas shop;
    public List<ShopItem> shopItems;
    public GameObject shopButtonPrefab;
    public InventoryComponent playerInv;

    // Start is called before the first frame update
    void Start()
    {
        playerInv = player.GetComponent<InventoryComponent>();
        for (int i = 0; i < shopItems.Count; i++)
        {
            shopItems[i].bought = false;
            GameObject temp = Instantiate(shopButtonPrefab);
            temp.transform.SetParent(shop.transform.GetChild(0).GetChild(0));
            GameObject img = temp.transform.GetChild(0).gameObject;
            img.GetComponent<Image>().sprite = shopItems[i].shopImage;
            img.transform.SetParent(temp.transform);
            int x = new int();
            x = i;
            temp.GetComponent<Button>().onClick.AddListener(delegate { BuyItem(x); });
            temp.GetComponent<Image>().transform.localScale = Vector3.one;
            temp.GetComponent<ShopItemUI>().Setup(shopItems[i].text, shopItems[i].cost);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyItem(int index)
    {
		if (!shopItems[index].bought)
		{
            if (playerInv.RemoveItems(shopItems[index].cost))
            {
                shopItems[index].bought = true;
                //Debug.Log("Bought item " + index.ToString());
                Move p = player.GetComponent<Move>();
                player.GetComponent<LightDiminish>().maxLight += shopItems[index].lightlifeBuff;
                player.GetComponent<LightDiminish>().refill();

                Image img = shop.transform.GetChild(0).GetChild(0).GetChild(index).GetChild(0).gameObject.GetComponent<Image>();
                //Debug.Log(img.gameObject.name);
                Color tmp = img.color;
                tmp.a = 0.5f;
                img.color= tmp;
            }
            else
            {
                //Debug.Log("Insufficient funds");
            }
        }

        

    }


    void OnTriggerEnter2D(Collider2D col)
	{
        if(col.name == player.name)
		{
            shop.enabled = true;
            player.GetComponent<LightDiminish>().refill();
            
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.name == player.name)
        {
            shop.enabled = false;
           
        }

    }

}
