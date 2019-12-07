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
        GetTree().ChangeScene("res://scenes/Win.tscn");
    }
}
