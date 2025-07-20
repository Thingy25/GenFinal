using System;
using DG.Tweening;
using UnityEngine;

public class StoneGate : MonoBehaviour
{
   [SerializeField] private Transform targetPosition;
   [SerializeField] private GameObject gateCamera;
   [SerializeField] private GameObject batteryCamera;
   private AudioSource audioSource;

private void Start()
   {
      audioSource = GetComponent<AudioSource>();
   }

   public void OpenTheGate()
   {
      Sequence openSequence = DOTween.Sequence();
      openSequence.InsertCallback(0f, ActivateCamera);
      openSequence.Append(transform.DOShakePosition(3f, 0.2f));
      openSequence.InsertCallback(4f,ActivateGatesound);
      openSequence.Append(transform.DOMove(targetPosition.position,5f).SetEase(Ease.InSine));
      openSequence.AppendCallback(DesctivateGatesound);
   }

   private void ActivateCamera()
   {
      gateCamera.SetActive(true);
   }

   private void ActivateGatesound()
   {
      audioSource.Play();
   }

   private void DesctivateGatesound()
   {
      batteryCamera.SetActive(true);
      gateCamera.SetActive(false);
      audioSource.Stop();
      Invoke("DesactivateBatteryCamera", 5f);
   }

   private void DesactivateBatteryCamera()
   {
      batteryCamera.SetActive(false);
   }
   
   
    
}
