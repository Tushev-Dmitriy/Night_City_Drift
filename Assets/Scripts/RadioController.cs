using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RadioController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private TMP_Dropdown stationDropdown;

    private readonly Dictionary<string, string> radioStations = new Dictionary<string, string>
    {
        { "Дача", "http://radio.omg56.ru:8000/Dacha_Oren" },
        { "Дорожное", "radio.omg56.ru:8000/Dorognoe_Oren" },
        { "Европа Плюс", "radio.omg56.ru:8000/Europa_plus_Oren" },
        { "Монте Карло", "radio.omg56.ru:8000/Monte_Carlo_Oren" },
        { "Новое", "radio.omg56.ru:8000/Novoe_Oren" },
        { "Ретро", "radio.omg56.ru:8000/Retro" },
        { "Comedy", "radio.omg56.ru:8000/Comedy" }
    };

    private int currentStationIndex = 0;

    private void Start()
    {
        stationDropdown.ClearOptions();
        stationDropdown.AddOptions(new List<string>(radioStations.Keys));
        stationDropdown.onValueChanged.AddListener(OnStationChanged);
        StartCoroutine(LoadRadioStream(radioStations.ElementAt(currentStationIndex).Value));
    }

    private void OnStationChanged(int index)
    {
        currentStationIndex = index;
        StartCoroutine(LoadRadioStream(radioStations.ElementAt(currentStationIndex).Value));
    }

    private IEnumerator LoadRadioStream(string url)
    {
        audioSource.Stop();
        audioSource.clip = null;

        Debug.Log(0);

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            Debug.Log(1);

            if (request.result == UnityWebRequest.Result.Success)
            {
                using (UnityWebRequest audioRequest = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
                {
                    ((DownloadHandlerAudioClip)audioRequest.downloadHandler).streamAudio = true;
                    yield return audioRequest.SendWebRequest();

                    if (audioRequest.result == UnityWebRequest.Result.Success)
                    {
                        audioSource.clip = DownloadHandlerAudioClip.GetContent(audioRequest);
                        audioSource.Play();
                        Debug.Log($"Playing station: {radioStations.ElementAt(currentStationIndex).Key}");
                    }
                    else
                    {
                        Debug.LogError($"Failed to load audio: {audioRequest.error}");
                    }
                }
            }
            else
            {
                Debug.LogError($"Failed to get stream: {request.error}");
            }
        }
    }
}