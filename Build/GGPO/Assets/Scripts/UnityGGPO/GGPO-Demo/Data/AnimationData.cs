public class AnimationData
{
    public static float FRAME_COUNTER = 0.2f;

    public static float IDLE_TOTAL_FRAMES = 9f;
    public static float WALK_FORWARD_TOTAL_FRAMES = 11f;
    public static float WALK_BACKWARD_TOTAL_FRAMES = 11f;
    public static float CROUCH_TOTAL_FRAMES = 4f;
    public static float JUMP_UP_TOTAL_FRAMES = 11f;
    public static float JUMP_FORWARD_TOTAL_FRAMES = 13f;
    public static float JUMP_BACKWARD_TOTAL_FRAMES = 13f;

    public static float IDLE_FRAME_RATE = 12f;

    public class AnimatorKeys
    {
        public static string IDLE = "Idle";
        public static string WALK_FORWARD = "WalkForward";
        public static string WALK_BACKWARD = "WalkBackward";
        public static string CROUCH = "Crouch";
        public static string JUMP_UP = "JumpUp";
        public static string JUMP_FORWARD = "JumpForward";
        public static string JUMP_BACKWARD = "JumpBackward";
    }
}