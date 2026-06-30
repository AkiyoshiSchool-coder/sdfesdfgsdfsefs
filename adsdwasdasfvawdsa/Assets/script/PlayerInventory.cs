using UnityEngine;
using System.Collections.Generic;   

public class PlayerInventory : MonoBehaviour
{
    public List<Item> itens = new List<Item>();

    public void AddItem(Item item)
    {
        if(itens.Contains(item))
        {
            return;
        }

        UIController.Instance.SetItem(item);

        itens.Add(item);
    }
}
