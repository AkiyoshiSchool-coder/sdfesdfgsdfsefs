using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    [SerializeField] private Camera camera;
    [SerializeField] private float Distance;
    [SerializeField] private Vector3 Offset;
    [SerializeField] private Interactable currentInteractable;

    [SerializeField] private Vector3 originPosition;
    [SerializeField] private Quaternion originRotation;

    [SerializeField] private bool isViewing;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInteractables();
    }

    void CheckInteractables()
    {
        RaycastHit hit;
        Vector3 Origin = camera.ViewportToWorldPoint(new Vector3(Offset.x,Offset.y,Offset.z));

        if(Physics.Raycast(Origin, camera.transform.forward, out hit, Distance))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if(interactable !=null)
            {
                UIController.Instance.AtivarCursor(true);
                if(Input.GetButtonDown(0))
                {
                    currentInteractable = interactable;

                    isViewing = true;

                    if(currentInteractable.item.Pegavel)
                    {
                        originPosition = currentInteractable.transform.position;
                        originRotation = currentInteractable.transform.rotation;
                    }
                }
            }
            else
            {
                UIController.Instance.AtivarCursor(false);
            }
        }
        else
        {
            UIController.Instance.AtivarCursor(false);
        }
    }
}
