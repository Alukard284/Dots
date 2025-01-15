using UnityEngine;

namespace DefaultNamespace
{
    public class CharacterHealth : MonoBehaviour
    {
        public ShootAbility shootAbility;
        public int health = int.MaxValue;
        public GoogleDriveDownloader driveDownloader;

        public int Health
        {
            get => health;
            set
            {
                health = value;

                if (health <= 0)
                {
                    Destroy(this.gameObject);
                }
            }
        }
        private void Start()
        {
            driveDownloader.DownloadFile();
            GameData gameData = driveDownloader.LoadGameData();
            if (gameData != null)
            {
                ApplyGameData(gameData);
            }
        }

        // Метод для применения загруженных данных
        public void ApplyGameData(GameData gameData)
        {
            if (gameData != null)
            {
                Health = gameData.health;
                Debug.Log($"Данные применены: Health = {Health}");
            }
            else
            {
                Debug.LogError("Не удалось применить данные: GameData is null.");
            }
        }
    }
}
