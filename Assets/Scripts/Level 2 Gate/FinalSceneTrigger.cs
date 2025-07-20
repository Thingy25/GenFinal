using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalSceneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable") && other.TryGetComponent<ShipBattery>(out ShipBattery shipBattery))
        {
            SceneManager.LoadScene("hannah/cinematica 2/Nivel 2  cinematica");
        }
    }
}
