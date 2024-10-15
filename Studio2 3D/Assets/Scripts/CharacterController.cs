using TMPro.EditorUtilities;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
   // [SerializeField] private Tilemap map;
    //private Vector3 input;
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
        //destination = transform.position;
        rb = GetComponent<Rigidbody>();
        //agent = GetComponent<NavMeshAgent>();
        //AssignInputs();
    }

    void AssignInputs()
    {
       input.Main.Move.performed += ctx => MouseMovement();
       jump.Main.Jump.performed += ctx => Jump(ctx);
       sprint.Main.Sprint.performed += ctx => Sprint(ctx);
    }

    void MouseMovement()
    {
      /* Vector2 mousePosition = input.Main.MousePosition.ReadValue<Vector2>();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3Int gridPosition = map.WorldToCell(mousePosition);
        if (map.HasTile(gridPosition))
        {
            destination = mousePosition;
        }*/

      
        RaycastHit hit;
        /*if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, clickableLayers))
        {
            if (hit.collider != null)
            {
                rb.isKinematic = true;
                transform.position = new Vector3(transform.position.x, gridPosition.y + 0.5f, transform.position.z);
            }
        }*/
     if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, clickableLayers))
     {
         animator.SetBool("hasClicked", true);
         //Debug.Log("test: " + transform.position);
         //Debug.Log(hit.point);
         agent.destination = hit.point;
         Debug.Log(agent.destination);
         Debug.Log("test 2: " + hit.point);

         //Instantiate(this, hit.point += new Vector3(0, 0.1f, 0), Quaternion.identity);
     }
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
        //Jump();
        Debug.Log("mouse position: " + Input.mousePosition);
        Debug.Log("speed: " + speed);
        Debug.Log("agent speed: " + agent.speed);
    }

     void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("hello?");
        //agent.enabled = false;
        if (context.performed)
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void Sprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            animator.SetBool("shift", true);
            animator.SetBool("HasClicked", false);
            agent.speed = agent.speed + 2;
        }
        else
        {
            agent.speed = 3;
            animator.SetBool("shift", false);
            animator.SetBool("HasClicked", true);
        }

    }

    private void Look()
    {
        //current position + direction pressed on keyboard - current position to find angle
        // var relative = (transform.position + agent.destination) - transform.position;
        //rotate around the up axis
        //var rot = Quaternion.LookRotation(relative, Vector3.up); // Vector3);
        //transform.rotation = rot;
        Vector3 direction = (agent.destination - transform.position).normalized;
        if (direction == comparative)
            animator.SetBool("hasClicked", false);
        Debug.Log("direction: " + direction);
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
    }

    /* private void Update()
    {
        //input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        //GetInput();
        //Look();
        if (Vector3.Distance(transform.position, destination) > 0.1f)
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        MouseMovement();

    }*/
    

    /*private void GetInput()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = 0;
        input.z = Input.GetAxisRaw(("Vertical"));
    }

    private void Move()
    {
      //  rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
    }*/
}
