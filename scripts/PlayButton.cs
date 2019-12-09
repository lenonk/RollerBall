using System.Diagnostics;
using Godot;

public class PlayButton : Button
{
    public override void _Ready() {
        GrabFocus();
    }

    void OnPlayButtonPressed() {
        GetTree().ChangeScene("res://scenes/Level.tscn");
    }

    void OnPlayButtonGuiInput(InputEvent e) {
        GrabFocus();
    }
}
