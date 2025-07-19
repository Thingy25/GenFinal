using System;
using UnityEngine;

public class RunesManager : MonoBehaviour
{
    public static RunesManager Instance;
    private int totalActivated = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
    private void CheckActivation()
    {
        if (totalActivated == 5)
        {
            //Activar animacion puerta
            Debug.Log("Abrete sesamo");
        }
    }

    public void OneMoreActivated()
    {
        totalActivated += 1;
        if (totalActivated > 5) totalActivated = 5;
        else if(totalActivated ==5) CheckActivation();
    }

    public void OneLessActivated()
    {
        totalActivated -= 1;
        if (totalActivated < 0) totalActivated = 0;
    }
}
