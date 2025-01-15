using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCount : MonoBehaviour, IAbilityTarget
{
    public List<GameObject> Targets { get; set; }
    [SerializeField] private string[] allowedTags;
    [SerializeField] public int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText.text = score.ToString();
    }
    public void Execute()
    {
        foreach (var target in Targets)
        {
            if (target != null && IsTagAllowed(target.tag)) // Проверяем тег
            {
                score++;
                scoreText.text = score.ToString();
                Debug.Log($"Collect meset: {score}");
            }
        }
        Destroy(gameObject); // Уничтожаем объект после применения урона
    }

    private bool IsTagAllowed(string tag)
    {
        foreach (var allowedTag in allowedTags)
        {
            if (tag == allowedTag)
            {
                return true;
            }
        }
        return false;
    }
}
