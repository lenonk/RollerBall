using Godot;

public class PlayButton : Button
{
    void OnPlayButtonPressed() {
        GetTree().ChangeScene("res://scenes/Level.tscn");
    }
}
