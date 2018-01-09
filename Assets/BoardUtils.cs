using System;

public static class BoardUtils
{

    public static string[,] CopyBoard(string[,] oldModel)
    {
        string[,] newModel = new string[3, 3];
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                newModel[i, j] = oldModel[i, j];
            }
        }
        return newModel;
    }

    public static GameWinner CheckForWinner(string[,] boardModel)
    {
        string winningSymbol = "";

        // Check columns
        for (int x = 0; x < 3; x++)
        {
            string row1 = boardModel[x, 0];
            string row2 = boardModel[x, 1];
            string row3 = boardModel[x, 2];
            if ((row1 == row2 && row2 == row3) && row1 != "")
            {
                winningSymbol = row1;
            }
        }

        // Check rows
        for (int y = 0; y < 3; y++)
        {
            string col1 = boardModel[0, y];
            string col2 = boardModel[1, y];
            string col3 = boardModel[2, y];
            if ((col1 == col2 && col2 == col3) && col1 != "")
            {
                winningSymbol = col1;
            }
        }

        // Check diagonals
        string diag1 = boardModel[0, 0];
        string diag2 = boardModel[1, 1];
        string diag3 = boardModel[2, 2];
        if (((diag1 == diag2) && (diag2 == diag3)) && diag1 != "")
        {
            winningSymbol = diag1;
        }

        diag1 = boardModel[0, 2];
        diag3 = boardModel[2, 0];
        if (((diag1 == diag2) && (diag2 == diag3)) && diag1 != "")
        {
            winningSymbol = diag1;
        }

        GameWinner winner;
        switch (winningSymbol)
        {
            case "X":
                winner = GameWinner.PLAYER;
                break;

            case "O":
                winner = GameWinner.AI;
                break;

            default:
                winner = GameWinner.NONE;
                break;
        }
        return winner;
    }
}
