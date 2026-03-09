using UnityEngine;

namespace overhealer.Core
{
    public class PlayerService :
        Service
    {
        public GameObject Player { get; private set; }

        public void SpawnPlayer(GameObject playerPrefab)
        {
            var player = GameInstance.CreateObject(playerPrefab, Vector3.zero, Vector3.zero);
            Player = player;
        }
    }
}