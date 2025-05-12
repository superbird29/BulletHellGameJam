using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
public class EnemyMovementUtil : MonoBehaviour
{
    public List<Vector3> vector3s;
    
    public LineRenderer line;

    public string folderName = "Assets/Enemies/EnemyMovementFlow";

    public string movementName;

    public EnemyBehaviourFlowUtil flowUtil;

    [ContextMenu("Generate Movement Path from line")]
    public void updateV3Array(){
        vector3s.Clear();
        for(int i = 0;i < line.positionCount;i++){
            vector3s.Add(line.GetPosition(i));
        }

        ConvertLineToMovementPathBehaviour();
        movementName = null;
    }

    public void ConvertLineToMovementPathBehaviour(){
        MoveAlongPathBehaviour moveAlongPathBehaviour = new();
        moveAlongPathBehaviour.pathPoints = vector3s;

        if(movementName == null){
            movementName = System.Guid.NewGuid().ToString();
        }

        flowUtil.SaveBehaviourToFolder(moveAlongPathBehaviour, folderName,movementName);
    }
}
#endif
