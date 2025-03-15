using System.Collections.Generic;
using UnityEngine;

public class SetCarPlateModel : MonoBehaviour
{
    [SerializeField] private GameObject rearPlate;
    [SerializeField] private GameObject backPlate;

    private readonly List<char> correctLetters = new List<char> { 'À', 'Â', 'Å', 'Ê', 'Ì', 'Í', 'Î', 'Ð', 'Ñ', 'Ò', 'Ó', 'Õ' };
    private readonly List<char> correctNumbers = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

    public void SetupCarPlate(string text)
    {
        char[] carPlateText = text.ToCharArray();

        for (int i = 0; i < carPlateText.Length; i++)
        {
            bool isLetter = (i == 0 || i == 4 || i == 5);
            int index = isLetter ? correctLetters.IndexOf(carPlateText[i]) : correctNumbers.IndexOf(carPlateText[i]);

            if (i == 6 && carPlateText[i] == '0')
            {
                DisableAllChildren(rearPlate.transform.GetChild(i + 1));
                DisableAllChildren(backPlate.transform.GetChild(i + 1));
            }
            else
            {
                SetPlateCharacter(rearPlate.transform.GetChild(i + 1), index);
                SetPlateCharacter(backPlate.transform.GetChild(i + 1), index);
            }
        }
    }

    private void SetPlateCharacter(Transform plateTransform, int activeIndex)
    {
        for (int j = 0; j < plateTransform.childCount; j++)
        {
            plateTransform.GetChild(j).gameObject.SetActive(j == activeIndex);
        }
    }

    private void DisableAllChildren(Transform plateTransform)
    {
        for (int j = 0; j < plateTransform.childCount; j++)
        {
            plateTransform.GetChild(j).gameObject.SetActive(false);
        }
    }
}
