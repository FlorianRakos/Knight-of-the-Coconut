using UnityEngine;
using UnityEngine.UI;
using System.Collections;



namespace RPG.SceneManagement {

    public class Fader : MonoBehaviour 
    {
        [SerializeField] float fadeTime = 2f;

        CanvasGroup canvasGroup;


        private void Awake() {
            canvasGroup = GetComponent<CanvasGroup>();

        }

        public void FadeOutImediate () {
            canvasGroup.alpha =1f;
        }


        public IEnumerator FadeOut(float time) 
        {
            while (canvasGroup.alpha < 1f) {
                
                canvasGroup.alpha += (Time.deltaTime / time);
                yield return null;
            }               
        }

        public IEnumerator FadeIn(float time) 
        {
            while (canvasGroup.alpha > 0f) {
                
                canvasGroup.alpha -= (Time.deltaTime / time);
                yield return null;
            }               
        }

        
        
    }
    }