using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCarPlateModel : MonoBehaviour
{
    [SerializeField] GameObject rearPlate;
    [SerializeField] GameObject backPlate;

    private List<char> correctLetters = new List<char> { 'À', 'Â', 'Å', 'Ê', 'Ì', 'Í', 'Î', 'Ð', 'Ñ', 'Ò', 'Ó', 'Õ' };
    private List<char> correctNumbers = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    public void SetupCarPlate(string text)
    {
        char[] carPlateText = text.ToCharArray();
        for (int i = 0; i < carPlateText.Length; i++)
        {
            if (i == 0 || i == 4 || i == 5)
            {
                Transform tempPos = rearPlate.transform.GetChild(i + 1);
                for (int j = 0; j < tempPos.childCount; j++)
                {
                    tempPos.GetChild(j).gameObject.SetActive(false);
                }

                tempPos.GetChild(correctLetters.IndexOf(carPlateText[i])).gameObject.SetActive(true);

                Transform tempPos1 = backPlate.transform.GetChild(i + 1);
                for (int j = 0; j < tempPos.childCount; j++)
                {
                    tempPos1.GetChild(j).gameObject.SetActive(false);
                }

                tempPos1.GetChild(correctLetters.IndexOf(carPlateText[i])).gameObject.SetActive(true);
            } else if ((i >= 1 && i <= 3) || (i >= 6 && i <= 8)) 
            {
                Transform tempPos = rearPlate.transform.GetChild(i + 1);
                for (int j = 0; j < tempPos.childCount; j++)
                {
                    tempPos.GetChild(j).gameObject.SetActive(false);
                }

                tempPos.GetChild(correctLetters.IndexOf(carPlateText[i])).gameObject.SetActive(true);

                Transform tempPos1 = backPlate.transform.GetChild(i + 1);
                for (int j = 0; j < tempPos.childCount; j++)
                {
                    tempPos1.GetChild(j).gameObject.SetActive(false);
                }

                tempPos1.GetChild(correctLetters.IndexOf(carPlateText[i])).gameObject.SetActive(true);
            }
        }
    }
}
