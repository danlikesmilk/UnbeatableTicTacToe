using System.Collections.Generic;
using UnityEngine;

public class GameAI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static Vector2Int ChooseMove(string[,] boardModel)
    {

        GameBoardNode rootNode = new GameBoardNode(boardModel, true);
        CreateGameTree(rootNode);
        List<Vector2Int> availableSpaces = GetAvailableMoves(boardModel);

        List<GameBoardNode> children = rootNode.GetChildren();
        List<int> moves = new List<int>();

        foreach(GameBoardNode child in children)
        {
            moves.Add(MiniMaxNode(child));
        }

        int index = moves.GetIndexOfMaxElement<int>();

        string[,] newModel = rootNode.GetChild(index).GetModel();

        Vector2Int chosenMove = new Vector2Int();
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if(boardModel[i,j] != newModel[i,j])
                {
                    chosenMove.x = i;
                    chosenMove.y = j;
                }
            }
        }
        return chosenMove;
    }

    private static List<Vector2Int> GetAvailableMoves(string[,] boardModel)
    {
        List<Vector2Int> availableSpaces = new List<Vector2Int>();

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (boardModel[i, j] == "")
                {
                    availableSpaces.Add(new Vector2Int(i, j));
                }
            }
        }

        return availableSpaces;
    }

    private static void CreateGameTree(GameBoardNode node)
    {
        List<Vector2Int> availableSpaces = GetAvailableMoves(node.GetModel());

        if(availableSpaces.Count == 0 || BoardUtils.CheckForWinner(node.GetModel()) != GameWinner.NONE)
        {
            return;
        }

        foreach (Vector2Int space in availableSpaces)
        {
            string[,] newModel = BoardUtils.CopyBoard(node.GetModel());
            if(node.GetAITurn())
            {
                newModel[space.x, space.y] = "O";
            }
            else
            {
                newModel[space.x, space.y] = "X";
            }
            GameBoardNode newNode = new GameBoardNode(newModel, !node.GetAITurn());
            node.AddChild(newNode);

            CreateGameTree(newNode);
        }
    }

    private static int MiniMaxNode(GameBoardNode node)
    {
        if(node.IsLeaf() || BoardUtils.CheckForWinner(node.GetModel()) != GameWinner.NONE)
        {
            return node.GetScore();
        }

        List<GameBoardNode> children = node.GetChildren();

        List<int> miniMaxValues = new List<int>();

        foreach(GameBoardNode child in children)
        {
            miniMaxValues.Add(MiniMaxNode(child));
        }

        if(children.Count == 8)
        {
            Debug.Log("Here!");
        }

        if(node.GetAITurn())
        {
            return miniMaxValues.GetMaxElement<int>();
        }
        else
        {
            return miniMaxValues.GetMinElement<int>();
        }

    }
}
