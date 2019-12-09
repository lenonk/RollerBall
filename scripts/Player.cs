using System.Diagnostics;
using Godot;

public class Player : KinematicBody
{
    // Variables
    [Export] public float Gravity = -9.8f;
    [Export] public float MaxSpeed = 15;
    [Export] public float Accel = 4;
    [Export] public float Decel = 6;

    private Vector3 _vel = new Vector3();
    private Vector3 _dir = new Vector3();

    private Camera _camera;
    private MeshInstance _mesh;

    private bool _isMoving;
 
    public override void _Ready() {
        _camera = GetNode<Camera>("CameraGimbal/InnerGimbal/Camera");
        _mesh = GetNode<MeshInstance>("MeshInstance");
    }

    public override void _Process(float delta) {
        ProcessInput(delta);
        ProcessMovement(delta);

        if (Translation.y < -15)
            GetTree().ChangeScene("res://scenes/GameOver.tscn");
    }

    private void ProcessInput(float delta) {
        _dir = new Vector3();
        Transform camXform = _camera.GetGlobalTransform();

        Vector2 inputMovementVector = new Vector2();

        if (Input.IsActionPressed("ui_up"))
            inputMovementVector.y += 1;
        if (Input.IsActionPressed("ui_down"))
            inputMovementVector.y -= 1;
        if (Input.IsActionPressed("ui_left"))
            inputMovementVector.x -= 1;
        if (Input.IsActionPressed("ui_right"))
            inputMovementVector.x += 1;

        if (inputMovementVector.x == 1 || inputMovementVector.y == 1)
            _isMoving = true;
        else
            _isMoving = false;

        inputMovementVector = inputMovementVector.Normalized();

        _dir += -camXform.basis.z * inputMovementVector.y;
        _dir += camXform.basis.x * inputMovementVector.x;
    }

    private void ProcessMovement(float delta) {
        _dir.y = 0;
        _dir = _dir.Normalized();

        _vel.y = 0;

        float accel = _dir.Dot(_vel) > 0 ? Accel : Decel;
        _dir *= MaxSpeed;
        _vel = _vel.LinearInterpolate(_dir, accel * delta);
        _vel.y = Gravity;

        ProcessRotation(_vel, delta);
        _vel = MoveAndSlide(_vel, Vector3.Up);
    }

    private void OnEnemyCollision(Node node) {
        if (_isMoving)
            GetTree().ChangeScene("res://scenes/GameOver.tscn");
    }

    private void ProcessRotation(Vector3 speed, float delta) {
        Vector3 rotation = speed * delta / 1; // 1 is radius of player mesh.
        _mesh.RotateZ(-rotation.x);
        _mesh.RotateX(rotation.z);
    }
}
