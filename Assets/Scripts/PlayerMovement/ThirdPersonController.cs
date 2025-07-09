using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonController : MonoBehaviour
{
   // Input fields
   private ThirdPersonAction playerActionAsset;
   private InputAction move;
   
   //movement flieds
   private Rigidbody rb;
   [SerializeField] private float movementForce = 1f;
   [SerializeField] private float jumpForce = 5;
   [SerializeField] private float maxSpeed = 5f;
   private Vector3 forceDirection = Vector3.zero;
   [SerializeField] private Camera playerCamera;

   private void Awake()
   {
      rb = this.GetComponent<Rigidbody>();
      playerActionAsset = new ThirdPersonAction();
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
   }

   private void OnEnable()
   {
      playerActionAsset.Player.Jump.performed += DoJump;
      move = playerActionAsset.Player.Move;
      playerActionAsset.Player.Enable();
   }
   private void OnDisable()
   {
      playerActionAsset.Player.Jump.performed -= DoJump;
      playerActionAsset.Player.Disable();
   }

   private void FixedUpdate()
   {
      forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * movementForce;
      forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * movementForce;
      
      rb.AddForce(forceDirection, ForceMode.Impulse);
      forceDirection = Vector3.zero;

      if (rb.linearVelocity.y < 0f)
      {
         rb.linearVelocity += Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;
      }
      
      LookAt();
   }

   private void LookAt()
   {
      Vector2 input = move.ReadValue<Vector2>();
      if (input.sqrMagnitude > 0.1f)
      {
         Vector3 lookDirection = GetCameraRight(playerCamera) * input.x + GetCameraForward(playerCamera) * input.y;
         lookDirection.y = 0f;

         if (lookDirection != Vector3.zero)
         {
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * 10f));
         }
      }
        
   }

   private Vector3 GetCameraForward(Camera playerCamera1)
   {
      Vector3 forward = playerCamera.transform.forward;
      forward.y = 0;
      return forward.normalized;
   }

   private Vector3 GetCameraRight(Camera playerCamera1)
   {
      Vector3 right = playerCamera.transform.right;
      right.y = 0;
      return right.normalized;
   }


   private void DoJump(InputAction.CallbackContext obj)
   {
     
      
      if (IsGrounded())
      {
         forceDirection+= Vector3.up * jumpForce;
         rb.AddForce(forceDirection, ForceMode.Force);
         Debug.Log("Salto");
      }
   }

   private bool IsGrounded()
   {
      Ray ray = new Ray(this.transform.position + Vector3.up * 0.25f, Vector3.down);
      if (Physics.Raycast(ray, out RaycastHit hit, 0.3f)) return true;
      else return false;
   }
   
}
