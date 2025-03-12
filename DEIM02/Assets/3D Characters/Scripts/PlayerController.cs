using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] public Animator anim;
    private bool moving;
    Rigidbody rb;

    // Cámara
    [SerializeField] Transform cam;

    // Llaves
    [SerializeField] GameObject llaveTrastero;
    [SerializeField] GameObject llaveCofre;
    [SerializeField] GameObject llaveHabitacion;

    public bool colisionaConObjeto;

    // ComprobantesLlave
    public bool tieneLlaveTrastero;
    public bool tieneLlaveCofre;
    public bool tieneLlaveHabitacion;

    // Puertas
    [SerializeField] GameObject puertaTrastero;
    [SerializeField] GameObject puertaHabitacion;
    [SerializeField] GameObject tapaCofre;

    // ComprobantesPuertas
    public bool isOpened;
    public bool isOpenedHab;
    public bool isChestOpened;

    // Datos de apertura de puertas y cofre
    public float openAngle = 90f;
    public float openSpeed = 0.4f;

    private GameObject objetoColisionado;

    // TrozosDeMapa
    public GameObject[] trozosDePapel; // Los tres trozos de papel en el juego
    public int trozosDePapelRecolectados = 0;

    // Interfaz
    [SerializeField] public GameObject mapPanel;
    [SerializeField] public GameObject mLetter;
    [SerializeField] public GameObject buttonPickUp;
    [SerializeField] public GameObject buttonOpen;
    [SerializeField] public GameObject panelBotonNPC; // Panel del botón del NPC
    [SerializeField] public GameObject panelNPC; // Panel del NPC
    public bool panelNPCActivado; // Si el panel está activado o no
    private bool cercaDelNpc = false; // Si el jugador está cerca del NPC
    public bool npcPuedeMoverse; // Si el panel ya ha sido cerrado
    private bool panelBotonNPCActivado = false; // Para asegurarse de que el panelBotonNPC solo aparezca una vez

    // Movimiento
    float horInput;
    float verInput;

    // Vela
    public GameObject vela;

    GameManager gameManager;

    public bool mapaActivo;

    // Raycast
    public Transform raycastPos;
    public float distance;
    public LayerMask raycastMask;
    public float offset;

    void Start()
    {
        moving = false;
        isOpened = false;
        rb = GetComponent<Rigidbody>();
        tieneLlaveTrastero = false;
        vela.SetActive(false);
        mapPanel.SetActive(false);
        anim = GetComponent<Animator>();
        speed = 6;
        panelBotonNPC.SetActive(false); // Asegurarse de que el panel del botón NPC esté desactivado al inicio
        npcPuedeMoverse = false;
    }

    void Update()
    {
        PlayerMovement();

        RaycastHit hit;
        if (Physics.Raycast(raycastPos.position, Vector3.down, out hit, distance, raycastMask))
        {
            transform.position = hit.point + Vector3.up * offset;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            mapaActivo = !mapaActivo;
            mapPanel.SetActive(mapaActivo);
        }

        // Activar panelBotonNPC cuando el jugador está cerca del NPC y solo una vez
        if (cercaDelNpc && !panelBotonNPCActivado)
        {
            panelBotonNPC.SetActive(true);
            panelBotonNPCActivado = true; // Evitar que se active más de una vez
        }

        if (Input.GetKeyDown(KeyCode.F) && cercaDelNpc && !panelNPCActivado)
        {
            panelBotonNPC.SetActive(false); // Desactivar panelBotonNPC
            panelNPC.SetActive(true); // Activar el panel del NPC
            panelNPCActivado = true; // El panel está activado
            speed = 0; // Desactiva la velocidad cuando el panel está activo
            npcPuedeMoverse = false; // El panel no está cerrado
        }
        else if (Input.GetKeyDown(KeyCode.F) && cercaDelNpc && panelNPCActivado) // Si está cerca y el panel está activado
        {
            panelNPC.SetActive(false); // Desactivar el panel NPC
            panelNPCActivado = false; // El panel ya no está activado
            speed = 6; // Restaura la velocidad cuando el panel se cierra
            npcPuedeMoverse = true; // El panel está cerrado
        }

        // Lógica de otras interacciones, como recoger objetos, abrir puertas, etc.
        if (Input.GetKeyDown(KeyCode.F) && colisionaConObjeto)
        {
            switch (objetoColisionado.tag)
            {
                case "Key":
                    buttonPickUp.SetActive(false);
                    colisionaConObjeto = false;
                    tieneLlaveTrastero = true;
                    Destroy(llaveTrastero);
                    break;

                case "KeyCofre":
                    buttonPickUp.SetActive(false);
                    colisionaConObjeto = false;
                    tieneLlaveCofre = true;
                    Destroy(llaveCofre);
                    break;

                case "LlaveHabitacion":
                    buttonPickUp.SetActive(false);
                    colisionaConObjeto = false;
                    tieneLlaveHabitacion = true;
                    Destroy(llaveHabitacion);
                    break;

                case "PuertaTrastero":
                    colisionaConObjeto = false;
                    ActivarGiro();
                    buttonOpen.SetActive(false);
                    break;

                case "CofreCandelabro":
                    buttonOpen.SetActive(false);
                    colisionaConObjeto = false;
                    AbrirCofre();
                    break;

                case "PuertaHabitacion":
                    buttonOpen.SetActive(false);
                    colisionaConObjeto = false;
                    ActivarGiroHab();
                    break;

                case "TrozosDePapel":
                    buttonPickUp.SetActive(false);
                    colisionaConObjeto = false;
                    TrozosDePapel();
                    Destroy(objetoColisionado);
                    break;
            }
        }
    }

    private void PlayerMovement()
    {
        if (panelNPCActivado) return; // Si el panel está activo, no permite mover al jugador

        horInput = Input.GetAxisRaw("Horizontal") * speed;
        verInput = Input.GetAxisRaw("Vertical") * speed;

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

        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        camForward.y = 0;
        camRight.y = 0;

        Vector3 forwardRelative = verInput * camForward;
        Vector3 rightRelative = horInput * camRight;

        Vector3 moveDir = forwardRelative + rightRelative;

        rb.linearVelocity = new Vector3(moveDir.x, rb.linearVelocity.y, moveDir.z);

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
        if (other.CompareTag("NPC"))
        {
            cercaDelNpc = true; // El jugador entra en la zona del NPC
        }

        // Lógica para otros objetos que el jugador puede recoger o interactuar
        if (other.CompareTag("Key") || other.CompareTag("KeyCofre") || other.CompareTag("LlaveHabitacion"))
        {
            colisionaConObjeto = true;
            objetoColisionado = other.gameObject;
            buttonPickUp.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            cercaDelNpc = false; // El jugador sale de la zona del NPC
            if (panelNPCActivado)
            {
                panelNPC.SetActive(false); // Cierra el panel al salir
                panelNPCActivado = false;
                speed = 10; // Restaura la velocidad al salir
                npcPuedeMoverse = true; // El panel se cierra
            }
            if (panelBotonNPCActivado)
            {
                panelBotonNPC.SetActive(false); // Si el jugador sale del área, desactivamos el panelBotonNPC
                panelBotonNPCActivado = false; // Aseguramos que no aparezca de nuevo
            }
        }
    }

    public void TrozosDePapel()
    {
        trozosDePapelRecolectados++;

        if (trozosDePapelRecolectados == 3)
        {
            mLetter.SetActive(true);
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
    }
}
