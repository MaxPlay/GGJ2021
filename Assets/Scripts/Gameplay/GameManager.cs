using GGJ.Levels;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace GGJ.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField]
        private LevelData levelData;

        [SerializeField]
        private ShipSpawnConfiguration shipConfiguration;

        public UnityEvent<WinningState> OnGameOver;

        public int SpawnedShips { get; private set; }

        public int ReachedShips { get; set; }

        public int ReachedChests { get; set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            StartCoroutine(ShipSpawnRoutine());
        }

        public bool SpawnShip()
        {
            Debug.Assert(levelData, "No level data assigned");
            if (SpawnedShips >= levelData.ShipCount)
                return false;

            Ship ship = Instantiate(shipConfiguration.ShipPrefab, shipConfiguration.GetSpawnLocation(), Quaternion.LookRotation(shipConfiguration.MoveDirection));
            ship.DoSpawnAnimation();
            ++SpawnedShips;
            return true;
        }

        private IEnumerator ShipSpawnRoutine()
        {
            while (SpawnShip())
                yield return new WaitForSeconds(shipConfiguration.GetSpawnTime());
        }

        public void ShipReachedHarbor(Ship ship)
        {
            ReachedShips += ship.CollectedChests + 1;
            ReachedChests += ship.CollectedChests;

            CheckWinningConditions();
        }

        private void CheckWinningConditions()
        {
            Debug.Assert(levelData, "No level data assigned");

            if (ReachedShips == levelData.RequiredShipCount && ReachedChests == levelData.RequiredChestCount)
                GameOver(WinningState.Won);
        }

        private void GameOver(WinningState winningState)
        {
            Debug.Log($"Game Over: {winningState}");
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(shipConfiguration.StartSpawnLine, shipConfiguration.EndSpawnLine);
        }

        public enum WinningState
        {
            Lost,
            Won
        }

        [Serializable]
        private class ShipSpawnConfiguration
        {
            public Ship ShipPrefab;
            public Vector3 MoveDirection;
            public Vector3 StartSpawnLine;
            public Vector3 EndSpawnLine;
            public float MinSpawnTime;
            public float MaxSpawnTime;

            public float GetSpawnTime() => UnityEngine.Random.Range(MinSpawnTime, MaxSpawnTime);
            public Vector3 GetSpawnLocation() => Vector3.Lerp(StartSpawnLine, EndSpawnLine, UnityEngine.Random.value);
        }
    }
}
