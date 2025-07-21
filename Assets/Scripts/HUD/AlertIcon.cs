using System.Collections;
using UnityEngine;

public class AlertIcon : MonoBehaviour
{

    public GameObject alaIzquierda;
    public bool blinkAlaIzquierda = true;

    IEnumerator BlinkAlaIzquierda()
    {
        while (blinkAlaIzquierda)
        {
            alaIzquierda.SetActive(!alaIzquierda.activeSelf);
            yield return new WaitForSeconds(0.5f);
        }
    }
   
}
