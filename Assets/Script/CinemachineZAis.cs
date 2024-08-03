using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[AddComponentMenu("")]
[ExecuteAlways]
[SaveDuringPlay]
public class CinemachineZToDutch : CinemachineExtension
{

    public CinemachineCore.Stage m_appyAfter = CinemachineCore.Stage.Aim;

    public Transform playerTransform;
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == m_appyAfter)
        {
            float playerZRotation = playerTransform.eulerAngles.z;

            // Set the Dutch (tilt) angle to match the player's Z-axis rotation
            state.Lens.Dutch = playerZRotation;
        }

    }
    // Start is called before the first frame update
    
}
