using UnityEngine;

public class FireExtinguisher : MonoBehaviour, ISpecialProps
{
    [SerializeField] ParticleSystem reactionVFX;
    void Start()
    {
        
    }

    void ISpecialProps.PerformSpecialAction()
    {
        reactionVFX.Play();
    }
}
