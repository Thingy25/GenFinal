using System;
using UnityEngine;

public class TriggerActivation : MonoBehaviour
{
    public AlertIcon alertIconScript;
    public bool isLeft = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isLeft)
        {
            alertIconScript.SetColorAlaDerecha();
        }
        else if (other.CompareTag("Player") && isLeft)
        {
            alertIconScript.SetColorIZquierda();
        }
    }
}
