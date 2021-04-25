using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ShopItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text textbox;
    public GameObject panel;
    public Canvas shop;
    public List<InventorySlot> cost;
    public string text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(Canvas _shop,string _text, List<InventorySlot> _cost)
	{
        shop = _shop;
        textbox = shop.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Text>();
        panel = shop.transform.GetChild(0).GetChild(1).gameObject;
        text = _text;
        cost = _cost;
        
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        panel.SetActive(true);
        textbox.enabled = true;
        for (int i = 0; i < cost.Count; i++)
        {
            for (int j = 0; j < cost[i].currentAmount; j++)
            {
                textbox.text = text;
                GameObject temp = new GameObject();
                temp.AddComponent<Image>();
                temp.transform.SetParent(panel.transform);
                temp.GetComponent<Image>().sprite = cost[i].item.UIImage;
                temp.GetComponent<Image>().transform.localScale = Vector3.one;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        panel.SetActive(false);
        textbox.enabled = false;
        foreach (Transform child in panel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

        


}
