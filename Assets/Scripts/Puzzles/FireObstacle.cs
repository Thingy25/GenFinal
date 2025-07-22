using System.Collections;
using UnityEngine;

public class FireObstacle : MonoBehaviour
{
    [SerializeField]
    int damageAmount = 0;

    Coroutine dmgCoroutine;
    bool isPlayerOnFire = false;
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
            StartCoroutine(DealDamageOvTime(damageable));
            isPlayerOnFire = true;
        }
        //ISpecialProps prop = other.GetComponent<ISpecialProps>(); //other.TryGetComponent<ISpecialProps>(out );
        if (other.TryGetComponent<ISpecialProps>(out var extinguisher))
        {
            extinguisher.PerformSpecialAction();
            StartCoroutine(StopFire());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerOnFire = false;
            StopCoroutine("DealDamageOvTime");
        }
    }

    IEnumerator DealDamageOvTime(IDamageable damageable)
    {       
        while (isPlayerOnFire)
        {
            yield return new WaitForSeconds(0.35f);
            damageable.ReceiveDamage(damageAmount);
            Debug.Log("DJKDFHDKFN");
        }
    }

    IEnumerator StopFire()
    {
        yield return new WaitForSeconds(3); this.gameObject.SetActive(false);
    }
}
