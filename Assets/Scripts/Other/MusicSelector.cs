using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class MusicSelector : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown musicDropdown;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private string[] trackNames;
    [SerializeField] private AudioClip[] audioClips;

    private void OnEnable()
    {
        musicDropdown.ClearOptions();
        musicDropdown.onValueChanged.RemoveAllListeners();
        musicDropdown.AddOptions(new System.Collections.Generic.List<string>(trackNames));
        int selectedTrackIndex = 0;
        musicDropdown.value = selectedTrackIndex;
        LoadTrack(selectedTrackIndex);
        musicDropdown.onValueChanged.AddListener(OnTrackChanged);
    }

    public void OnTrackChanged(int index)
    {
        LoadTrack(index);
    }

    private void LoadTrack(int index)
    {
        if (index < 0 || index >= audioClips.Length) return;

        musicSource.clip = audioClips[index];
        musicSource.Play();
    }

    private void OnDisable()
    {
        musicSource.Stop();
    }
}