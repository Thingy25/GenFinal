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
        if (other.TryGetComponent<IDamageable>(out var damageable))
        {
            dmgCoroutine = StartCoroutine(DealDamageOvTime(damageable));
        }
        ISpecialProps prop = other.GetComponent<ISpecialProps>(); //other.TryGetComponent<ISpecialProps>(out );
        if (other.TryGetComponent<ISpecialProps>(out var extinguisher))
        {
            extinguisher.PerformSpecialAction();
            StartCoroutine(StopFire());
        }
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

    IEnumerator StopFire()
    {
        yield return new WaitForSeconds(3); this.gameObject.SetActive(false);
    }
}
