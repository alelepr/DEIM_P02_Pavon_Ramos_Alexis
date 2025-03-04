using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]  float speed;
    //[SerializeField] private Transform playerPointer;
    [SerializeField] private Animator anim;
    Rigidbody rb;
    [SerializeField] Transform cam;

    private bool moving;
    private bool canAttack;


    void Start()
    {
        canAttack = true;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        PlayerMovement();

    }

    private void PlayerMovement()
    {
        moving = false;
         
        //inputs
        float horInput = Input.GetAxisRaw("Horizontal") * speed;
        float verInput = Input.GetAxisRaw("Vertical") * speed;

        //camera direction
        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        camForward.y = 0;
        camRight.y = 0;


        //creating relative cam direction
        Vector3 forwardRelative = verInput * camForward;
        Vector3 rightRelative = horInput * camRight;

        Vector3 moveDir = forwardRelative + rightRelative;

        //movement
        rb.linearVelocity = new Vector3(moveDir.x, rb.linearVelocity.y, moveDir.z);

        transform.forward = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

      
               
       /* 
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.x = 0;
        transform.rotation = Quaternion.Euler(rotation);
        anim.SetBool("moving", moving);*/

    }
    public void AttackEnded()
    {

        canAttack = true;
    }
}
