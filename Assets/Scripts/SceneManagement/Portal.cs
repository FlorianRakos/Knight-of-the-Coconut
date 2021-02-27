using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {

    [SerializeField] bool isLevelExit = true;

    int currentSceneIndex;

    private void Start() {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            if(isLevelExit)
            {
                LoadNextScene();
            }
            else
            {
                LoadLastScene();
            }
        }

    }

    private void LoadLastScene()
    {
        SceneManager.LoadScene(currentSceneIndex - 1);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}