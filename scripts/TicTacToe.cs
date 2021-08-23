using Godot;
using Array = Godot.Collections.Array;

public class TicTacToe : Node
{
    [Export]
    private NodePath title;

    private const string X = "X";
    private const string O = "O";

    private Label label;
    private Button[,] grid = new Button[3,3];

    private Solution[] solutions = new Solution[]{
        //horizontal
        new Solution{ x1= 0, y1 = 0, x2 = 0, y2 = 1, x3 = 0, y3 = 2},
        new Solution{ x1= 1, y1 = 0, x2 = 1, y2 = 1, x3 = 1, y3 = 2},
        new Solution{ x1= 2, y1 = 0, x2 = 2, y2 = 1, x3 = 2, y3 = 2},

        // vertical
        new Solution{ x1= 0, y1 = 0, x2 = 1, y2 = 0, x3 = 2, y3 = 0},
        new Solution{ x1= 0, y1 = 1, x2 = 1, y2 = 1, x3 = 2, y3 = 1},
        new Solution{ x1= 0, y1 = 2, x2 = 1, y2 = 2, x3 = 2, y3 = 2},

        //diagonal
        new Solution{ x1= 0, y1 = 0, x2 = 1, y2 = 1, x3 = 2, y3 = 2},
        new Solution{ x1= 2, y1 = 0, x2 = 1, y2 = 1, x3 = 0, y3 = 2},
    };

    private struct Solution{
        public int x1,y1,x2,y2,x3,y3;
    }

    private string current = "X";
    private int step = 0;

    public override void _Ready()
    {
        label = GetNode<Label>(title);

        Node child = GetChild(0);

        int index = 0;

        for(int x = 0; x < 3; x++)
        {
            for(int y = 0; y < 3; y++)
            {
                Button button = child.GetChild<Button>(index);
                Array array = new Array{button};

                button.Connect("pressed", this, "ButtonClicked", array);
                grid[x,y] = button;

                index++;
            }
        }

        SetTurn(current);
    }

    private void ButtonClicked(Button button)
    {
        step++;
        button.Text = current;

        if(CheckWinner(current))
        {
            return;
        }

        current = current == X ? O : X;

        SetTurn(current);
    }

    private bool CheckWinner(string current)
    {
        foreach(Solution item in solutions)
        {
            string a = grid[item.x1, item.y1].Text;
            string b = grid[item.x2, item.y2].Text;
            string c = grid[item.x3, item.y3].Text;

            if(a == current && b == current && c == current)
            {
                Win(current);
                return true;
            }
        }
        
        if(step == 9)
        {
            Draw();
            return true;
        }

        return false;
    }

    private void SetTurn(string current)
    {
        string text = "Turn: " + current;
        GD.Print(text);
        label.Text = text;
    }

    private void Draw()
    {
        const string text = "DRAW!";
        GD.Print(text);
        label.Text = text;
    }

    private void Win(string winner)
    {
        string text = current + " WINS!";
        GD.Print(text);
        label.Text = text;
    }
}
