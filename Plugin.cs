using BepInEx;

namespace VoiceAdd
{
    [BepInPlugin("com.CWX.VoiceAdder.Boss", "CWX-VoiceAdderBoss", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private void Start()
        {
            new BossVoicePatch().Enable();
        }
    }
}