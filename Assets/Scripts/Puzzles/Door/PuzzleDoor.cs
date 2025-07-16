using System.Collections;
using UnityEngine;

public class PuzzleDoor : ActivatableObject
{
    void Start()
    {
        //Invoke("OnActivation", 2f);
    }

    public override void OnActivation()
    {
        StartCoroutine(OpenDoor());
    }

    IEnumerator OpenDoor()
    {
        while (transform.position.x >= -1.5)
        {
            transform.Translate(-Vector3.right * 0.05f);
            yield return new WaitForSeconds(0.03f);
        }
    }
}
