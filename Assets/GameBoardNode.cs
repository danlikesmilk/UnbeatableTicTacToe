using System;
using System.Collections.Generic;

public class GameBoardNode
{
    private GameBoardNode parent;
    private List<GameBoardNode> children;
    private string[,] boardModel;
    private bool AITurn;
    private int score;

    public GameBoardNode(string[,] boardModel, bool AITurn)
	{
        this.boardModel = boardModel;
        this.AITurn = AITurn;
        children = new List<GameBoardNode>();
        
        switch(BoardUtils.CheckForWinner(boardModel))
        {
            case GameWinner.AI:
                score = 1;
                break;

            case GameWinner.PLAYER:
                score = -1;
                break;

            default:
                score = 0;
                break;
        }
    }

    public enum GameTurn
    {
        AI,
        PLAYER
    }


    public string[,] GetModel()
    {
        return boardModel;
    }

    public void SetParent(GameBoardNode parent)
    {
        this.parent = parent;
    }

    public GameBoardNode GetParent()
    {
        return parent;
    }

    public GameBoardNode GetChild(int index)
    {
        return children[index];
    }

    public List<GameBoardNode> GetChildren()
    {
        return children;
    }

    public void AddChild(GameBoardNode child)
    {
        children.Add(child);
        child.SetParent(this);
    }

    public bool IsRootNode()
    {
        if(parent == null)
        {
            return true;
        }
        return false;
    }
    
    public bool GetAITurn()
    {
        return AITurn;
    }

    public bool IsLeaf()
    {
        return GetChildren().Count == 0;
    }

    public int GetScore()
    {
        return score;
    }
}
