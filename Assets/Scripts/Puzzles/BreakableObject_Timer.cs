using System.Collections;
using UnityEngine;

public class BreakableObject_Timer : BreakableObject
{
    [SerializeField]
    float reactionTimer = 0;
    [SerializeField]
    GameObject objectToHide;
    void Start()
    {

    }

    public override void OnThrowableHit()
    {
        base.OnThrowableHit();
        StartCoroutine(OnObjectBroken());
    }

    IEnumerator OnObjectBroken()
    {
        yield return new WaitForSeconds(reactionTimer);
        objectToHide.SetActive(false);
        this.enabled = false;
    }
}
