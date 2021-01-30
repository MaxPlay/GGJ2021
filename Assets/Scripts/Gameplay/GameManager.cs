using GGJ.Levels;
using UnityEngine;
using UnityEngine.Events;

namespace GGJ.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField]
        private LevelData levelData;

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

        public void SpawnShip()
        {
            Debug.Assert(levelData, "No level data assigned");
            if (SpawnedShips >= levelData.ShipCount)
                return;

            // TODO: We need some useful spawning logic here
        }

        public void ShipReachedHarbor(Ship ship)
        {
            ReachedShips++;
            if (ship.HasChest)
                ReachedChests++;

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

        public enum WinningState
        {
            Lost,
            Won
        }
    }
}
