using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    public static InterfaceManager _instance;
    [SerializeField] private RectTransform whiteTurnPanel;
    [SerializeField] private RectTransform blackTurnPanel;
    [SerializeField] private Button resetButton;

    public PawnPromotionPanel pawnPromotionPanel;
    public WinPanel winPanel;

    private void Awake()
    {
        if (instance != null)
        {
            if (instance != this)
            {
                DestroyImmediate(gameObject);
                return;
            }
        }

        resetButton.onClick.AddListener(Game.instance.Reset);
    }

    public static InterfaceManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<InterfaceManager>();
                if (_instance == null)
                {
                    _instance = Instantiate(Resources.Load<InterfaceManager>("InterfaceManager"));
                }
            }
            return _instance;
        }
    }

    public void StartTurn(bool white)
    {
        whiteTurnPanel.gameObject.SetActive(white);
        blackTurnPanel.gameObject.SetActive(!white);
    }

    public void Reset()
    {
        pawnPromotionPanel.Hide();
        winPanel.Hide();
    }
}
