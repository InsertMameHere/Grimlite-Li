using Grimoire.Botting.Commands.Combat;
using Grimoire.Game;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grimoire.Networking.Handlers
{
    public class HandlerAutoZoneTheFirstSpeaker : IJsonMessageHandler
    {
        public string[] HandledCommands { get; } = { "ct" };

        private int truthCount = 0;
        private int listenCount = 0;
        private int equalCount = 0;
        private static int[] GenerateSequence(int start, int step, int count)
        {
            return Enumerable.Range(0, count).Select(i => start + i * step).ToArray();
        }

        private readonly Dictionary<string, int[]> truthMappings = new Dictionary<string, int[]>
        {
            { "LEGION REVENANT", new[] { 1, 4 }.Concat(GenerateSequence(8, 4, 23)).ToArray() },
            { "LORD OF ORDER", GenerateSequence(5, 4, 25) },
            { "ARCHPALADIN", GenerateSequence(2, 4, 25) },
            { "STONECRUSHER", GenerateSequence(3, 4, 25) },
            { "QUANTUM CHRONOMANCER", GenerateSequence(3, 4, 25) },
            { "VERUS DOOMKNIGHT", GenerateSequence(3, 4, 25) }
        };

        private readonly Dictionary<string, int[]> listenMappings = new Dictionary<string, int[]>
        {
            { "LEGION REVENANT", GenerateSequence(3, 3, 25) },
            { "LORD OF ORDER", GenerateSequence(1, 3, 25) },
            { "STONECRUSHER", GenerateSequence(2, 3, 25) },
            { "QUANTUM CHRONOMANCER", GenerateSequence(2, 3, 25) },
            { "VERUS DOOMKNIGHT", GenerateSequence(2, 3, 25) }
        };

        private readonly Dictionary<string, int[]> equalMappings = new Dictionary<string, int[]>
        {
            { "LEGION REVENANT", GenerateSequence(2, 4, 25) },
            { "LORD OF ORDER", GenerateSequence(4, 4, 25) },
            { "ARCHPALADIN", GenerateSequence(3, 4, 25) },
            { "STONECRUSHER", GenerateSequence(1, 4, 25) },
            { "QUANTUM CHRONOMANCER", GenerateSequence(1, 4, 25) },
            { "VERUS DOOMKNIGHT", GenerateSequence(1, 4, 25) }
        };

        public async void Handle(JsonMessage message)
        {
            try
            {
                JArray animsArray = (JArray)message.DataObject["anims"];
                string customMessage = animsArray[0]["msg"].ToString();

                switch (customMessage)
                {
                    case "I will make you see the truth.":
                        Player.WalkToPoint("893", "257");
                        truthCount++;
                        if (truthMappings.TryGetValue(Player.EquippedClass, out int[] truthArray) && truthArray.Contains(truthCount))
                        {
                            for (int i = 0; i < 25; i++)
                            {
                                Player.UseSkill("5");
                                await Task.Delay(100);
                            }
                        }
                        break;
                    case "You shall listen.":
                        Player.WalkToPoint("893", "257");
                        listenCount++;
                        if (listenMappings.TryGetValue(Player.EquippedClass, out int[] listenArray) && listenArray.Contains(listenCount))
                        {
                            for (int i = 0; i < 25; i++)
                            {
                                Player.UseSkill("5");
                                await Task.Delay(100);
                            }
                        }
                        break;
                    case "All stand equal beneath the eyes of the Eternal.":
                        equalCount++;
                        if (equalMappings.TryGetValue(Player.EquippedClass, out int[] equalArray) && equalArray.Contains(equalCount))
                        {
                            Player.WalkToPoint("489", "273");
                        }
                        break;
                    default:
                        Player.WalkToPoint("893", "257");
                        break;
                }
            }
            catch
            {
                // Handle exceptions appropriately (e.g., log the error)
            }
        }
    }
}
