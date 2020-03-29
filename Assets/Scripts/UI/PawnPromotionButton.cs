using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent (typeof(Button))]
public class PawnPromotionButton : MonoBehaviour
{
    [SerializeField] private Figure promotionFigure;

    public void Show(Figure parentFigure)
    {
        Game game = Game.instance;
        Figure childFigure = parentFigure;
        UnityAction clickAction = delegate
        {
            if (!(promotionFigure is Pawn))
            {
                parentFigure.Kill();
                childFigure = game.CreateFigure(promotionFigure, parentFigure.white, parentFigure.cell);
            }
            InterfaceManager.instance.pawnPromotionPanel.Hide();
            childFigure.EndTurn();
        };
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(clickAction);
    }
}