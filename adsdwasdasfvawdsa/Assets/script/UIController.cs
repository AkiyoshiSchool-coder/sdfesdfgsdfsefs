using UnityEngine;
using System.Collections;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField] private GameObject CursorAtivo;
    [SerializeField] private GameObject BackImage;
    [SerializeField] private GameObject InventoryImage;
    public TMP_Text Inventory;
    public TMP_Text Info;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            InventoryImage.SetActive(!InventoryImage.activeInHierarchy);
        }
    }
    public void SetBackImage(bool choice)
    {
        BackImage.SetActive(choice);
    }
    public void AtivarCursor(bool choice)
    {
        CursorAtivo.SetActive(choice);
    }
    public void SetItem(Item item)
    {
        Inventory.text = item.collectText;
        Info.text = item.collectText;
        StartCoroutine(Fading());
    }

    IEnumerator Fading()
    {
        Color newColor = Info.color;
        while(newColor.a <1)
        {
            newColor.a += Time.deltaTime;
            Info.color = newColor;
            yield return null;
        }

        yield return new WaitForSeconds(2f);
        
        while(newColor.a >0)
        {
            newColor.a -= Time.deltaTime;
            Info.color = newColor;
            yield return null;
        }
    }
}
