using GGJ.Levels;
using System;
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
            SpawnShip();
        }

        public void SpawnShip()
        {
            Debug.Assert(levelData, "No level data assigned");
            if (SpawnedShips >= levelData.ShipCount)
                return;

            Ship ship = Instantiate(shipConfiguration.ShipPrefab, Vector3.Lerp(shipConfiguration.StartSpawnLine, shipConfiguration.EndSpawnLine, UnityEngine.Random.value), Quaternion.LookRotation(shipConfiguration.MoveDirection));
            ship.DoSpawnAnimation();
            ++SpawnedShips;
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
        }
    }
}
