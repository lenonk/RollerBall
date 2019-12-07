using System.Diagnostics;
using Godot;

public class Player : KinematicBody
{
    // Constants
    const int Speed = 12;
    const float rotSpeed = (float)(Speed * 0.75);

    // Variables
    Vector3 velocity = new Vector3();

    Vector3 GetRotationSpeed(Vector3 speed) {
        return speed * (float)0.75;
    }

    public override void _PhysicsProcess(float delta) {
        var _mesh = GetNode<MeshInstance>("MeshInstance");

        if (Input.IsActionPressed("ui_left") && Input.IsActionPressed("ui_right"))
            velocity.x = Mathf.Lerp(velocity.x, 0, (float)0.05);
        else if (Input.IsActionPressed("ui_left"))
            velocity.x = Mathf.Lerp(velocity.x, -Speed, (float)0.1);
        else if (Input.IsActionPressed("ui_right"))
            velocity.x = Mathf.Lerp(velocity.x, Speed, (float)0.1);
        else
            velocity.x = Mathf.Lerp(velocity.x, 0, (float)0.05);

        if (Input.IsActionPressed("ui_up") && Input.IsActionPressed("ui_down"))
            velocity.z = Mathf.Lerp(velocity.z, 0, (float)0.05);
        else if (Input.IsActionPressed("ui_up"))
            velocity.z = Mathf.Lerp(velocity.z, -Speed, (float)0.1);
        else if (Input.IsActionPressed("ui_down"))
            velocity.z = Mathf.Lerp(velocity.z, Speed, (float)0.1);
        else
            velocity.z = Mathf.Lerp(velocity.z, 0, (float)0.05);

        var rotSpeed = GetRotationSpeed(velocity);
        _mesh.RotateZ(-Mathf.Deg2Rad(rotSpeed.x));
        _mesh.RotateX(Mathf.Deg2Rad(rotSpeed.z));
        
        MoveAndSlide(velocity);
    }

    public void OnEnemyBodyEntered(object sender) {
        if (!(sender is KinematicBody))
            return;

        KinematicBody body = (KinematicBody)sender;
        if (body.Name == "Player")
            GetTree().ChangeScene("res://scenes/GameOver.tscn");
    }
}
