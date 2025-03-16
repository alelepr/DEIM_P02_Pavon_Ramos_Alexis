using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //refactorizar

    //Patrón Singleton
    public static AudioManager instance;


    [SerializeField] public AudioSource audioSourceBGM;

    //Audio personaje
    [Tooltip("Referencia al Audio Source de los pasos")]
    [SerializeField] private AudioSource footstepsAudioSource;

   
    //Audio Objetos
    [Tooltip("Referencia al Audio Source de las puertas")]
    [SerializeField] private AudioSource objectsAudioSource;


    //Audio música
   
    [Tooltip("Referencia al Audio Source del main menu")]
    [SerializeField] private AudioSource mainMenuAudioSource;

    [Tooltip("Referencia al Audio Source de la victoria")]
    [SerializeField] private AudioSource winAudioSource;

    [Tooltip("Referencia al Audio Source del click")]
    [SerializeField] private AudioSource clickAudioSource;





    //----------------------------------------------
    //AUDIOCLIPS

    
    [Tooltip("Referencia al Audio Clip de las llaves")]
    [SerializeField] private AudioClip keysAudioClip; 
    
    [Tooltip("Referencia al Audio Clip de la puerta")]
    [SerializeField] private AudioClip doorAudioClip;

    [Tooltip("Referencia al Audio Clip de la victoria")]
    [SerializeField] private AudioClip winAudioClip;

    [Tooltip("Referencia al Audio Clip del click")]
    [SerializeField] private AudioClip clickAudioClip;


    private void Awake()
    {
        if (instance == null)
        {
            //guardamos el audio manager en esta misma variable para su uso global

            instance = this;
            //cuando haya cambio de escenas que no se destruya
            //los audiosources tienen que ser hijos de audiomanager para no ser destruidos
            DontDestroyOnLoad(gameObject);
        }
        else if(instance !=this) 
        {
            Destroy(gameObject);
        }
        
    }
    public static void PlayFootStepSound()
    {
        
         //Efecto de sonido para los pasos
         //Si el audio no se esta reproduciendo que lo haga
         if (!instance.footstepsAudioSource.isPlaying)
         {
            //Cambiamos el 
             instance.footstepsAudioSource.pitch = Random.Range(0.9f,1.2f);
             instance.footstepsAudioSource.Play();

         }
        
    }

    
    public static void PlayWinSound()
    {

        if (!instance.winAudioSource.isPlaying)
        {
            instance.winAudioSource.clip = instance.winAudioClip;
            instance.winAudioSource.Play();
        }
    }

    public static void PlayClickSound()
    {

        if (!instance.clickAudioSource.isPlaying)
        {
            instance.clickAudioSource.clip = instance.clickAudioClip;
            instance.clickAudioSource.Play();
        }
    }

   
    public static void PlayDoorSound()
    {

        if (!instance.objectsAudioSource.isPlaying)
        {
            instance.objectsAudioSource.clip = instance.doorAudioClip;
            instance.objectsAudioSource.Play();
        }
    }

    public static void PlayKeySound()
    {

        if (!instance.objectsAudioSource.isPlaying)
        {
            instance.objectsAudioSource.clip = instance.keysAudioClip;
            instance.objectsAudioSource.Play();
        }
    }

   
    public static void PlayBGMMusic()
    {

        if (!instance.audioSourceBGM.isPlaying)
        {
            instance.mainMenuAudioSource.Stop();
            instance.audioSourceBGM.Play();
            
            
        }

    }

    public static void PlayMainMenuMusic()
    {

        if (!instance.mainMenuAudioSource.isPlaying)
        {

            instance.mainMenuAudioSource.Play();
            instance.audioSourceBGM.Stop();


        }

    }

}
