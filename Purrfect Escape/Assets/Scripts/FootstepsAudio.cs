using UnityEngine;

public class FootstepsAudio : MonoBehaviour
{
    private AudioSource m_AudioSource;
    private Vector3 m_previousPosition;
    private float m_totalDistance;

    [SerializeField] private float m_stepThreshold = 0.2f;
    [SerializeField] private LayerMask m_footstepMask;
    [SerializeField] private AudioClip[] footstepClips;

    [SerializeField] private float m_minPitch = 0.95f;
    [SerializeField] private float m_maxPitch = 1.05f;
    private void Awake()
    {
        m_previousPosition = transform.position;
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        float distanceThisFrame = Vector2.Distance(transform.position, m_previousPosition);
        m_totalDistance += distanceThisFrame;

        if (m_totalDistance >= m_stepThreshold)
        {
            TakeStep();
            m_totalDistance = m_totalDistance % m_stepThreshold;
        }

        m_previousPosition = transform.position;
    }

    private void TakeStep()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.35f, m_footstepMask);
        if (hitInfo.collider != null && footstepClips.Length > 0)
        {
            m_AudioSource.pitch = Random.Range(m_minPitch, m_maxPitch);
            m_AudioSource.PlayOneShot(footstepClips[Random.Range(0, footstepClips.Length)]);
        }
    }
}