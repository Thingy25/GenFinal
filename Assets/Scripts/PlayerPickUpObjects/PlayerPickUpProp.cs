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

    private void Awake()
    {
        playerActionAsset = new ThirdPersonAction();
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
            Debug.Log("interactua");
            float pickUpDistance = 20f;
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
            Debug.Log("Lanzao el objeto");
            objectGrabbable.Drop();
            objectGrabbable = null;
        }
       
    }

   
}
