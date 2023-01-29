using MelonLoader;

namespace debug
{
    public class debug : MelonMod
    {
        public override void OnApplicationStart()
        {
            MelonLogger.Msg("Hello World!");
        }
    }
}
