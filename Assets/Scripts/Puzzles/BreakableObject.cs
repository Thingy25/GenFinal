using UnityEngine;

public class BreakableObject : MonoBehaviour, IBreakables
{
    [SerializeField] 
    GameObject partToBreak;
    [SerializeField]
    GameObject brokenPart;
    void Start()
    {
        
    }

    void IBreakables.OnThrowableHit()
    {
        Debug.Log("BROOOOOOOOOOOken");
        partToBreak.SetActive(false);
        brokenPart.SetActive(true);
    }
}
