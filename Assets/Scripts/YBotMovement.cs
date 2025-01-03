using UnityEngine;

public class YBotMovement : MonoBehaviour
{
    private Animator m_Animator;

    private int m_IsRunningHash;
    private int m_IsSprintingHash;

    private InputSystem_Actions m_Actions;

    private Vector2 m_CurrentMovement;
    private bool m_MovementPressed;
    private bool m_SprintPressed;

    private void Awake()
    {
        m_Actions = new();

        m_Actions.YBotMovement.Movement.performed += ctx =>
        {
            m_CurrentMovement = ctx.ReadValue<Vector2>();
            m_MovementPressed = m_CurrentMovement.x != 0 || m_CurrentMovement.y != 0;
        };
        m_Actions.YBotMovement.Run.performed += ctx => m_SprintPressed = ctx.ReadValueAsButton();
    }

    void Start()
    {
        m_Animator = GetComponent<Animator>();

        m_IsRunningHash = Animator.StringToHash("isRunning");
        m_IsSprintingHash = Animator.StringToHash("isSprinting");
    }

    void Update()
    {
        HandleMovement();

        HandleRotation();
    }

    private void HandleRotation()
    {
        Vector3 currentPosition = transform.position;

        Vector3 newPosition = new Vector3(m_CurrentMovement.x, 0, m_CurrentMovement.y);

        Vector3 lookAtPosition = currentPosition + newPosition;

        transform.LookAt(lookAtPosition);
    }

    private void HandleMovement()
    {
        bool isRunning = m_Animator.GetBool(m_IsRunningHash);
        bool isSprinting = m_Animator.GetBool(m_IsSprintingHash);

        if (m_MovementPressed && !isRunning)
        {
            m_Animator.SetBool(m_IsRunningHash, true);
        }

        if (!m_MovementPressed && isRunning)
        {
            m_Animator.SetBool(m_IsRunningHash, false);
        }

        if ((m_MovementPressed && m_SprintPressed) && !isSprinting)
        {
            m_Animator.SetBool(m_IsSprintingHash, true);
        }

        if ((!m_MovementPressed || !m_SprintPressed) && isSprinting)
        {
            m_Animator.SetBool(m_IsSprintingHash, false);
        }
    }

    private void OnEnable()
    {
        m_Actions.Enable();
    }

    private void OnDisable()
    {
        m_Actions.Disable();
    }
}
