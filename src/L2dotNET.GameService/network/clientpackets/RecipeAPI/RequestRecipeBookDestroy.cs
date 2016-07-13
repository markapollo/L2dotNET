﻿using System.Linq;
using L2dotNET.GameService.Config;
using L2dotNET.GameService.Model.Player;
using L2dotNET.GameService.Network.Serverpackets;
using L2dotNET.GameService.Tables;
using L2dotNET.Network;

namespace L2dotNET.GameService.Network.Clientpackets.RecipeAPI
{
    class RequestRecipeBookDestroy : PacketBase
    {
        public RequestRecipeBookDestroy(Packet packet, GameClient client)
        {
            _client = client;
        }

        private int _id;

        public override void Read()
        {
            _id = packet.ReadInt();
        }

        public override void RunImpl()
        {
            L2Player player = Client.CurrentPlayer;

            if (player.RecipeBook == null)
            {
                player.SendSystemMessage(SystemMessage.SystemMessageId.RecipeIncorrect);
                player.SendActionFailed();
                return;
            }

            L2Recipe rec = player.RecipeBook.FirstOrDefault(r => r.RecipeId == _id);

            if (rec == null)
            {
                player.SendSystemMessage(SystemMessage.SystemMessageId.RecipeIncorrect);
                player.SendActionFailed();
                return;
            }

            player.UnregisterRecipe(rec, true);
        }
    }
}