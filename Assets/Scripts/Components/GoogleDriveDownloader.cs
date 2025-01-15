using System;
using System.IO;
using System.Net;
using UnityEngine;

namespace DefaultNamespace
{
    public class GoogleDriveDownloader : MonoBehaviour
    {
        // URL для скачивания файла с Google Drive
        private string fileUrl = "https://drive.google.com/uc?export=download&id=1pbF5MlmSB5WheP0TtR2XsrP7oL67gggE";

        // Имя файла для сохранения
        private string fileName = "GameData.json";

        // Метод для загрузки файла
        public void DownloadFile()
        {
            string savePath = Path.Combine(Application.persistentDataPath, fileName);

            // Проверка наличия файла
            if (File.Exists(savePath))
            {
                Debug.Log("Файл уже существует. Удаляем старый файл.");
                File.Delete(savePath); // Удаляем существующий файл
            }

            using (WebClient client = new WebClient())
            {
                try
                {
                    // Загрузка файла
                    client.DownloadFile(fileUrl, savePath);
                    Debug.Log($"Файл успешно загружен и сохранен по пути: {savePath}");
                }
                catch (WebException ex)
                {
                    Debug.LogError($"Ошибка при загрузке файла: {ex.Message}");
                }
            }
        }

        // Метод для чтения данных из загруженного файла
        public GameData LoadGameData()
        {
            string filePath = Path.Combine(Application.persistentDataPath, fileName);

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                GameData gameData = JsonUtility.FromJson<GameData>(json);
                Debug.Log("Данные успешно загружены.");
                return gameData;
            }
            else
            {
                Debug.LogError("Файл не найден.");
                return null;
            }
        }
    }

    // Класс для хранения данных игры
    [Serializable]
    public class GameData
    {
        public int health;
        public int score;
    }
}