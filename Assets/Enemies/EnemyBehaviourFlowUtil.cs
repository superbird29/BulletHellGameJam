using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class EnemyBehaviourFlowUtil : MonoBehaviour
{
    [SerializeField] List<BaseMovementBehaviour> movementBehaviours;

    [SerializeField] String movementFolderPath = "Assets/Enemies/Enemy Behaviour Stuff/EnemyMovementFlow";

    [SerializeField] String currentMovementFlowFolderName;

    [SerializeField] List<BaseAimingBehaviour> aimingBehaviours;

    [SerializeField] String aimingFolderPath = "Assets/Enemies/Enemy Behaviour Stuff/EnemyAimingFlow";

    [SerializeField] String currentAimingFlowFolderName;

    [SerializeField] List<BaseFiringBehaviour> firingBehaviours;

    [SerializeField] String firingFolderPath = "Assets/Enemies/Enemy Behaviour Stuff/EnemyFiringFlow";

    [SerializeField] String currentFiringFlowFolderName;

    [SerializeField] String compositeStateFolderPath = "Assets/Enemies/Enemy Behaviour Stuff/EnemyCompositeStates";

    [SerializeField] String currentCompositeStateFileName;

    [ContextMenu("Save Movement Flow to folder")]
    public void LinkMovementBehaviours()
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

    [ContextMenu("Save Aiming Flow to folder")]
    public void LinkAimingBehaviours()
    {
        if (aimingBehaviours.Count == 0) return;

        BaseAimingBehaviour copyOfBehaviour = Instantiate(aimingBehaviours[0]);
        aimingBehaviours[0] = copyOfBehaviour;
        for (int i = 1; i < aimingBehaviours.Count; i++)
        {
            copyOfBehaviour = Instantiate(aimingBehaviours[i]);
            aimingBehaviours[i] = copyOfBehaviour;
            aimingBehaviours[i - 1].nextBehaviour = aimingBehaviours[i];
        }

        SaveBehavioursToFolder(aimingBehaviours);
    }

    [ContextMenu("Save Firing Flow to folder")]
    public void LinkFiringBehaviours()
    {
        if (firingBehaviours.Count == 0) return;

        BaseFiringBehaviour copyOfBehaviour = Instantiate(firingBehaviours[0]);
        firingBehaviours[0] = copyOfBehaviour;
        for (int i = 1; i < firingBehaviours.Count; i++)
        {
            copyOfBehaviour = Instantiate(firingBehaviours[i]);
            firingBehaviours[i] = copyOfBehaviour;
            firingBehaviours[i - 1].nextBehaviour = firingBehaviours[i];
        }

        SaveBehavioursToFolder(firingBehaviours);
    }

    [ContextMenu("Create Composite Behaviour State From Flows")]
    public void SaveCompositeState()
    {
        LinkMovementBehaviours();
        LinkAimingBehaviours();
        LinkFiringBehaviours();
        EnemyCompositeBehaviourState state = new();
        state.startingAimingBehaviour = aimingBehaviours.Count > 0 ? aimingBehaviours[0] : null;
        state.startingFiringBehaviour = firingBehaviours.Count > 0 ? firingBehaviours[0] : null;
        state.startingMovementBehaviour = movementBehaviours.Count > 0 ? movementBehaviours[0] : null;

        if (currentCompositeStateFileName.Length == 0)
        {
            currentCompositeStateFileName = GetRandomName();
        }

        if (!AssetDatabase.IsValidFolder(compositeStateFolderPath))
        {
            Directory.CreateDirectory(compositeStateFolderPath);
        }

        string path = Path.Combine(compositeStateFolderPath, $"{currentCompositeStateFileName}_CompositeState.asset");
        AssetDatabase.CreateAsset(state, path);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Saved Behaviours to " + path);
        currentCompositeStateFileName = null;

    }

    public void SaveBehavioursToFolder(List<BaseMovementBehaviour> behaviours)
    {
        if (currentMovementFlowFolderName.Length == 0)
        {
            currentMovementFlowFolderName = GetRandomName();
        }

        String fullFolderPath = movementFolderPath + "/" + currentMovementFlowFolderName;
        // Ensure folder exists
        if (!AssetDatabase.IsValidFolder(fullFolderPath))
        {
            Directory.CreateDirectory(fullFolderPath);
        }
        Debug.Log(fullFolderPath);
        Debug.Log(currentMovementFlowFolderName);
        // Save each object as an asset
        for (int i = 0; i < behaviours.Count; i++)
        {
            string path = Path.Combine(fullFolderPath, $"{i}_Behaviour.asset");
            AssetDatabase.CreateAsset(behaviours[i], path);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        List<BaseMovementBehaviour> loadedBehaviours = new List<BaseMovementBehaviour>();
        for (int i = 0; i < behaviours.Count; i++)
        {
            string assetPath = Path.Combine(fullFolderPath, $"{i}_Behaviour.asset");
            var loaded = AssetDatabase.LoadAssetAtPath<BaseMovementBehaviour>(assetPath);
            if (loaded != null)
                loadedBehaviours.Add(loaded);
        }

        movementBehaviours = loadedBehaviours;

        Debug.Log("Saved Behaviours to " + fullFolderPath);
        currentMovementFlowFolderName = null;
    }
    public void SaveBehavioursToFolder(List<BaseAimingBehaviour> behaviours)
    {
        Debug.Log(currentAimingFlowFolderName.Length == 0);
        if (currentAimingFlowFolderName.Length == 0)
        {
            currentAimingFlowFolderName = GetRandomName();

        }
        String fullFolderPath = aimingFolderPath + "/" + currentAimingFlowFolderName;
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

        List<BaseAimingBehaviour> loadedBehaviours = new List<BaseAimingBehaviour>();
        for (int i = 0; i < behaviours.Count; i++)
        {
            string assetPath = Path.Combine(fullFolderPath, $"{i}_Behaviour.asset");
            var loaded = AssetDatabase.LoadAssetAtPath<BaseAimingBehaviour>(assetPath);
            if (loaded != null)
                loadedBehaviours.Add(loaded);
        }

        aimingBehaviours = loadedBehaviours;

        Debug.Log("Saved Behaviours to " + fullFolderPath);
        currentAimingFlowFolderName = null;
    }

    private String GetRandomName()
    {
        Debug.Log("HEY!");
        Guid myGuid = System.Guid.NewGuid();
        Debug.Log(myGuid);

        return myGuid.ToString().Substring(25);
    }

    public void SaveBehavioursToFolder(List<BaseFiringBehaviour> behaviours)
    {
        if (currentFiringFlowFolderName.Length == 0)
        {

            currentFiringFlowFolderName = GetRandomName();

        }
        String fullFolderPath = firingFolderPath + "/" + currentFiringFlowFolderName;
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

        List<BaseFiringBehaviour> loadedBehaviours = new List<BaseFiringBehaviour>();
        for (int i = 0; i < behaviours.Count; i++)
        {
            string assetPath = Path.Combine(fullFolderPath, $"{i}_Behaviour.asset");
            var loaded = AssetDatabase.LoadAssetAtPath<BaseFiringBehaviour>(assetPath);
            if (loaded != null)
                loadedBehaviours.Add(loaded);
        }

        firingBehaviours = loadedBehaviours;

        Debug.Log("Saved Behaviours to " + fullFolderPath);
        currentFiringFlowFolderName = null;
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
