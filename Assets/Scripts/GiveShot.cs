using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ND_VariaBULLET;

public class GiveShot : MonoBehaviour
{
    [Header("Shot Controller")]
    [Tooltip("Shot controller we're modifying")]
    public SpreadPattern shotController;

    [Header("Shot Settings")]
    [Tooltip("The shot prefab to use")]
    public GameObject shotPrefab;

    public void AddShot()
    {
        if (shotController != null)
        {
            // Set default emitter type if needed
            shotController.DefaultEmitter = BasePattern.PrefabType.Bullet;
            
            // Increment emitter count by 1
            shotController.EmitterAmount++;

            // Wait a frame for the emitter to be created
            StartCoroutine(ConfigureEmitterNextFrame());
        }
    }

    private IEnumerator ConfigureEmitterNextFrame()
    {
        yield return null; // Wait one frame

        // Get the last emitter added
        int lastIndex = shotController.transform.childCount - 1;
        if (lastIndex >= 0)
        {
            Transform emitter = shotController.transform.GetChild(lastIndex);
            foreach (Transform point in emitter.transform)
            {
                if (point.TryGetComponent<FireBullet>(out var pointComponent))
                {
                    pointComponent.Shot = shotPrefab;
                    break;
                }
            }
        }
    }
}
