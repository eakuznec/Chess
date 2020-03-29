using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent (typeof(Image))]
public class Cell : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Position _pos;
    private Image _cellImage;
    [SerializeField] private Color _lightColor;
    [SerializeField] private Color _darkColor;
    [SerializeField] private RectTransform _hightlightImage;
    [HideInInspector] public Figure figure;

    private void Awake()
    {
        _cellImage = GetComponent<Image>();
        if(_cellImage == null)
        {
            _cellImage = gameObject.AddComponent<Image>();
        }
    }

    public Position pos
    {
        get => _pos;
        set
        {
            _pos = value;
            name = string.Format("Cell ({0}:{1})", x, y);
            if ((_pos.x + _pos.y) % 2 == 0)
            {
                _cellImage.color = _lightColor;
            }
            else
            {
                _cellImage.color = _darkColor;
            }
        }
    }

    public int x
    {
        get => _pos.x;
    }

    public int y
    {
        get => _pos.y;
    }

    public bool hightlight
    {
        set => _hightlightImage.gameObject.SetActive(value);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Game game = Game.instance;
        if (game.state == GameState.GAME && figure != null)
        {
            if(game.whiteTurn == figure.white)
            {
                game.dragFigure = figure;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Game game = Game.instance;
        if (game.state == GameState.GAME && game.dragFigure)
        {
            if (game.board.avaliableTarget.Contains(game.mouseoverCell))
            {
                game.dragFigure.MoveOnCell(game.mouseoverCell);
            }
            game.dragFigure = null;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Game.instance.mouseoverCell = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Game game = Game.instance;
        if(game.mouseoverCell == this)
        {
            game.mouseoverCell = this;
        }
    }
}
