using Godot;

namespace GodotBackgroundSimulation.Scripts.GodotScripts;

public partial class PlayerController : CharacterBody2D
{
    private AnimationPlayer _animationPlayer;
    private Sprite2D _sprite;
    private Vector2I _facingDirection;
    private string DirectionString => GetFacingDirectionString();

    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _sprite = GetNode<Sprite2D>("Sprite2D");
    }

    public override void _Process(double delta)
    {
        Vector2 inputVector = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        Velocity = inputVector * 50;

        if (inputVector != Vector2.Zero)
        {
            UpdateFacingDirection(inputVector);
            _animationPlayer.Play($"{DirectionString}-walk");
        }
        else
        {
            _animationPlayer.Play($"{DirectionString}-idle");
        }

        MoveAndSlide();
    }
    
    private void UpdateFacingDirection(Vector2 movement)
    {
        if (movement.Y > 0)
        {
            _facingDirection = Vector2I.Down;
        }
        else if (movement.Y < 0)
        {
            _facingDirection = Vector2I.Up; 
        }
        else if (movement.X != 0)
        {
            _sprite.FlipH = movement.X > 0;
            _facingDirection = movement.X > 0 ? Vector2I.Right : Vector2I.Left;
        }
    }
    
    private string GetFacingDirectionString()
    {
        var directionString = "front"; // default
        
        if (_facingDirection == Vector2I.Up) directionString = "back";
        else if (_facingDirection == Vector2I.Left || _facingDirection == Vector2I.Right) directionString = "side";

        return directionString;
    }
}
