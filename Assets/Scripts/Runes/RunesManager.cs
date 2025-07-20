using System;
using UnityEngine;

public class RunesManager : MonoBehaviour
{
    public static RunesManager Instance;
    public int totalActivated = 0;
    [SerializeField] private StoneGate stoneGateScript;

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
            stoneGateScript.OpenTheGate();
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
