using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using UnityEngine.Events;

public class PlayerInteract : MonoBehaviour
{

    [SerializeField] private Camera camera;
    [SerializeField] private float Distance;
    [SerializeField] private float rotateSpeed = 200;
    [SerializeField] private Transform objViewer;
    [SerializeField] private Vector3 Offset;
    [SerializeField] private Interactable currentInteractable;

    [SerializeField] private Vector3 originPosition;
    [SerializeField] private Quaternion originRotation;

    [SerializeField] private bool isViewing;
    [SerializeField] private bool canFinish;
    public UnityEvent OnView;
    public UnityEvent OnFinishView;
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

        if(isViewing)
        {
            if(currentInteractable.item.Pegavel && Input.GetMouseButton(0))
            {
                RotateObject();
            }
            if(canFinish && Input.GetMouseButtonDown(1))
            {
                FinishViewing();
            }
            return;
        }

        RaycastHit hit;
        Vector3 Origin = camera.ViewportToWorldPoint(new Vector3(Offset.x,Offset.y,Offset.z));

        if(Physics.Raycast(Origin, camera.transform.forward, out hit, Distance))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if(interactable !=null)
            {
                UIController.Instance.AtivarCursor(true);
                if(Input.GetMouseButtonDown(0))
                {
                    if(interactable.isMoving)
                    {
                        return;
                    }

                    OnView.Invoke();

                    currentInteractable = interactable;

                    isViewing = true;

                    Invoke("CanFinish",1f);

                    if(currentInteractable.item.Pegavel)
                    {
                        originPosition = currentInteractable.transform.position;
                        originRotation = currentInteractable.transform.rotation;
                        StartCoroutine(MovingObject(currentInteractable, objViewer.position));
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
    public void CanFinish()
    {
        canFinish = true;
        UIController.Instance.SetBackImage(true);
    }

    public void FinishViewing()
    {
        canFinish = false;
        isViewing = false;
        UIController.Instance.SetBackImage(false);
        if(currentInteractable.item.Pegavel)
        {
            currentInteractable.transform.rotation = originRotation;
            StartCoroutine(MovingObject(currentInteractable,originPosition));
        }
        OnFinishView.Invoke();
    }
    IEnumerator MovingObject(Interactable obj, Vector3 pos)
    {
        obj.isMoving = true;
        float timer = 0;
        while(timer<1)
        {
            obj.transform.position = Vector3.Lerp(obj.transform.position,pos, Time.deltaTime * 5);
            timer+= Time.deltaTime;
            yield return null;
        }
        obj.transform.position = pos;
        obj.isMoving = false;
    }

    void RotateObject()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        currentInteractable.transform.Rotate(camera.transform.right, - Mathf.Deg2Rad * y * rotateSpeed, Space.World);
        currentInteractable.transform.Rotate(camera.transform.up, -Mathf.Deg2Rad * x * rotateSpeed, Space.World);
    }
}
