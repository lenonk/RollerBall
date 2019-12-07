using Godot;
using System;

public class coin : Area
{
    [Signal]
    public delegate void CoinCollected();

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
