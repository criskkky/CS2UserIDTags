using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Core.Attributes.Registration;

namespace CS2UserIDTags
{
    [MinimumApiVersion(142)]
    public class CS2UserIDTags : BasePlugin
    {
        public override string ModuleName => "CS2UserIDTags";
        public override string ModuleDescription => "Adds the UserID to the client's clan tag";
        public override string ModuleAuthor => "criskkky";
        public override string ModuleVersion => "1.0.0";

        public override void Load(bool hotReload)
        {
            RegisterEventHandler<EventPlayerConnectFull>(OnPlayerConnectFull);
        }

        private HookResult OnPlayerConnectFull(EventPlayerConnectFull @event, GameEventInfo info)
        {
            CCSPlayerController? player = @event.Userid;

            if (IsValidPlayer(player))
            {
                ApplyClanTag(player);
            }

            return HookResult.Continue;
        }

        private bool IsValidPlayer(CCSPlayerController? player)
        {
            return player != null && player.IsValid && !player.IsBot;
        }

        private void ApplyClanTag(CCSPlayerController player)
        {
            int userID = player.UserId ?? -1;
            string clanTag = $"[#{userID}]";
            player.Clan = clanTag;
        }
    }
}