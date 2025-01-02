using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    private int isRunningHash;
    private int isSprintingHash;
    private int velocityHash;

    private float velocity = 0.0f;
    [SerializeField] private float acceleration = 0.1f;
    [SerializeField] private float deceleration = 0.3f;

    void Start()
    {
        animator = GetComponent<Animator>();

        isRunningHash = Animator.StringToHash("isRunning");
        isSprintingHash = Animator.StringToHash("isSprinting");

        velocityHash = Animator.StringToHash("Velocity");
    }

    void Update()
    {
        float vertInput = Input.GetAxis("Vertical");
        bool isRunning = animator.GetBool(isRunningHash);

        bool isSprinting = animator.GetBool(isSprintingHash);
        bool sprintPressed = Input.GetKey(KeyCode.LeftShift);

        if (vertInput > 0 && velocity < 1)
        {
            velocity += Time.deltaTime * acceleration;

            velocity = velocity >= 1 ? 1 : velocity;
        }

        if (vertInput == 0 && velocity > 0)
        {
            velocity -= Time.deltaTime * deceleration;

            velocity = velocity <= 0 ? 0 : velocity;
        }

        animator.SetFloat(velocityHash, velocity);

        #region Legacy
        //if (!isRunning && vertInput > 0)
        //{
        //    animator.SetBool(isRunningHash, true);
        //}

        //if (isRunning && vertInput == 0)
        //{
        //    animator.SetBool(isRunningHash, false);
        //}

        //if (!isSprinting && vertInput > 0 && sprintPressed)
        //{
        //    animator.SetBool(isSprintingHash, true);
        //}

        //if (isSprinting && (vertInput == 0 || !sprintPressed))
        //{
        //    animator.SetBool(isSprintingHash, false);
        //}
        #endregion
    }
}
