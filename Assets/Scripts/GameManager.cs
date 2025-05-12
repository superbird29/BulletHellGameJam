using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerManager _PlayerManager;
    public RoundManager _RoundManager;

    // This is reference to the Deck script it holds information for the player hand and deck
    public Deck _DeckManager;
    public EnemyManager _EnemyManager;


    private static bool m_ShuttingDown = false;
    private static object m_Lock = new object();
    private static GameManager m_Instance;
    public static GameManager Instance
    {
        get
        {
            if (m_ShuttingDown)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(GameManager) +
                    "' already destroyed. Returning null.");
                return null;
            }
            lock (m_Lock)
            {
                if (m_Instance == null)
                {
                    // Search for existing instance.
                    m_Instance = (GameManager)FindObjectOfType(typeof(GameManager));
                    // Create new instance if one doesn't already exist.
                    if (m_Instance == null)
                    {
                        // Need to create a new GameObject to attach the singleton to.
                        var singletonObject = new GameObject();
                        m_Instance = singletonObject.AddComponent<GameManager>();
                        singletonObject.name = typeof(GameManager).ToString() + " (Singleton)";
                        // Make instance persistent.
                        DontDestroyOnLoad(singletonObject);
                    }
                }
                return m_Instance;
            }
        }
    }

    private void Start()
    {
        SwitchLevel("Title Sceen");
    }


    public void SwitchLevel(string lvlname)
    {
        StartCoroutine(SwitchLevelIE(lvlname));
    }

    public IEnumerator SwitchLevelIE(string lvlname)
    {
        SceneManager.LoadScene(lvlname);
        if (lvlname != "Title Sceen")
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            _PlayerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
            _RoundManager = GameObject.Find("Canvas").GetComponent<RoundManager>();
            _EnemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
            _EnemyManager.Started();
        }
    }

    public void ReloadLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
