/*//using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


using SceneManagerUnity = UnityEngine.SceneManagement.SceneManager;

namespace GestionEscenas
{
    public class SceneManager : MonoBehaviour
    {   
        
        
        public static SceneManager instance; //Instancia del patrón Singleton
        [SerializeField] private CanvasGroup fade; //Creamos una variable referente al canvas group que gestinará su propio alpha y el de sus hijos
        [SerializeField] private float fadeDuration;
        public bool isLoadingScene;



        private void Awake() //Obligatorio en el patrón singleton junto con la instancia de la propia clase en la que estamos trabajando
        {
            if (instance == null)
            {
                //guardamos el audio manager en esta misma variable para su uso global

                instance = this;
                //cuando haya cambio de escenas que no se destruya
                //los audiosources tienen que ser hijos de audiomanager para no ser destruidos
                DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }

        }


       



        public static void LoadScene(string sceneName)
        {
            if (!instance.isLoadingScene)
            {
                instance.StartCoroutine(instance.LoadSceneCo(sceneName));

            }



        }

        /*private IEnumerator LoadSceneCo(string sceneName)
        {
            isLoadingScene = true;
            yield return instance.fade.DOFade(1, fadeDuration).SetEase(Ease.Linear).WaitForCompletion();

            AsyncOperation loadOp = SceneManagerUnity.LoadSceneAsync(sceneName); //referencia asincrona al cargado de escena
            while (!loadOp.isDone) {

                yield return null; //Espera al siguiente frame

            }

            instance.fade.DOFade(0, fadeDuration).SetEase(Ease.Linear);
            isLoadingScene = false;
        }


        public static Scene GetActiveScene() {

            return SceneManagerUnity.GetActiveScene();

        }

        





    }

}*/
