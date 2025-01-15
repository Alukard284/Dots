using System;
using System.IO;
using System.Net;
using UnityEngine;

namespace DefaultNamespace
{
    public class GoogleDriveDownloader : MonoBehaviour
    {
        // URL ��� ���������� ����� � Google Drive
        private string fileUrl = "https://drive.google.com/uc?export=download&id=1pbF5MlmSB5WheP0TtR2XsrP7oL67gggE";

        // ��� ����� ��� ����������
        private string fileName = "GameData.json";

        // ����� ��� �������� �����
        public void DownloadFile()
        {
            string savePath = Path.Combine(Application.persistentDataPath, fileName);

            // �������� ������� �����
            if (File.Exists(savePath))
            {
                Debug.Log("���� ��� ����������. ������� ������ ����.");
                File.Delete(savePath); // ������� ������������ ����
            }

            using (WebClient client = new WebClient())
            {
                try
                {
                    // �������� �����
                    client.DownloadFile(fileUrl, savePath);
                    Debug.Log($"���� ������� �������� � �������� �� ����: {savePath}");
                }
                catch (WebException ex)
                {
                    Debug.LogError($"������ ��� �������� �����: {ex.Message}");
                }
            }
        }

        // ����� ��� ������ ������ �� ������������ �����
        public GameData LoadGameData()
        {
            string filePath = Path.Combine(Application.persistentDataPath, fileName);

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                GameData gameData = JsonUtility.FromJson<GameData>(json);
                Debug.Log("������ ������� ���������.");
                return gameData;
            }
            else
            {
                Debug.LogError("���� �� ������.");
                return null;
            }
        }
    }

    // ����� ��� �������� ������ ����
    [Serializable]
    public class GameData
    {
        public int health;
        public int score;
    }
}