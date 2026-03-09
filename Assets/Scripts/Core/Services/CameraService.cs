using UnityEngine;

namespace overhealer.Core
{
    public class CameraService :
        Service
    {
        public Camera CurrentCamera { get; private set; }

        public void SpawnCamera(GameObject cameraPrefab)
        {
            var cameraContainer = GameInstance.CreateObject(cameraPrefab, Vector3.zero, Vector3.zero);

            CurrentCamera = cameraContainer.GetComponentInChildren<Camera>();
        }
    }
}