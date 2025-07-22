using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManagercCnematicEscene : MonoBehaviour
{
    public void LoadScene()
    {
        Time.timeScale = 1;
        if (Time.timeScale != 1)
        {
        }
        SceneManager.LoadScene("Nivel1");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Nivel 2");
        }
        
    }
}
