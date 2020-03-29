using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(RectTransform))]
public class PawnPromotionPanel : MonoBehaviour
{
    private Figure _parentFigure;
    private PawnPromotionButton[] buttons;

    private void Awake()
    {
        buttons = FindObjectsOfType<PawnPromotionButton>();
    }
    
    public void Show(Figure parentFigure)
    {
        Game.instance.state = GameState.PROMOTION;
        gameObject.SetActive(true);
        _parentFigure = parentFigure;
        foreach(PawnPromotionButton button in buttons)
        {
            button.Show(_parentFigure);
        }
    }

    public void Hide()
    {
        Game.instance.state = GameState.GAME;
        gameObject.SetActive(false);
    }

    public Figure parentFigure
    {
        get => _parentFigure;
    }
}
