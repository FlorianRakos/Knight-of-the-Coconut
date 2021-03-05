using UnityEngine;

namespace RPG.Core {

public class PersistentObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject persistentObjectPrefab;

    static bool hasSpawned = false;

    private void Awake() {
        if(hasSpawned) return;
        SpawnPersistentObjects();
        hasSpawned = true;
    }

    private void SpawnPersistentObjects()
    {
        GameObject persistentObject = Instantiate(persistentObjectPrefab, new Vector3(0f,0f,0f), Quaternion.identity);
        DontDestroyOnLoad(persistentObject);
    }

}
}