using System.Collections;
using UnityEngine;

public class AnotherBtnController : MonoBehaviour
{
    [SerializeField] GameObject btnMenu;

    private Animator _btnMenuAnimator;

    private void Awake()
    {
        _btnMenuAnimator = btnMenu.GetComponent<Animator>();
    }

    public void ShowBtnMenu()
    {
        btnMenu.SetActive(true);
        _btnMenuAnimator.SetTrigger("Show");
    }

    public void HideBtnMenu()
    {
        StartCoroutine(HideMenuCoroutine());
    }

    private IEnumerator HideMenuCoroutine()
    {
        _btnMenuAnimator.SetTrigger("Hide");
        yield return new WaitForSeconds(1f);
        btnMenu.SetActive(false);
    }
}