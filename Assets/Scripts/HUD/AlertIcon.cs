using System;
using System.Collections;
using Unity.VectorGraphics;
using UnityEngine;

public class AlertIcon : MonoBehaviour
{

   public Color targetColor;
   public Color fineColor;

   public SVGImage alaDerecha;
   public SVGImage alaIzuierda;
   public SVGImage salida;

   public GameObject alertaIcono;
   public GameObject oxigenoIcono;

   private bool blinkAlaDerecha = true;
   private bool blinkAlaIzquierda = false;

   private void Start()
   {
      StartCoroutine(StartBlinkDerecha());
   }

   public void SetColorAlaDerecha()
   {
      blinkAlaDerecha = false;
      blinkAlaIzquierda = true;
      alaDerecha.color = fineColor;
      alaIzuierda.color = targetColor;
      alertaIcono.SetActive(false);
      oxigenoIcono.SetActive(true);
      StartCoroutine(StartBlinkIzquierdo());
      ObjetiveManagerLevel1.Instance.SetPuzzleSprite();
   }

   public void SetColorIZquierda()
   {
      blinkAlaIzquierda = false;
      oxigenoIcono.SetActive(false);
      StartCoroutine(StartExitBlink());
      ObjetiveManagerLevel1.Instance.SetPanelSprite();
   }

   IEnumerator StartBlinkDerecha()
   {
      while (blinkAlaDerecha)
      {
         
         alaDerecha.color = targetColor;
         yield return new WaitForSeconds(0.5f);
         alaDerecha.color = fineColor;
         yield return new WaitForSeconds(0.5f);
        
      }
   }

   IEnumerator StartBlinkIzquierdo()
   {
      while (blinkAlaIzquierda)
      {
         alaIzuierda.color = targetColor;
         yield return new WaitForSeconds(0.5f);
         alaIzuierda.color = fineColor;
         yield return new WaitForSeconds(0.5f);
      }
   }

   IEnumerator StartExitBlink()
   {
      while (true)
      {
         salida.color= targetColor;
         yield return new WaitForSeconds(0.5f);
         salida.color = fineColor;
         yield return new WaitForSeconds(0.5f);
      }
   }
   
   
}
