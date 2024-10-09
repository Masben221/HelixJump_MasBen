namespace HelixJump
{
    public enum Sound

    {        
        Jump = 0,
        Fly = 1,
        Bolt = 2,        
        Toilet = 3,
        PistonHit = 4,
        FireHit = 5,
        BombHit = 6,
        SpikeHit = 7,
        Finish = 8,
        Die = 9,        
        FloorDestroy = 10,
        Button = 11,
        
        BGM_1 = 12,
        BGM_2 = 13,

        AddShield = 14,
        AddHP = 15,
        Shield = 16,
        Damage = 17,
        SuperPower = 18,
        AddLife = 19,
        AddSuperPower = 20        
    }

    public static class SoundExtensions
    {
        public static void Play(this Sound sound)
        {
            SoundPlayer.Instance.Play(sound);
        }
        public static void Stop(this Sound sound)
        {
            SoundPlayer.Instance.Stop(sound);
        }
        public static void PlayLoop(this Sound sound)
        {
            SoundPlayer.Instance.PlayLoop(sound);
        }
    }
}