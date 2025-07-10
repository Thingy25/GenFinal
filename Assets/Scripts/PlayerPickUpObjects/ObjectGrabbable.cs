using System;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
   private Rigidbody rb;
   private Transform objectGrabPointTransform;

   private void Awake()
   {
      rb = GetComponent<Rigidbody>();
   }

   public void Grab(Transform objectGrabTransform)
   {
      this.objectGrabPointTransform = objectGrabTransform;
      rb.useGravity = false;
   }

   public void Drop( Vector3 cameraPos)
   {
      this.objectGrabPointTransform = null;
      rb.useGravity = true;
      rb.AddForce(cameraPos * 50f, ForceMode.Impulse);
   }

   private void FixedUpdate()
   {
      if (objectGrabPointTransform != null)
      {
         Vector3 newPos=  Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.fixedDeltaTime * 50f);
         rb.MovePosition(newPos);
      }
   }

   void OnCollisionEnter(Collision collision)
   {
       IBreakables breakable = collision.gameObject.GetComponent<IBreakables>();
       breakable?.OnThrowableHit();
   }
}
