using UnityEngine;

public class FireExtinguisher : MonoBehaviour, ISpecialProps
{
    [SerializeField] ParticleSystem reactionVFX;
    void Start()
    {
        reactionVFX = GetComponent<ParticleSystem>();
    }

    void ISpecialProps.PerformSpecialAction()
    {
        reactionVFX.Play();
    }
}
