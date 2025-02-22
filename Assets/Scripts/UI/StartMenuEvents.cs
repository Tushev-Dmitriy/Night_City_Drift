using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StartMenuEvents : MonoBehaviour
{
    private UIDocument _document;

    private Button _pastButton;
    private Button _nextButton;

    private List<Button> _menuButtons = new List<Button>();

    private AudioSource _audioSource;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        _audioSource = GetComponent<AudioSource>();

        _pastButton = _document.rootVisualElement.Q("PastBtn") as Button;
        _nextButton = _document.rootVisualElement.Q("NextBtn") as Button;

        _pastButton.RegisterCallback<ClickEvent>(OnPastBtnClick);
        _nextButton.RegisterCallback<ClickEvent>(OnNextBtnClick);

        _menuButtons = _document.rootVisualElement.Query<Button>().ToList();
        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonClick);
        }
    }

    private void OnPastBtnClick(ClickEvent evt)
    {
        Debug.Log("Past");
    }

    private void OnNextBtnClick(ClickEvent evt)
    {
        Debug.Log("Next");
    }

    private void OnAllButtonClick(ClickEvent evt)
    {
        _audioSource.Play();
    }

    private void OnDisable()
    {
        _pastButton.UnregisterCallback<ClickEvent>(OnPastBtnClick);
        _nextButton.UnregisterCallback<ClickEvent>(OnNextBtnClick);

        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonClick);
        }
    }
}
