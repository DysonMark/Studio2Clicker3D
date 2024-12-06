using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    #region Variables
    
    private Rigidbody rb;
    [SerializeField] private float speed = 5;
    CustomActions input;
    CustomActions jump;
    CustomActions sprint;
    NavMeshAgent agent;
    private Vector3 destination;
    [SerializeField] LayerMask clickableLayers;
    [SerializeField] private float lookRotationSpeed = 5f;
    [SerializeField] private float jumpForce = 5;    
    private Animator animator;
    private Vector3 comparative = new Vector3(0, 0, 0);
    private bool held = false;
    [SerializeField] private float addSpeed = 2.5f;
    public bool isMoving = false;
    #endregion

    void Awake()
     {
         agent = GetComponent<NavMeshAgent>();
         animator = GetComponent<Animator>();
         input = new CustomActions();
         jump = new CustomActions();
         sprint = new CustomActions();
         AssignInputs();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void AssignInputs()
    {
       input.Main.Move.performed += ctx => MouseMovement();
       jump.Main.Jump.performed += ctx => Jump(ctx);
       sprint.Main.Sprint.performed += ctx => Sprint(ctx);
       sprint.Main.Sprint.canceled += ctx => NoSprint(ctx);
       input.Main.StopMoving.performed += ctx => StopMoving();
    }

    void OnEnable()
    {
        input.Enable();
        jump.Enable();
        sprint.Enable();
    }

    void OnDisable()
    {
        input.Disable();
        jump.Disable();
        sprint.Disable();
    }

    private void Update()
    {
        Look();
    }

    #region Movement
    
    void MouseMovement()
    { 
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, clickableLayers))
        {
            animator.SetBool("hasClicked", true);
            agent.destination = hit.point;
            isMoving = true;
        }
    }
     void Jump(InputAction.CallbackContext context)
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void Sprint(InputAction.CallbackContext context)
    {
        animator.SetBool("shift", true);
        agent.speed = agent.speed + addSpeed;
    }
    
    void NoSprint(InputAction.CallbackContext context)
    {
        agent.speed = 6;
        animator.SetBool("shift", false);
        animator.SetBool("HasClicked", true);
    }

    private void StopMoving()
    {
        agent.SetDestination(transform.position);
    }

    private void Look()
    {
        Vector3 direction = (agent.destination - transform.position).normalized;
        if (direction == comparative)
            animator.SetBool("hasClicked", false);
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed); // * lookRotationSpeed);
    }
    #endregion
}
