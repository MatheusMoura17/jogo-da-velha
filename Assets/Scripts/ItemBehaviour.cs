using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBehaviour : MonoBehaviour {

    [Header("Graphics")]
    public Image imageRenderer;
    public Sprite NSprite;
    public Sprite XSprite;
    public Sprite OSprite;

    public SymbolName value;

	// Use this for initialization
	void Start () {
        Reset();
	}

    public void EnemyClick()
    {
        if (value == SymbolName.N)
        {
            value = GameController.instance.enemySymbol;
            UpdateGraphics();
            GameController.instance.CalculateRound();
        }
    }

    public void UserClick()
    {
        if (value == SymbolName.N)
        {
            value = GameController.instance.playerSymbol;
            UpdateGraphics();
            GameController.instance.CalculateRound();
            GameController.instance.EnemySelectRandom();
        }
    }

    private void UpdateGraphics()
    {
        switch (value)
        {
            case SymbolName.O:
                imageRenderer.sprite = OSprite;
                break;
            case SymbolName.X:
                imageRenderer.sprite = XSprite;
                break;
            default:
                imageRenderer.sprite = NSprite;
                break;
        }
    }

    public void Reset()
    {
        value = SymbolName.N;
        UpdateGraphics();
    }
}
