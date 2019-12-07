using Godot;

public class coin : Area
{
    [Signal]
    public delegate void CoinCollected();

    public override void _Ready() {
        /*var _counter = GetTree().GetRoot()
            .GetNode<Spatial>("Level")
            .GetNode<Control>("Control")
            .GetNode<Label>("Counter");*/

        var _counter = GetTree().GetRoot().GetNode<Label>("Level/Control/Counter");
        Connect("CoinCollected", _counter, "OnCoinCollected");
    }

    public override void _PhysicsProcess(float delta) {
        RotateY(Mathf.Deg2Rad(3));
    }

    public void OnCoinBodyEntered(object sender) {
        if (!(sender is KinematicBody))
            return;

        var _body = (KinematicBody)sender;
        if (_body.Name == "Player") {
            GetNode<Timer>("Timer").Start();
            GetNode<AnimationPlayer>("AnimationPlayer").Play("bounce");
        }
    }

    public void OnTimerTimeout() {
        EmitSignal(nameof(CoinCollected));
        QueueFree();
    }
}
