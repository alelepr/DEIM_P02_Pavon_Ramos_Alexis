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

    //PUZZLE 2

    public bool tieneLlaveTrastero;
    public bool tieneLlaveCofre;
    [SerializeField] GameObject puertaTrastero;

    //PUZZLE 3
    public int trozosDePapelRecolectados = 0; // Cuenta los trozos de papel recolectados
    [SerializeField] public GameObject panelClave; // Referencia al Canvas donde se muestra la clave
    public GameObject[] trozosDePapel; // Los tres trozos de papel en el juego
    [SerializeField] GameObject tapaCofre;



    void Start()
    {
        canAttack = true;
        rb = GetComponent<Rigidbody>();
        tieneLlaveTrastero = false;

        panelClave.SetActive(false);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            //if (Input.GetKeyDown(KeyCode.F))
            //{
            print("PulsaF");
            tieneLlaveTrastero = true;
            Destroy(other.gameObject);
            //}


        }if (other.gameObject.CompareTag("KeyCofre"))
        {
            //if (Input.GetKeyDown(KeyCode.F))
            //{
            print("PulsaF");
            tieneLlaveCofre = true;
            Destroy(other.gameObject);
            //}


        }

        if (other.gameObject.CompareTag("PuertaTrastero") && tieneLlaveTrastero)
        {
            puertaTrastero.transform.Rotate(0, 90f, 0);

        }

        if (other.gameObject.CompareTag("CofreCandelabro") && tieneLlaveCofre)
        {
             tapaCofre.transform.Rotate(-90f, 0, 0);

        }

        // Detecta la colisión con los trozos de papel
        if (other.CompareTag("TrozosDePapel"))
        {
            TrozosDePapel();
            // Desactiva el trozo de papel recogido

            other.gameObject.SetActive(false);
        }
    }

    public void TrozosDePapel()
    {

        // El jugador recoge un trozo de papel
        trozosDePapelRecolectados++;

        

        // Verifica si se han recogido todos los trozos de papel
        if (trozosDePapelRecolectados == 3)
        {
            
                // Muestra el Canvas con la clave
                //panelClave.SetActive(true);

                            
        }
    }

}
