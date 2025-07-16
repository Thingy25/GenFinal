using UnityEngine;

public class BreakableObject : MonoBehaviour, IBreakables
{
    [SerializeField] 
    protected GameObject partToBreak;
    [SerializeField]
    protected GameObject brokenPart;

    bool wasbroken;
    void Start()
    {
        //Invoke("IBreakables.OnThrowableHit", 2);
    }

    public virtual void OnThrowableHit()
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
