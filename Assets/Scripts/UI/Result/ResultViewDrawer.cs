using UnityEngine;
using TMPro;
using System.Linq;

public class ResultViewDrawer : MonoBehaviour
{
    void Awake()
    {
        var winner = OilResultClientData.Tanks.OrderByDescending(tank => tank.oilAmount).First().spinner;
        GetComponent<TextMeshProUGUI>().text = $"{winner} Win!";
    }
}
