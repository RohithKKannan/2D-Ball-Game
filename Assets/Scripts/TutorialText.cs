using UnityEngine;
using System.Collections;
using TMPro;
public class TutorialText : MonoBehaviour
{
    private TMP_Text tutorialText;
    private int maxCount;
    private int maxVisibleCount;
    [SerializeField] private float timeBetweenChars = 0.1f;
    [SerializeField] private float timeAfterText = 3f;
    void Start()
    {
        tutorialText = GetComponent<TMP_Text>();
        maxCount = tutorialText.text.Length;
        tutorialText.maxVisibleCharacters = 0;
        maxVisibleCount = 0;
        StartCoroutine(DisplayText());
    }
    IEnumerator DisplayText()
    {
        maxVisibleCount++;
        while (maxVisibleCount <= maxCount)
        {
            yield return new WaitForSeconds(timeBetweenChars);
            tutorialText.maxVisibleCharacters = maxVisibleCount;
            maxVisibleCount++;
        }
        yield return new WaitForSeconds(timeAfterText);
        tutorialText.maxVisibleCharacters = 0;
    }
}
