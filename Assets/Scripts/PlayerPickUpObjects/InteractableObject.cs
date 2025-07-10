using UnityEngine;
using UnityEngine.Events;
public class InteractableObject : MonoBehaviour
{
    private Outline outline;
    public string message;

    public UnityEvent onInteraction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        outline = GetComponent<Outline>();
        DisableOutLine();
    }

    public void Inteact()
    {
        onInteraction.Invoke();
    }

    public void DisableOutLine()
    {
        outline.enabled = false;
    } 

    public void EnableOutLine()
    {
        outline.enabled = true;
    }

}
