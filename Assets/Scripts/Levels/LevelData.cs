using UnityEngine;
using UnityEngine.SceneManagement;

namespace GGJ.Levels
{
    [CreateAssetMenu(menuName = "GGJ/Level Data")]
    public class LevelData : ScriptableObject
    {
        [Header("Meta Data")]
        [SerializeField]
        private string levelName;

        [SerializeField]
        private Sprite previewSprite;

        [Header("Configuration")]
        [SerializeField]
        private string sceneName;

        [SerializeField]
        private int shipCount;

        [SerializeField]
        private int chestCount;

        [SerializeField]
        private int requiredChestCount;

        [SerializeField]
        private int requiredShipCount;

        [SerializeField]
        private bool usesTutorial;

        public bool UsesTutorial => usesTutorial;
        public string LevelName => levelName;
        public Sprite PreviewSprite => previewSprite;
        public int RequiredChestCount => requiredChestCount;
        public int RequiredShipCount => requiredShipCount;
        public int ShipCount => shipCount;
        public int ChestCount => chestCount;

        public void StartLevel()
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}