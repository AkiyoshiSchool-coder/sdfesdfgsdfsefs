using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField] private GameObject CursorAtivo;
    [SerializeField] private GameObject BackImage;

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
        
    }
    public void SetBackImage(bool choice)
    {
        BackImage.SetActive(choice);
    }
    public void AtivarCursor(bool choice)
    {
        CursorAtivo.SetActive(choice);
    }
}
