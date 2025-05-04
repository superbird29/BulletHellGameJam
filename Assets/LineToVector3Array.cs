using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineToVector3Array : MonoBehaviour
{
    public List<Vector3> vector3s;
    
    public LineRenderer line;

    [ContextMenu("Generate Points")]
    public void updateV3Array(){
        for(int i = 0;i< line.positionCount;i++){
        vector3s.Add(line.GetPosition(i));
        }
    }
}
