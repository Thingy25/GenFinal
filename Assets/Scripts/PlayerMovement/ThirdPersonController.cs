using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonController : MonoBehaviour
{
   // Input fields
   private ThirdPersonAction playerActionAsset;
   private InputAction move;
   private InputAction aim;
   
   //movement flieds
   private Rigidbody rb;
   [SerializeField] private float movementForce = 1f;
   [SerializeField] private float jumpForce = 5;
   [SerializeField] private float maxSpeed = 5f;
   [SerializeField] private float fallForce = 100f;
   private Vector3 forceDirection = Vector3.zero;
   [SerializeField] private Camera playerCamera;
   
   //cinemachine camera
   [SerializeField] private CinemachineCamera cinemachineCamera;
   [SerializeField] private CinemachineRotationComposer rotationComposer;
   public float originalFOV = 50;
   
   //Animation
   [SerializeField] private Animator animator;
   private float sadTimer = 30f;
   
   //Sound Effects
   public AudioSource pasos;
   
   //Conditionals
   public bool CanWalk = true;
   private bool isGrounded = true;


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
      playerActionAsset.Player.Pause.performed += PauseGame;
      move = playerActionAsset.Player.Move;
      aim = playerActionAsset.Player.Aim;
      playerActionAsset.Player.Enable();
   }
   private void OnDisable()
   {
      playerActionAsset.Player.Jump.performed -= DoJump;
      playerActionAsset.Player.Disable();
   }

   private void Update()
   {
      if (aim.ReadValue<float>() > 0.1)
      {
         CameraZoom();
         HudManager.Instance.ActivateCrossHair();
      }
      else
      {
         HudManager.Instance.DesactiveCrossHair();
         cinemachineCamera.Lens.FieldOfView = originalFOV;
         rotationComposer.TargetOffset = new Vector3(0, 0, 0);
      }

      sadTimer -= Time.deltaTime;
      if(sadTimer<0) SetSadIdle();


   }

   private void FixedUpdate()
   {
      if (CanWalk)
      {
         animator.SetFloat("RunValue", move.ReadValue<Vector2>() .magnitude);
      
         forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * movementForce;
         forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * movementForce;
      
         rb.AddForce(forceDirection, ForceMode.Impulse);
         forceDirection = Vector3.zero;

         if (rb.linearVelocity.y < 0f)
         {
           rb.AddForce(Vector3.down * fallForce* Time.fixedDeltaTime, ForceMode.Force);
         }
      }
      LookAt();
   }

   private void SetSadIdle()
   {
      animator.SetTrigger("IsSad");
      sadTimer = 30f;
   }

   private void CameraZoom()
   {
      cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(originalFOV, 50f, 3f);
      rotationComposer.TargetOffset = new Vector3(Mathf.Lerp(0,2,3f), 0, 0);
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
         animator.SetBool("IsJumping", true);
         forceDirection+= Vector3.up * jumpForce;
         rb.AddForce(forceDirection, ForceMode.Force);
         Debug.Log("Salto");
      }
     
   }

   private void PauseGame(InputAction.CallbackContext obj)
   {
      HudManager.Instance.PauseGame();
   }

   private bool IsGrounded()
   {
      Ray ray = new Ray(this.transform.position + Vector3.up * 0.25f, Vector3.down);
      if (Physics.Raycast(ray, out RaycastHit hit, 2f)) return true;
      else return false;
   }

   public void ChangeWalkBool()
   {
      if (CanWalk) CanWalk = false;
      else CanWalk = true;
   }

   private void OnCollisionEnter(Collision other)
   {
      if(other.gameObject.CompareTag("Ground") && isGrounded)
         animator.SetBool("IsJumping", false);
   }
}
