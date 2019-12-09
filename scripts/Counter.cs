using System.Diagnostics;
using Godot;

public class Counter : Label
{
    int coins = 0;

    public override void _Ready()
    {
        Text = coins.ToString();        
    }

    public void OnCoinCollected() {
        coins++;
        Text = coins.ToString();
        if (GetTree().GetNodesInGroup("Coins").Count == 1)
            GetNode<Timer>("Timer").Start();
    }

    public void OnTimerTimeout() {
        if (GetTree().GetCurrentScene().GetName() == "Level") {
            Debug.WriteLine(GetTree().GetCurrentScene().GetName());
            GetTree().ChangeScene("res://scenes/Level2.tscn");
        }
        else
            GetTree().ChangeScene("res://scenes/Win.tscn");
    }
}
