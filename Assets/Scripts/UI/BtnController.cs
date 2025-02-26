using System.Collections;
using UnityEngine;

public class BtnController : MonoBehaviour
{
    [SerializeField] GameObject btnMenu;

    private Animator _btnMenuAnimator;
    private bool _btnMenuActive = false;

    private void Awake()
    {
        _btnMenuAnimator = btnMenu.GetComponent<Animator>();
    }

    public void ShowBtnMenu()
    {
        if (_btnMenuActive)
        {
            StartCoroutine(HideMenuCoroutine());
        }
        else
        {
            btnMenu.SetActive(true);
            _btnMenuAnimator.SetTrigger("Show");
            _btnMenuActive = true;
        }
    }

    private IEnumerator HideMenuCoroutine()
    {
        _btnMenuAnimator.SetTrigger("Hide");
        yield return new WaitForSeconds(1f);
        btnMenu.SetActive(false);
        _btnMenuActive = false;
    }
}