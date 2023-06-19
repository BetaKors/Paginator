namespace BetaKors.Paginator
{
    public class TransitionParams { }

    public class EnlargeParams : TransitionParams
    {
        public float SmallEnlargeAmount { get; set; }
        public float SmallEnlargeTime { get; set; }
        public float ShrinkTime { get; set; }
        public float BigEnlargeTime { get; set; }

        public EnlargeParams(float smallEnlargeAmount, float smallEnlargeTime, float shrinkTime, float bigEnlargeTime)
        {
            SmallEnlargeAmount = smallEnlargeAmount;
            SmallEnlargeTime = smallEnlargeTime;
            ShrinkTime = shrinkTime;
            BigEnlargeTime = bigEnlargeTime;
        }
    }

    public class CrossfadeParams : TransitionParams
    {
        public float Duration { get; set; }

        public CrossfadeParams(float duration)
        {
            Duration = duration;
        }
    }

    public class SwipeLeftParams : TransitionParams
    {
        public float Duration { get; set; }

        public SwipeLeftParams(float duration)
        {
            Duration = duration;
        }
    }
}
