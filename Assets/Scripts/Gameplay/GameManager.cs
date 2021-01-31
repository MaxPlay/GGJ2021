using GGJ.Levels;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace GGJ.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private LevelData levelData;

        [SerializeField]
        private ShipSpawnConfiguration shipConfiguration;

        [SerializeField]
        private ChestSpawnConfiguration chestConfiguration;
        [SerializeField]
        private HostileSpawnConfiguration hostileConfiguration;

        [SerializeField]
        private TMP_Text scoreText, chestText;

        [SerializeField]
        private WindManager windManager;

        [SerializeField]
        GameObject tutorialObject;

        public enum WinningState
        {
            Lost,
            Won
        }

        public static GameManager Instance { get; private set; }
        public int ReachedChests { get; set; }
        public int Points { get; set; }
        public int ReachedShips { get; set; }
        public int CrashedShips { get; set; }
        public int SpawnedShips { get; private set; }

        [SerializeField]
        private UnityEvent<bool, int, int, LevelData> onGameOver = new UnityEvent<bool, int, int, LevelData>();
        public UnityEvent<bool, int, int, LevelData> OnGameOver { get => onGameOver; }

        public WindManager WindManager => windManager;

        public void ShipReachedHarbor(Ship ship)
        {
            ReachedShips++;
            Points += ship.CollectedChests + 1;
            ReachedChests += ship.CollectedChests;

            CheckEndgameCondition();
        }

        public void ShipCrashed(Ship ship)
        {
            CrashedShips++;

            CheckEndgameCondition();
        }

        public bool SpawnShip()
        {
            Debug.Assert(levelData, "No level data assigned");
            if (SpawnedShips >= levelData.ShipCount)
                return false;

            Ship ship = Instantiate(shipConfiguration.ShipPrefab, shipConfiguration.GetSpawnLocation(), Quaternion.LookRotation(shipConfiguration.MoveDirection));
            ship.OnCrash.AddListener(ShipCrashed);
            ship.DoSpawnAnimation();
            ++SpawnedShips;
            return true;
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void CheckEndgameCondition()
        {
            if (!levelData)
            {
                Debug.LogError("No level data assigned");
                return;
            }
            if (scoreText && chestText)
                UpdateTexts();
            if (levelData.ShipCount <= ReachedShips + CrashedShips)
            {
                CheckWinningConditions();
                CheckLosingConditions();
            }
        }

        private void CheckWinningConditions()
        {
            Debug.Assert(levelData, "No level data assigned");

            if (ReachedChests >= levelData.RequiredChestCount && Points >= levelData.RequiredShipCount)
                GameOver(WinningState.Won);
        }

        private void CheckLosingConditions()
        {
            Debug.Assert(levelData, "No level data assigned");

            if (ReachedChests < levelData.RequiredChestCount || Points < levelData.RequiredShipCount)
                GameOver(WinningState.Lost);
        }

        private void GameOver(WinningState winningState)
        {
            Debug.Log($"Game Over: {winningState}");
            onGameOver.Invoke(winningState == WinningState.Won, Points, ReachedChests, levelData);
        }

        private void UpdateTexts()
        {
            scoreText.text = Points.ToString() + "\n/" + levelData.RequiredShipCount;
            chestText.text = ReachedChests.ToString() + "\n/" + levelData.RequiredChestCount;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(shipConfiguration.StartSpawnLine, shipConfiguration.EndSpawnLine);

            Gizmos.color = Color.green;
            Vector2 center = chestConfiguration.SpawnArea.center;
            Vector2 size = chestConfiguration.SpawnArea.size;
            Gizmos.DrawWireCube(new Vector3(center.x, 0, center.y), new Vector3(size.x, 0, size.y));

            Gizmos.color = Color.magenta;
            center = hostileConfiguration.SpawnArea.center;
            size = hostileConfiguration.SpawnArea.size;
            Gizmos.DrawWireCube(new Vector3(center.x, 0, center.y), new Vector3(size.x, 0, size.y));
        }

        private IEnumerator ShipSpawnRoutine()
        {
            while (SpawnShip())
                yield return new WaitForSeconds(shipConfiguration.GetSpawnTime());
        }

        private void SpawnChests()
        {
            Debug.Assert(levelData != null, "No level data assigned, no chests will spawn");

            if (levelData.ChestCount == 0)
                return;

            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Obstacle");

            float minDistanceSqr = chestConfiguration.MinDistanceFromObstacles * chestConfiguration.MinDistanceFromObstacles;
            List<Vector3> chestPositions = new List<Vector3>();

            for (int i = 0; i < levelData.ChestCount; i++)
            {
                bool positionValid = true;
                Vector3 position = Vector3.zero;
                int tryCount = 0;
                do
                {
                    position.x = UnityEngine.Random.Range(chestConfiguration.SpawnArea.xMin, chestConfiguration.SpawnArea.xMax);
                    position.z = UnityEngine.Random.Range(chestConfiguration.SpawnArea.yMin, chestConfiguration.SpawnArea.yMax);


                    for (int j = 0; j < gameObjects.Length; j++)
                    {
                        if ((gameObjects[j].transform.position - position).sqrMagnitude < minDistanceSqr)
                        {
                            positionValid = false;
                            break;
                        }
                    }

                    for (int j = 0; j < chestPositions.Count; j++)
                    {
                        if ((chestPositions[j] - position).sqrMagnitude < minDistanceSqr)
                        {
                            positionValid = false;
                            break;
                        }
                    }
                    ++tryCount;
                } while (!positionValid && tryCount < 20);

                Instantiate(chestConfiguration.ChestPrefab, position, Quaternion.identity);
                chestPositions.Add(position);
            }
        }

        private void Start()
        {
            SpawnChests();
            StartCoroutine(ShipSpawnRoutine());
            tutorialObject.SetActive(levelData.UsesTutorial);
            if (scoreText && chestText)
                UpdateTexts();
        }

        [Serializable]
        private class ChestSpawnConfiguration
        {
            public Chest ChestPrefab;
            public Rect SpawnArea;
            public float MinDistanceFromObstacles;
        }

        [Serializable]
        private class ShipSpawnConfiguration
        {
            public Vector3 EndSpawnLine;
            public float MaxSpawnTime;
            public float MinSpawnTime;
            public Vector3 MoveDirection;
            public Ship ShipPrefab;
            public Vector3 StartSpawnLine;

            public Vector3 GetSpawnLocation() => Vector3.Lerp(StartSpawnLine, EndSpawnLine, UnityEngine.Random.value);

            public float GetSpawnTime() => UnityEngine.Random.Range(MinSpawnTime, MaxSpawnTime);
        }

        [Serializable]
        private class HostileSpawnConfiguration
        {
            //public PirateShip PirateShipPrefab;
            public Whale WhalePrefab;
            public Rect SpawnArea;
        }
    }
}