using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class EnemyBehaviourFlowUtil : MonoBehaviour
{
    [SerializeField] List<BaseMovementBehaviour> movementBehaviours;

    [SerializeField] string folderPath = "Assets/Enemies/EnemyMovementFlow";

    [SerializeField] string currentFlowFolderName = "default";

    [ContextMenu("Save Movement Flow to folder")]
    public void LinkBehaviours()
    {
        if (movementBehaviours.Count == 0) return;

        BaseMovementBehaviour copyOfBehaviour = Instantiate(movementBehaviours[0]);
        movementBehaviours[0] = copyOfBehaviour;
        for (int i = 1; i < movementBehaviours.Count; i++)
        {
            copyOfBehaviour = Instantiate(movementBehaviours[i]);
            movementBehaviours[i] = copyOfBehaviour;
            movementBehaviours[i - 1].nextBehaviour = movementBehaviours[i];
        }

        SaveBehavioursToFolder(movementBehaviours);
    }

    public void SaveBehavioursToFolder(List<BaseMovementBehaviour> behaviours)
    {
        
        String fullFolderPath = folderPath + "/" + currentFlowFolderName;
        // Ensure folder exists
        if (!AssetDatabase.IsValidFolder(fullFolderPath))
        {
            Directory.CreateDirectory(fullFolderPath);
        }

        // Save each object as an asset
        for (int i = 0; i < behaviours.Count; i++)
        {
            string path = Path.Combine(fullFolderPath, $"{i}_Behaviour.asset");
            AssetDatabase.CreateAsset(behaviours[i], path);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Saved Behaviours to " + fullFolderPath);
    }

    public void SaveBehaviourToFolder(EnemyBehaviour behaviour, string folder, string fileName)
    {
        
        String fullFolderPath = folder;
        // Ensure folder exists
        if (!AssetDatabase.IsValidFolder(fullFolderPath))
        {
            Directory.CreateDirectory(fullFolderPath);
        }

        string path = Path.Combine(fullFolderPath, $"{fileName}.asset");
        AssetDatabase.CreateAsset(behaviour, path);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Saved Behaviours to " + fullFolderPath);
    }
}
