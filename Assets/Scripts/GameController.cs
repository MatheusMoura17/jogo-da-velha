using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public ItemBehaviour[] grid;

    public static GameController instance;

    [Header("Graphics")]
    public Text roundMessages;
    public GameObject gamePanel;
    public GameObject choisingSymbol;

    [HideInInspector]
    public SymbolName playerSymbol;
    public SymbolName enemySymbol;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        Reset();
    }

    private void Reset()
    {
        foreach (ItemBehaviour item in grid)
            item.Reset();
        gamePanel.SetActive(false);
        choisingSymbol.SetActive(true);
        roundMessages.text = "Escolha seu simbolo";
    }

    private void StartRound()
    {
        gamePanel.SetActive(true);
        choisingSymbol.SetActive(false);
        roundMessages.text = "É a sua vez de jogar!";
    }

    public void OnSelectSymbol_O()
    {
        playerSymbol = SymbolName.O;
        enemySymbol = SymbolName.X;
        StartRound();
    }

    public void EnemySelectRandom()
    {
        ItemBehaviour[] items = grid;
        for (int i = 0; i > items.Length; i++)
        {
            int random = Random.Range(0, i);
            ItemBehaviour older = items[i];
            items[i] = items[random];
            items[random] = older;
        }

        foreach (ItemBehaviour item in items)
        {
            if (item.value == SymbolName.N)
            {
                item.EnemyClick();
                break;
            }
        }

    }

    public void OnSelectSymbol_X()
    {
        playerSymbol = SymbolName.X;
        enemySymbol = SymbolName.O;
        StartRound();
    }

    public void CalculateRound()
    {
        ItemBehaviour[,] items = ConvertGridToMatrix(grid, 3);
        SymbolName winner;
        if (FindMatch(out winner, items))
        {
            if (winner == playerSymbol)
            {
                roundMessages.text = "Parabéns! você ganhou";
                Invoke("Reset", 2);
            }
            else
            {
                roundMessages.text = "Você perdeu...";
                Invoke("Reset", 2);
            }
        }
        else if (!HasSlotEmpty(grid))
        {
            roundMessages.text = "Deu veia...";
            Invoke("Reset", 2);
        }

    }

    private bool HasSlotEmpty(ItemBehaviour[] grid)
    {
        foreach (ItemBehaviour item in grid)
        {
            if (item.value == SymbolName.N)
                return true;
        }

        return false;
    }

    private bool FindMatch(out SymbolName winner, ItemBehaviour[,] items)
    {
        int rayCount = 2;
        for(int i = 0; i < items.GetLength(0); i++)
        {
            for (int j = 0; j < items.GetLength(1); j++)
            {
                //horizontal
				if ((items.GetLength(1) - j) > rayCount && items[j , i].value!=SymbolName.N)
                {
					if (items[j , i].value==items[j+1 , i].value && items[j , i].value == items[j +2, i].value) {
						winner = items [j, i].value;
						return true;
					}
                }
				//vertical
				if ((items.GetLength(0) - i) > rayCount && items[j , i].value!=SymbolName.N)
				{
					if (items[j , i].value==items[j , i+1].value && items[j , i].value == items[j, i+2].value) {
						winner = items [j, i].value;
						return true;
					}
				}
				//diagonal direito
				if (((items.GetLength(1) - j) > rayCount && (items.GetLength(0) - i) > rayCount) && items[j , i].value!=SymbolName.N)
				{
					if (items[j , i].value==items[j+1 , i+1].value && items[j , i].value == items[j+2, i+2].value) {
						winner = items [j, i].value;
						return true;
					}
				}

				//diagonal esquerdo
				bool isTopRight = j==items.GetLength (0)-1 && i == 0;
				if (((j - rayCount) > rayCount && (i - rayCount) > rayCount || isTopRight) && items[j , i].value!=SymbolName.N)
				{
					if (items[j , i].value==items[j-1 , i+1].value && items[j , i].value == items[j-2, i+2].value) {
						winner = items [j, i].value;
						return true;
					}
				}
            }
        }
        winner = SymbolName.N;
        return false;
    }


    private ItemBehaviour[,] ConvertGridToMatrix(ItemBehaviour[] grid, int size)
    {
        ItemBehaviour[,] items = new ItemBehaviour[size, size];
        int gridCounter = 0;
        for (int i = 0; i < items.GetLength(0); i++)
        {
            for (int j = 0; j < items.GetLength(1); j++)
            {
                items[j, i] = grid[gridCounter++];
            }
        }
        return items;
    }

}
