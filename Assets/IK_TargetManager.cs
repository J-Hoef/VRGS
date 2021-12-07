using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class IK_TargetManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _ikTargets;
    
    private void Awake()
    {
        
    }

    void SetTarget()
    {
        
    }
    
    void Update()
    {
        
    }
}

public class TargetBehaviour : MonoBehaviour
{
    [SerializeField] private TargetData _targetDataData;
    private void Awake()
    {
        
    }
    ///When no procedural targets found, grip targets
}

public enum TargetStates
{
    idle,
    grip,
    procedural
}

public class TargetData : ScriptableObject
{
    [SerializeField] private List<Vector3>_positions;
    [SerializeField] private TargetStates _targetStates;
}
