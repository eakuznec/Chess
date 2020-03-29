using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private RectTransform whitePanel;
    [SerializeField] private RectTransform blackPanel;
    [SerializeField] private Button resetButton;

    private void Awake()
    {
        resetButton.onClick.AddListener(Game.instance.Reset);
    }

    public void Show(bool white)
    {
        Game.instance.state = GameState.WIN;
        whitePanel.gameObject.SetActive(white);
        blackPanel.gameObject.SetActive(!white);
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        Game.instance.state = GameState.GAME;
        gameObject.SetActive(false);
    }
}
