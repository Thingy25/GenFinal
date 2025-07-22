using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class PuzzleCondition
{
    public Renderer targetRenderer;

    public enum TargetState
    {
        Off,       // ⚪
        On,        // 🟢
        Selected   // 🔴
    }

    public TargetState expectedState;
}

public class LightRotator : MonoBehaviour
{
    [Header("Luces modificables")]
    public List<Renderer> lights;
    public Material offMaterial;
    public Material onMaterial;
    public Material selectedMaterial;

    [Header("Jugador")]
    public Transform playerTransform;
    public float interactionDistance = 3.5f;
    public GameObject playerController;

    [Header("Condición de victoria")]
    public List<PuzzleCondition> puzzleConditions;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip puzzleSolvedClip;

    [Header("VFX")]
    public ParticleSystem confettiVFX;
    public float confettiDuration = 2f;

    [Header("Puerta y cámara")]
    public Transform doorToMove;
    public float doorMoveAmount = 1.5f;
    public float doorMoveDuration = 1f;
    //public Camera mainCamera;
    public GameObject doorCamera;
    public float cameraWaitTime = 2f;

    private int selectedIndex = 0;
    private HashSet<int> activeIndices = new HashSet<int>();
    private bool puzzleSolved = false;

    void Start()
    {
        UpdateVisuals();
        if (doorCamera != null) doorCamera.SetActive(false);
    }

    void Update()
    {
        if (puzzleSolved || playerTransform == null) return;
        //Debug.Log(Vector3.Distance(transform.position, playerTransform.position));
        if (Vector3.Distance(transform.position, playerTransform.position) > interactionDistance) return;

        float scroll = Mouse.current.scroll.ReadValue().y;
        if (scroll != 0)
        {
            selectedIndex = (selectedIndex + (scroll > 0 ? 1 : -1) + lights.Count) % lights.Count;
            UpdateVisuals();
        }

        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            Debug.Log("edjldlw");
            if (!activeIndices.Contains(selectedIndex) && activeIndices.Count < 7)
            {
                activeIndices.Add(selectedIndex);
                UpdateVisuals();
                CheckPuzzleSolution();
            }
        }

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (activeIndices.Contains(selectedIndex))
            {
                Debug.Log("dnslcskl");
                activeIndices.Remove(selectedIndex);
                UpdateVisuals();
                CheckPuzzleSolution();
            }
        }
    }

    void UpdateVisuals()
    {
        for (int i = 0; i < lights.Count; i++)
        {
            if (puzzleSolved)
            {
                lights[i].material = onMaterial;
            }
            else if (i == selectedIndex)
            {
                lights[i].material = selectedMaterial;
            }
            else if (activeIndices.Contains(i))
            {
                lights[i].material = onMaterial;
            }
            else
            {
                lights[i].material = offMaterial;
            }
        }
    }

    void CheckPuzzleSolution()
    {
        foreach (var condition in puzzleConditions)
        {
            if (condition.targetRenderer == null) continue;

            int index = lights.IndexOf(condition.targetRenderer);
            if (index == -1) return;

            switch (condition.expectedState)
            {
                case PuzzleCondition.TargetState.Off:
                    if (activeIndices.Contains(index)) return;
                    break;
                case PuzzleCondition.TargetState.On:
                    if (!activeIndices.Contains(index)) return;
                    break;
                case PuzzleCondition.TargetState.Selected:
                    if (index != selectedIndex) return;
                    break;
            }
        }

        puzzleSolved = true;
        Debug.Log("¡Puzzle resuelto!");
        ObjetiveManagerLevel1.Instance.SetHelmetSprite();
        LevelManager.Instance.CallNextLevelBeat();
        UpdateVisuals();

        if (audioSource && puzzleSolvedClip)
            audioSource.PlayOneShot(puzzleSolvedClip);

        if (confettiVFX != null)
        {
            confettiVFX.gameObject.SetActive(true);
            confettiVFX.Play();
        }

        if (playerController != null)
        {
            var movementScript = playerController.GetComponent<ThirdPersonController>();
            if (movementScript != null)
                movementScript.enabled = false;
        }

        StartCoroutine(SolveSequence());
    }

    IEnumerator SolveSequence()
    {
        // Esperar por la animación/confetti
        yield return new WaitForSeconds(confettiDuration);

        // Cambiar cámara
        if (doorCamera != null) //&& mainCamera != null)
        {
            //mainCamera.enabled = false;
            doorCamera.SetActive(true);
        }

        // Esperar para ver la puerta
        yield return new WaitForSeconds(cameraWaitTime);



        // Mover puerta suavemente
        if (doorToMove != null)
        {
            Vector3 startPos = doorToMove.position;
            Vector3 endPos = startPos + Vector3.up * doorMoveAmount;

            float elapsed = 0f;
            while (elapsed < doorMoveDuration)
            {
                elapsed += Time.deltaTime;
                doorToMove.position = Vector3.Lerp(startPos, endPos, elapsed / doorMoveDuration);
                yield return null;
            }

            doorToMove.position = endPos;
        }

        // Volver a la cámara principal
        if (doorCamera != null)
        {
            doorCamera.SetActive(false);
            //mainCamera.enabled = true;
        }

        // Restaurar movimiento
        if (playerController != null)
        {
            var movementScript = playerController.GetComponent<ThirdPersonController>();
            if (movementScript != null)
                movementScript.enabled = true;
        }
    }
}