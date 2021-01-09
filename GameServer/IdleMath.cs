namespace GameServer
{
    public class IdleMath
    {

        public static IdleNumber Max(IdleNumber a, IdleNumber b)
        {

            if (a > b) return a;
            return b;

        }
        
        public static IdleNumber Min(IdleNumber a, IdleNumber b)
        {

            if (a < b) return a;
            return b;

        }
        
    }
}