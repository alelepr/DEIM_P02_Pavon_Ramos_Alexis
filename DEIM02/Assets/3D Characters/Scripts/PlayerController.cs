using UnityEngine;
using System.Collections;
using JetBrains.Annotations;


public class PlayerController : MonoBehaviour
{

    [SerializeField]  float speed;
    //[SerializeField] private Transform playerPointer;
    [SerializeField] private Animator anim;
    Rigidbody rb;
    [SerializeField] Transform cam;

    private bool moving;
    private bool canAttack;
    public bool puedeCogerLlaveTrastero;
    [SerializeField] GameObject llaveTrastero;
    [SerializeField] GameObject llaveCofre;
    public bool colisionaConObjeto;

    //PUZZLE 2

    public bool tieneLlaveTrastero;
    public bool tieneLlaveCofre;
    [SerializeField] GameObject puertaTrastero;
    

    //PUZZLE 3
    public int trozosDePapelRecolectados = 0; // Cuenta los trozos de papel recolectados
    [SerializeField] public GameObject panelClave; // Referencia al Canvas donde se muestra la clave
    public GameObject[] trozosDePapel; // Los tres trozos de papel en el juego
    [SerializeField] GameObject tapaCofre;
    public bool isOpened;
    public bool isChestOpened;
    public float openAngle = 90f;
    public float openSpeed = 0.4f;

    private GameObject objetoColisionado; // Declaramos una variable para guardar el GameObject

    public GameObject vela;
    public bool chestOpened;




    void Start()
    {
        isOpened = false;
        canAttack = true;
        rb = GetComponent<Rigidbody>();
        tieneLlaveTrastero = false;
        vela.SetActive(false);
        panelClave.SetActive(false);
    }

    void Update()
    {
        PlayerMovement();

        if (Input.GetKeyDown(KeyCode.F) && colisionaConObjeto)
        {
            switch (objetoColisionado.tag)
            {
                case "Key":

                    colisionaConObjeto = false;
                    tieneLlaveTrastero = true;
                    Destroy(llaveTrastero);
                    break; 
                
                case "KeyCofre":

                    colisionaConObjeto = false;
                    tieneLlaveCofre = true;
                    Destroy(llaveCofre);
                    break;
            }
                

        }

    }

    private void PlayerMovement()
    {
        moving = false;

        // Inputs
        float horInput = Input.GetAxisRaw("Horizontal") * speed;
        float verInput = Input.GetAxisRaw("Vertical") * speed;

        // Dirección de la cámara
        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        camForward.y = 0;
        camRight.y = 0;

        // Crear dirección relativa a la cámara
        Vector3 forwardRelative = verInput * camForward;
        Vector3 rightRelative = horInput * camRight;

        Vector3 moveDir = forwardRelative + rightRelative;

        // Movimiento
        rb.linearVelocity = new Vector3(moveDir.x, rb.linearVelocity.y, moveDir.z);

        // Mantener la última dirección si el jugador deja de moverse respetando solo el eje Y
        if (moveDir.magnitude > 0)
        {
            Vector3 direction = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z).normalized;
            if (direction.magnitude > 0)
            {
                transform.rotation = Quaternion.Euler(0, Quaternion.LookRotation(direction).eulerAngles.y, 0);
            }
        }
    }


    public void AttackEnded()
    {

        canAttack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            colisionaConObjeto = true;
            objetoColisionado = other.gameObject; // Guardamos el GameObject que colisionó


        }

        if (other.gameObject.CompareTag("KeyCofre"))
        {
            colisionaConObjeto = true;
            objetoColisionado = other.gameObject; // Guardamos el GameObject que colisionó


        }

        if (other.gameObject.CompareTag("PuertaTrastero") && tieneLlaveTrastero)
        {
            ActivarGiro();

        }

        if (other.gameObject.CompareTag("CofreCandelabro") && tieneLlaveCofre)
        {
             AbrirCofre();

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
    public void ActivarGiro()
    {
        if (isOpened == false)
        {
            StartCoroutine(OpenDoor());

        }

    }public void AbrirCofre()
    {
        if (isChestOpened == false)
        {
            StartCoroutine(OpenChest());

        }

    }


    public IEnumerator OpenDoor()
    {
        isOpened = true;
        Quaternion startRotation = puertaTrastero.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, openAngle, 0) * startRotation;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            puertaTrastero.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime);
            elapsedTime += Time.deltaTime * openSpeed;
            yield return null;
        }

        puertaTrastero.transform.rotation = targetRotation;

    }public IEnumerator OpenChest()
    {
        isChestOpened = true;
        Quaternion startRotation = tapaCofre.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(openAngle, 0, 0) * startRotation;
        float elapsedTime = 0f;
        

        while (elapsedTime < 1f)
        {
            tapaCofre.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime);
            elapsedTime += Time.deltaTime * openSpeed;
            vela.SetActive(true);
            yield return null;
        }

        tapaCofre.transform.rotation = targetRotation;
        chestOpened = true;

        if (chestOpened)
        {
            
        }
    }

        


}
