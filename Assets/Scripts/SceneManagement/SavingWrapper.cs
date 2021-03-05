using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;

namespace RPG.SceneManagement {
public class SavingWrapper : MonoBehaviour
{
    [SerializeField] float fadeInTime = .5f;
    const string defaultSaveFile = "save";

    IEnumerator Start() {
        yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
        print("loading scene");
        FindObjectOfType<Fader>().FadeOutImediate();
        yield return FindObjectOfType<Fader>().FadeIn(fadeInTime);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
        
        if(Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
    }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        public void Load()
        {
        GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

}
}
