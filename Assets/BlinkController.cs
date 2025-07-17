using UnityEngine;

public class BlinkController : MonoBehaviour
{
    public Renderer faceRenderer; // Asigna el renderer de la cara
    public Material normalFaceMaterial;
    public Material blinkFaceMaterial;

    public float blinkInterval = 3f; // Tiempo entre parpadeos
    public float blinkDuration = 0.1f; // Duración del parpadeo

    private float timer;

    void Start()
    {
        timer = blinkInterval;
        SetFace(normalFaceMaterial);
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            StartCoroutine(Blink());
            timer = blinkInterval + Random.Range(-1f, 1f); // Un poco aleatorio
        }
    }

    System.Collections.IEnumerator Blink()
    {
        SetFace(blinkFaceMaterial);
        yield return new WaitForSeconds(blinkDuration);
        SetFace(normalFaceMaterial);
    }

    void SetFace(Material mat)
    {
        Material[] mats = faceRenderer.materials;
        mats[0] = mat; // Asume que el material de la cara está en el slot 0
        faceRenderer.materials = mats;
    }
}