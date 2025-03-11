using UnityEngine;
using System.Collections;
using JetBrains.Annotations;


public class PlayerController : MonoBehaviour
{

    [SerializeField]  float speed;
    [SerializeField] public Animator anim;
    Rigidbody rb;
    [SerializeField] Transform cam;

    private bool moving;

    public bool puedeCogerLlaveTrastero;
    [SerializeField] GameObject llaveTrastero;
    [SerializeField] GameObject llaveCofre;
    [SerializeField] GameObject llaveHabitacion;
    public bool colisionaConObjeto;

   

    public bool tieneLlaveTrastero;
    public bool tieneLlaveCofre;
    public bool tieneLlaveHabitacion;
    [SerializeField] GameObject puertaTrastero;
    [SerializeField] GameObject puertaHabitacion;
    

    //PUZZLE 3
    public int trozosDePapelRecolectados = 0; // Cuenta los trozos de papel recolectados
    [SerializeField] public GameObject mapPanel; // Referencia al Canvas donde se muestra la clave
    public GameObject[] trozosDePapel; // Los tres trozos de papel en el juego
    [SerializeField] GameObject tapaCofre;
    public bool isOpened;
    public bool isOpenedHab;
    public bool isChestOpened;
    public float openAngle = 90f;
    public float openSpeed = 0.4f;

    float horInput;
    float verInput;

    private GameObject objetoColisionado; // Declaramos una variable para guardar el GameObject

    public GameObject vela;
    public bool chestOpened;

    GameManager gameManager;

    public bool mapaActivo;





    void Start()
    {
        moving = false;
        isOpened = false;
        rb = GetComponent<Rigidbody>();
        tieneLlaveTrastero = false;
        vela.SetActive(false);
        mapPanel.SetActive(false);
        anim = GetComponent<Animator>();
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
                
               case "LlaveHabitacion":
                    
                    colisionaConObjeto = false;
                    tieneLlaveHabitacion = true;
                    Destroy(llaveHabitacion);
                    break;
                
               case "PuertaTrastero":
                    
                    colisionaConObjeto = false;
                    ActivarGiro();
                    break;
                
                case "CofreCandelabro":
                    
                    colisionaConObjeto = false;
                    AbrirCofre();
                    break;

                case "PuertaHabitacion":
                    
                    colisionaConObjeto = false;
                    ActivarGiroHab();
                    break;
            }
                

        }

        if (Input.GetKeyDown(KeyCode.M) && mapaActivo)
        {
            gameManager.mapPanelUp();
        }

    }

    private void PlayerMovement()
    {
        // Obtener las entradas de movimiento
         horInput = Input.GetAxisRaw("Horizontal") * speed;
         verInput = Input.GetAxisRaw("Vertical") * speed;

        // Si el jugador tiene alguna entrada de movimiento, establece 'moving' en true
        if (horInput != 0 || verInput != 0)
        {
            moving = true;
            anim.SetBool("moving", moving);
        }
        else
        {
            moving = false;
            anim.SetBool("moving", moving);
        }

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


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            colisionaConObjeto = true;
            objetoColisionado = other.gameObject; // Guardamos el GameObject con el que colisiona


        }

        if (other.gameObject.CompareTag("KeyCofre"))
        {
            colisionaConObjeto = true;
            objetoColisionado = other.gameObject; 


        }
        
        if (other.gameObject.CompareTag("LlaveHabitacion"))
        {
            colisionaConObjeto = true;
            objetoColisionado = other.gameObject; 


        }

        if (other.gameObject.CompareTag("PuertaTrastero") && tieneLlaveTrastero)
        {
            //ActivarGiro();
            colisionaConObjeto = true;
            objetoColisionado = other.gameObject;

        }
        
        if (other.gameObject.CompareTag("PuertaHabitacion") && tieneLlaveHabitacion)
        {
            ActivarGiroHab();
            colisionaConObjeto = true;
            objetoColisionado = other.gameObject;

        }

        if (other.gameObject.CompareTag("CofreCandelabro") && tieneLlaveCofre)
        {
             
            colisionaConObjeto = true;
            objetoColisionado = other.gameObject;

        }

        // Detecta la colisión con los trozos de papel
        if (other.CompareTag("TrozosDePapel"))
        {
            TrozosDePapel();
            // Desactiva el trozo de papel recogido

            Destroy(other.gameObject);
        }
    }

    public void TrozosDePapel()
    {

        // El jugador recoge un trozo de papel
        trozosDePapelRecolectados++;

        
        // Verifica si se han recogido todos los trozos de papel
        if (trozosDePapelRecolectados == 3)
        {
            //gameManager.mapPanelUp();
            mapaActivo = true;
            llaveCofre.SetActive(true);

            
        }
    }
    public void ActivarGiro()
    {
        if (isOpened == false)
        {
            StartCoroutine(OpenDoor());

        }
    }
    
    public void ActivarGiroHab()
    {
        if (isOpenedHab == false)
        {
            StartCoroutine(OpenDoorHab());

        }
    }
    
    public void AbrirCofre()
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

    }
    
    public IEnumerator OpenDoorHab()
    {
        isOpenedHab = true;
        Quaternion startRotation = puertaHabitacion.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, -openAngle, 0) * startRotation;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            puertaHabitacion.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime);
            elapsedTime += Time.deltaTime * openSpeed;
            yield return null;
        }

        puertaHabitacion.transform.rotation = targetRotation;

    }
    
    public IEnumerator OpenChest()
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
            llaveHabitacion.SetActive(true);
            yield return null;
        }

        tapaCofre.transform.rotation = targetRotation;
        chestOpened = true;

        
    }

        


}
