using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ShopItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text text;
    public GameObject panel;
    // public List<InventorySlot> cost;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(string _text, List<InventorySlot> cost)
	{

        text.text = _text;
        for (int i = 0; i < cost.Count; i++)
        {
            for (int j = 0; j < cost[i].currentAmount; j++)
            {
                GameObject temp = new GameObject();
                temp.AddComponent<Image>();
                temp.transform.SetParent(panel.transform);
                temp.GetComponent<Image>().sprite = cost[i].item.UIImage;
                temp.GetComponent<Image>().transform.localScale = Vector3.one;
            }
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        panel.SetActive(true);
        text.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        panel.SetActive(false);
        text.enabled = false;
    }

        


}
