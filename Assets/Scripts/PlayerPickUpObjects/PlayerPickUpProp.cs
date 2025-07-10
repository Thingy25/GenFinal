using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerPickUpProp : MonoBehaviour
{
    private ThirdPersonAction playerActionAsset;
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private LayerMask pickUpLayerMask;

    private ObjectGrabbable objectGrabbable;
    private InteractableObject currentInteractable;
    float pickUpDistance = 20f;

    private void Awake()
    {
        playerActionAsset = new ThirdPersonAction();
    }

    private void Update()
    {
        if (objectGrabbable == null)
        {
            CheckInteract();
        }
        else
        {
            DisableCurrentInteractable();
        }
       
    }

    void OnEnable()
    {
        playerActionAsset.Enable();
       playerActionAsset.Player.PickUp.performed += OnPick;
    }

    void OnDisable()
    {
        playerActionAsset.Player.PickUp.performed -= OnPick;
        playerActionAsset.Disable();
    }

    private void OnPick(InputAction.CallbackContext context)
    {
        if (objectGrabbable == null)
        {
            Ray ray = new Ray(playerCameraTransform.position, playerCameraTransform.forward);
            if (Physics.Raycast(ray, out RaycastHit rayCastHit, pickUpDistance, pickUpLayerMask))
            {
                if (rayCastHit.transform.TryGetComponent( out objectGrabbable))
                {
                    objectGrabbable.Grab(objectGrabPointTransform);
                    Debug.Log(objectGrabbable);
                }
            }
            
        }
        else
        {
            objectGrabbable.Drop(playerCameraTransform.forward);
            objectGrabbable = null;
        }

        if (currentInteractable != null)
        {
            currentInteractable.Inteact();
        }
      
       
    }

    void CheckInteract()
    {
        RaycastHit hit;
        Ray ray = new Ray(playerCameraTransform.position, playerCameraTransform.forward);
        if (Physics.Raycast(ray, out hit, pickUpDistance, pickUpLayerMask))
        {
            if (hit.collider.tag == "Interactable" )
            {
                
                InteractableObject newInteractable = hit.collider.GetComponent<InteractableObject>();
                if (currentInteractable && newInteractable != currentInteractable)
                {
                    currentInteractable.DisableOutLine();
                }
                if (newInteractable.enabled)
                {
                    SetNewCurrentInteractable(newInteractable);
                }
                else DisableCurrentInteractable();
            }
            else DisableCurrentInteractable();
        }
        else DisableCurrentInteractable();
        
    }
    void SetNewCurrentInteractable(InteractableObject newInteractable)
    {
        currentInteractable = newInteractable;
        currentInteractable.EnableOutLine();
        HudManager.Instance.EnableInteraction(currentInteractable.message + " (E)");
    }

    void DisableCurrentInteractable()
    {
        HudManager.Instance.DisableInteractionText();
        if(currentInteractable)
        {
            currentInteractable.DisableOutLine();
            currentInteractable = null;
        }
    }
}
