namespace Hullbreakers
{
    public class TargetPlayer : AI
    {
        protected override void HandleRotation()
        {
            rotation.target = PlayerRb.position;
        }
    }
}
