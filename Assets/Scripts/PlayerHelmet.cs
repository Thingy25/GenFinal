using UnityEngine;

public class PlayerHelmet : MonoBehaviour
{
    public delegate void HelmetAvailableEvent();
    public static event HelmetAvailableEvent OnHelmetEnabled;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnHelmetEnabled?.Invoke();
        }
    }
}
