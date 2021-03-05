using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using RPG.Saving;


namespace RPG.SceneManagement {
public class Portal : MonoBehaviour {

    enum DestinationIdentifier {
        A, B, C, D, E
    }
    [SerializeField] int sceneToLoad = -1;
    [SerializeField] Transform spawnPoint;
    [SerializeField] DestinationIdentifier destination;

    [SerializeField] float fadeOutTime = 2f;
    [SerializeField] float fadeInTime = 2f;
    [SerializeField] float fadeWaitTime = 1f;    

    int currentSceneIndex;

    private void Start() {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") 
        {   
        StartCoroutine(Transition(sceneToLoad));
        }

    }



    private IEnumerator Transition(int nextScene) {

        if (sceneToLoad < 0) {
            Debug.LogError("Scene to load not set.");
            yield break;
        }

        DontDestroyOnLoad(this.gameObject);

        Fader fader = FindObjectOfType<Fader>();
        yield return fader.FadeOut(fadeOutTime);

        //save
        FindObjectOfType<SavingWrapper>().Save();

        yield return SceneManager.LoadSceneAsync(nextScene);

        //load
        FindObjectOfType<SavingWrapper>().Load();
        
        Portal spawnPortal = GetSpawnPortal();
        AdjustPlayerSpawn(spawnPortal);

        FindObjectOfType<SavingWrapper>().Save();

        yield return new WaitForSeconds(fadeWaitTime);
        yield return fader.FadeIn(fadeInTime);

        Destroy(gameObject);
    }

    private Portal GetSpawnPortal()
    {
        foreach(Portal portal in GameObject.FindObjectsOfType<Portal>()) {
            if (portal == this) continue;
            if (portal.destination != destination) continue;

           return portal;
        }
        return null;
    }

    private void AdjustPlayerSpawn(Portal spawnPortal)
    {
        GameObject player = GameObject.FindWithTag("Player");
        
        player.GetComponent<NavMeshAgent>().Warp(spawnPortal.spawnPoint.position) ;
        player.transform.rotation = spawnPortal.spawnPoint.rotation;
    }
}
}