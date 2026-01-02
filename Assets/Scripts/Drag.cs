using UnityEngine;
using System.Collections;

public enum BodyPartType
{
    Head,
    Hand,
    Leg,
    Eye,
    Ear,
    Nose,
    Mouth
}

public class Drag : MonoBehaviour
{
    [SerializeField] private Transform dropArea;
    [SerializeField] private float snapDistance = 0.7f;

    [SerializeField] private float shakeDuration = 0.15f;
    [SerializeField] private float shakeStrength = 0.1f;

    [SerializeField] private AudioClip correctChime;
    [SerializeField] private AudioClip wrongBoing;

    [SerializeField] private BodyPartType bodyPartType;
    [SerializeField] private Level1StarManager level1StarManager;
    [SerializeField] private Level2StarManager level2StarManager;
    [SerializeField] private Level3StarManager level3StarManager;
    [SerializeField] private Level3TimerManager level3TimerManager;
    [SerializeField] private MastiAnimationController mastiController;

    private Vector2 initialPosition;
    private Vector2 offset;
    private bool locked;

    private Camera cam;
    private Animator animator;
    private AudioSource audioSource;

    void Awake()
    {
        cam = Camera.main;
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        initialPosition = transform.position;
    }

    void OnMouseDown()
    {
        if (level3TimerManager != null)
            level3TimerManager.StartTimer();

        if (locked || cam == null) return;

        Vector2 mouseWorld = GetMouseWorldPos();
        offset = mouseWorld - (Vector2)transform.position;
    }

    void OnMouseDrag()
    {
        if (locked || cam == null) return;

        Vector2 mouseWorld = GetMouseWorldPos();
        transform.position = mouseWorld - offset;
    }

    void OnMouseUp()
    {
        if (locked || cam == null) return;

        float distance = Vector2.Distance(transform.position, dropArea.position);

        if (distance <= snapDistance)
            HandleCorrectPlacement();
        else
            StartCoroutine(ShakeAndReset());
    }

    void HandleCorrectPlacement()
    {
        transform.position = dropArea.position;
        locked = true;

        if (animator != null)
            animator.SetTrigger("Correct");

        if (mastiController != null)
            mastiController.PlayHappy();

        PlayCorrectAudio();
        PlayHaptic();

        if (level1StarManager != null)
            level1StarManager.ReportCorrectPlacement(bodyPartType);

        if (level2StarManager != null)
            level2StarManager.ReportCorrectPlacement(bodyPartType);

        if (level3StarManager != null)
            level3StarManager.AddStar();
    }

    void PlayCorrectAudio()
    {
        if (audioSource == null) return;

        if (correctChime != null)
            audioSource.PlayOneShot(correctChime);
    }

    void PlayHaptic()
    {
#if UNITY_ANDROID || UNITY_IOS
        Handheld.Vibrate();
#endif
    }

    IEnumerator ShakeAndReset()
    {
        Vector2 startPos = transform.position;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-shakeStrength, shakeStrength);
            transform.position = startPos + new Vector2(x, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = initialPosition;

        if (audioSource != null && wrongBoing != null)
            audioSource.PlayOneShot(wrongBoing);

        if (mastiController != null)
            mastiController.PlayThink();
    }

    Vector2 GetMouseWorldPos()
    {
        Vector3 screenPos = Input.mousePosition;
        screenPos.z = Mathf.Abs(cam.transform.position.z);
        return cam.ScreenToWorldPoint(screenPos);
    }
}
