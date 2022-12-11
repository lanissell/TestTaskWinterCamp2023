namespace Plates
{
    public class PlateMovingBack: Plate
    {
        public override bool PlateHaveEffect()
        {
            GlobalEventManager.SendOnPlayerMovementStart(PlateNum);
            return true;
        }
    }
}
