using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GameSetting", menuName = "Game Setting", order = 51)]
public class GameSettings : ScriptableObject
{
    public List<FigureStartSettings> figureSettings = new List<FigureStartSettings>();
}
