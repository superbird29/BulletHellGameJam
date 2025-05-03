using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitcher : MonoBehaviour
{
    //switches scene with level name
    void SwitchLevel(string lvlname)
    {
        SceneManager.LoadScene(lvlname);
    }
}
