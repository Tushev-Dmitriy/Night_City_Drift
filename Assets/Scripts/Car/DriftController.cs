using System.Collections;
using UnityEngine;
using TMPro;

public class DriftController : MonoBehaviour
{
    [SerializeField] private PrometeoCarController _carController;
    [SerializeField] private GameEvent addMoneyEvent;
    [HideInInspector] public TMP_Text driftText;

    private float currentTime = 0f;
    private float driftCooldown = 0f;
    private float score = 0f;
    private Coroutine scoreUpdateCoroutine;

    private void Start()
    {
        StartCoroutine(CheckDriftCooldown());
    }

    private void Update()
    {
        if (_carController.isDrifting)
        {
            if (driftCooldown <= 0f)
            {
                currentTime = 0f;
                score = 0f;
            }

            currentTime += Time.deltaTime;
            driftCooldown = 0.5f;

            if (currentTime >= 0.1f)
            {
                score += 10;
                currentTime = 0f;
                scoreUpdateCoroutine = StartCoroutine(UpdateScoreTextSmoothly(score));
            }
        }
    }

    private IEnumerator UpdateScoreTextSmoothly(float targetScore)
    {
        float displayScore = 0f;
        if (driftText.text != null && float.TryParse(driftText.text, out float parsedScore))
        {
            displayScore = parsedScore;
        }

        while (displayScore < targetScore)
        {
            displayScore += 100f * Time.deltaTime;
            driftText.text = $"{Mathf.Floor(displayScore)}";
            yield return null;
        }

        driftText.text = $"{Mathf.Floor(targetScore)}";
    }

    private IEnumerator CheckDriftCooldown()
    {
        driftText.text = null;

        while (true)
        {
            if (driftCooldown > 0f)
            {
                driftCooldown -= Time.deltaTime;
                if (driftCooldown <= 0f && !_carController.isDrifting && score > 0)
                {
                    SendScoreAndReset();
                }
            }
            yield return null;
        }
    }

    private void SendScoreAndReset()
    {
        if (score > 0)
        {
            SaveManager.Instance?.CheckDriftScore((int)score);
            addMoneyEvent.Raise(-(int)score);
        }

        currentTime = 0f;
        score = 0f;
        driftText.text = null;
        StopCoroutine(scoreUpdateCoroutine);
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentTime = 0f;
        score = 0f;
        driftText.text = null;
        StopCoroutine(scoreUpdateCoroutine);
    }
}