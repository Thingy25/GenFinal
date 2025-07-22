using System;
using UnityEngine;

public class TriggerActivation : MonoBehaviour
{
    public AlertIcon alertIconScript;
    public bool isLeft = true;
    private BoxCollider _boxCollider;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isLeft)
        {
            alertIconScript.SetColorAlaDerecha();
            _boxCollider.enabled = false;
        }
        else if (other.CompareTag("Player") && isLeft)
        {
            alertIconScript.SetColorIZquierda();
            _boxCollider.enabled = false;
        }
    }
}
