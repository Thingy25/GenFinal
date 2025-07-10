using UnityEngine;

public class BreakableObject : MonoBehaviour, IBreakables
{
    [SerializeField] 
    GameObject partToBreak;
    [SerializeField]
    GameObject brokenPart;

    bool wasbroken;
    void Start()
    {
        //Invoke("IBreakables.OnThrowableHit", 2);
    }

    void IBreakables.OnThrowableHit()
    {
        if (!wasbroken)
        {
            Debug.Log("BROOOOOOOOOOOken");
            partToBreak.SetActive(false);
            brokenPart.SetActive(true);
            wasbroken = true;
        }
    }
}
