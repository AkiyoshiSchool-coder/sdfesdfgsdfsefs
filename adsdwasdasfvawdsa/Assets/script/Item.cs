using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public bool Pegavel;
    public AudioClip audioClip;
    public string texto;

    
    public bool inventoryItem;
    public string collectText;
}
