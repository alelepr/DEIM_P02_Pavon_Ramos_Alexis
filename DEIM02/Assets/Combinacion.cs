using UnityEngine;
using UnityEngine.UI;

public class Combinacion : MonoBehaviour
{
    // Variables para el puzzle
    [SerializeField] public GameObject cajaFuerte;  // Caja fuerte en el juego
    [SerializeField] public GameObject canvasCombinacion;  // Canvas que contiene el panel con los botones de la combinaci�n
    [SerializeField] public GameObject linterna;  // El objeto (linterna) que el jugador obtendr� al abrir la caja fuerte
    [SerializeField] public GameObject tapaCofre;

    private string combinacionCorrecta = "4586";  // La combinaci�n correcta
    private string combinacionIngresada = "";  // La combinaci�n que el jugador ingresa
    private bool combinacionCorrectaFlag = false;  // Verifica si la combinaci�n ingresada es correcta

    void Start()
    {
        // Asegurarse de que la linterna est� desactivada al inicio
        linterna.SetActive(false);
        // Asegurarse de que el canvas de la combinaci�n est� desactivado al inicio
        canvasCombinacion.SetActive(false);
    }

    // Este m�todo se llama cuando el jugador interact�a con la caja fuerte
    public void AbrirCajaFuerte()
    {
        // Muestra el canvas de la combinaci�n
        canvasCombinacion.SetActive(true);
    }

    // M�todo para ingresar el n�mero en la combinaci�n
    public void IngresarNumero(string numero)
    {
        // A�adir el n�mero al final de la combinaci�n ingresada
        combinacionIngresada += numero;
        Debug.Log("Combinaci�n Ingresada: " + combinacionIngresada);
    }

    // M�todo para verificar si la combinaci�n ingresada es correcta
    public void VerificarCombinacion()
    {
        // Verifica si la combinaci�n es correcta
        if (combinacionIngresada == combinacionCorrecta)
        {
            combinacionCorrectaFlag = true;
            AbrirCajaFuerteAccion();  // Abre la caja fuerte
        }
        else
        {
            // Si la combinaci�n es incorrecta, reinicia el ingreso
            combinacionIngresada = "";
            Debug.Log("Combinaci�n incorrecta.");
        }
    }

    // Acciones para abrir la caja fuerte cuando la combinaci�n es correcta
    private void AbrirCajaFuerteAccion()
    {
        if (combinacionCorrectaFlag)
        {
            // Desactivar el canvas de la combinaci�n
            canvasCombinacion.SetActive(false);

            // Aqu� activamos la linterna (o cualquier objeto que el jugador deba obtener)
            linterna.SetActive(true);

            // Si deseas reproducir una animaci�n de la caja fuerte abri�ndose, tambi�n podr�as hacerla aqu�.
            Debug.Log("�Combinaci�n correcta! La caja fuerte se ha abierto.");
        }
    }

    // M�todo para cancelar el ingreso de la combinaci�n (puedes agregar un bot�n para esto)
    public void CancelarIngreso()
    {
        combinacionIngresada = "";  // Reinicia la combinaci�n ingresada
        Debug.Log("Ingreso cancelado.");
    }
}