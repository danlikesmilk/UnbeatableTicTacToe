using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoardController : MonoBehaviour {

    [SerializeField]
    private GameObject mainPanel;

    [SerializeField]
    private GameObject xText;

    [SerializeField]
    private GameObject oText;

    [SerializeField]
    private GameObject winMessage;

    [SerializeField]
    private GameObject drawMessage;

    [SerializeField]
    private GameObject loseMessage;

    private string[,] boardModel;
    private RectTransform[,] panelTransforms;
    private int emptyPanels;

	// Use this for initialization
	void Start () {

        emptyPanels = 9;

        boardModel = new string[3, 3];
		for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                boardModel[i, j] = "";
            }
        }

        panelTransforms = new RectTransform[3,3];
        for(int i = 0; i < mainPanel.transform.childCount; i++)
        {
            RectTransform panelTransform = mainPanel.transform.GetChild(i).gameObject.GetComponent<RectTransform>();
            int xPos = Math.Sign(panelTransform.anchoredPosition.x) + 1;
            int yPos = Math.Sign(panelTransform.anchoredPosition.y) + 1;
            panelTransforms[xPos, yPos] = panelTransform;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SelectSquare()
    {
        var panel = EventSystem.current.currentSelectedGameObject;

        RectTransform rectTransform = panel.transform.parent.gameObject.GetComponent<RectTransform>();
        UpdateModel(rectTransform, "X");
        Instantiate(xText, panel.transform);
        Destroy(panel.GetComponent<Button>());
        emptyPanels--;

        GameWinner possibleWinner = BoardUtils.CheckForWinner(boardModel);
        if (emptyPanels == 0 || possibleWinner != GameWinner.NONE)
        {
            DisplayResultScreen(possibleWinner);
        }
        else
        {
            CarryOutAIMove();
        }
    }

    private void CarryOutAIMove()
    {
        Vector2Int chosenAIMove = GameAI.ChooseMove(boardModel);

        boardModel[chosenAIMove.x, chosenAIMove.y] = "O";

        GameObject chosenAIPanel = panelTransforms[chosenAIMove.x, chosenAIMove.y].gameObject.transform.GetChild(0).gameObject;
        Instantiate(oText, chosenAIPanel.transform);
        chosenAIPanel.GetComponent<Button>().onClick = null;
        emptyPanels--;

        GameWinner possibleWinner = BoardUtils.CheckForWinner(boardModel);
        if (emptyPanels == 0 || possibleWinner != GameWinner.NONE)
        {
            DisplayResultScreen(possibleWinner);
        }
    }

    private void DisplayResultScreen(GameWinner winner)
    {
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                RectTransform rectTransform = panelTransforms[i, j];
                Destroy(rectTransform.gameObject.transform.GetChild(0).GetComponent<Button>());
            }
        }

        Transform canvas = mainPanel.transform.parent;
        GameObject resultObject = new GameObject("ResultObject");
        resultObject.transform.SetParent(canvas);
        if(winner == GameWinner.PLAYER)
        {
            Instantiate(winMessage, canvas);
        }
        if(winner == GameWinner.NONE)
        {
            Instantiate(drawMessage, canvas);
        }
        if(winner == GameWinner.AI)
        {
            Instantiate(loseMessage, canvas);
        }
    }

    private void UpdateModel(RectTransform rectTransform, string newState)
    {
        int xPos = Math.Sign(rectTransform.anchoredPosition.x) + 1;
        int yPos = Math.Sign(rectTransform.anchoredPosition.y) + 1;

        boardModel[xPos, yPos] = newState;
    }
}
