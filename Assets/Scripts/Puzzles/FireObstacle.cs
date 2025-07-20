using System.Collections;
using UnityEngine;

public class FireObstacle : MonoBehaviour
{
    [SerializeField]
    int damageAmount = 0;

    Coroutine dmgCoroutine;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            dmgCoroutine = StartCoroutine(DealDamageOvTime(damageable));
        }
        ISpecialProps prop = other.GetComponent<ISpecialProps>();
    }

    private void OnTriggerExit(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            StopCoroutine(dmgCoroutine);
        }
    }

    IEnumerator DealDamageOvTime(IDamageable damageable)
    {       
        while (true)
        {
            yield return new WaitForSeconds(0.35f);
            damageable.ReceiveDamage(damageAmount);
        }
    }
}
