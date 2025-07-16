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
        Debug.Log("fhwjfw");
        while (transform.localPosition.y <= 2.5f)
        {
            transform.Translate(Vector3.forward * 0.05f);
            yield return new WaitForSeconds(0.03f);
        }
    }
}
