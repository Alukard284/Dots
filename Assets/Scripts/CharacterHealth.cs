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

        // ����� ��� ���������� ����������� ������
        public void ApplyGameData(GameData gameData)
        {
            if (gameData != null)
            {
                Health = gameData.health;
                Debug.Log($"������ ���������: Health = {Health}");
            }
            else
            {
                Debug.LogError("�� ������� ��������� ������: GameData is null.");
            }
        }
    }
}
