using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationBlendController : MonoBehaviour
{
    private Animator m_Animator;

    private int m_VelocityXHash;
    private int m_VelocityZHash;

    private float m_InputX;
    private float m_InputZ;

    private float m_VelocityX;
    private float m_VelocityZ;

    private float m_MaxRunVelocity = 1f;
    private float m_MaxSprintVelocity = 2f;

    [SerializeField] private float m_Acceleration = 0.1f;
    [SerializeField] private float m_Deceleration = 0.3f;

    void Start()
    {
        m_Animator = GetComponent<Animator>();

        m_VelocityXHash = Animator.StringToHash("VelocityX");
        m_VelocityZHash = Animator.StringToHash("VelocityZ");
    }

    void Update()
    {
        m_InputX = Input.GetAxis("Horizontal");
        m_InputZ = Input.GetAxis("Vertical");

        bool sprintPressed = Input.GetKey(KeyCode.LeftShift);

        float currentMaxVelocity = sprintPressed ? m_MaxSprintVelocity : m_MaxRunVelocity;

        HandleVelocity(currentMaxVelocity);

        m_Animator.SetFloat(m_VelocityXHash, m_VelocityX);
        m_Animator.SetFloat(m_VelocityZHash, m_VelocityZ);
    }

    private void HandleVelocity(float currentMaxVelocity)
    {
        // Increase velocity left
        if (m_InputX > 0 && m_VelocityX < currentMaxVelocity)
        {
            m_VelocityX += m_Acceleration * Time.deltaTime;

            if (m_VelocityX > currentMaxVelocity) m_VelocityX = currentMaxVelocity;
        }

        // Increase velocity right
        if (m_InputX < 0 && m_VelocityX > -currentMaxVelocity)
        {
            m_VelocityX -= m_Acceleration * Time.deltaTime;

            if (m_VelocityX < -currentMaxVelocity) m_VelocityX = -currentMaxVelocity;
        }

        // Decrease left/right velocity
        if (m_InputX == 0 && m_VelocityX != 0)
        {
            if (m_VelocityX > 0) m_VelocityX -= m_Deceleration * Time.deltaTime;
            else if (m_VelocityX < 0) m_VelocityX += m_Deceleration * Time.deltaTime;

            if (m_VelocityX > -0.05f && m_VelocityX < 0.05f) m_VelocityX = 0;
        }

        // Increase velocity forward
        if (m_InputZ > 0 && m_VelocityZ < currentMaxVelocity)
        {
            m_VelocityZ += m_Acceleration * Time.deltaTime;

            if (m_VelocityZ > currentMaxVelocity) m_VelocityZ = currentMaxVelocity;
        }

        // Decrease velocity forward
        if (m_InputZ == 0.0f && m_VelocityZ != 0.0f)
        {
            m_VelocityZ -= m_Deceleration * Time.deltaTime;

            if (m_VelocityZ < 0.0f) m_VelocityZ = 0.0f;
        }

        // Decrease to run if not sprinting
        if (m_InputZ > 0 && m_VelocityZ > currentMaxVelocity)
        {
            m_VelocityZ -= m_Deceleration * Time.deltaTime;

            if (m_VelocityZ < currentMaxVelocity) m_VelocityZ = currentMaxVelocity;
        }
    }
}
