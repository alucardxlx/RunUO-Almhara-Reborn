/*
LoggedQuestSystem with two example quests
Copyright (C) 2006 BEYLER Jean Christophe

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/

using System;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Engines.Quests;
using Server.Fearyourself.Quests.Core;

namespace Server.Fearyourself.Quests.BunnyAttack
{
    public class Bob : BaseQuester
    {
        [Constructable]
        public Bob()
        {
        }

        public override void InitBody()
        {
            Name = "Bob";
            Body = 400;
            VirtualArmor = 50;
            CantWalk = true;
            Female = false;

            HairItemID = 0x203B;
            HairHue = 0x1;

            AddItem(new Server.Items.LongPants(0x1));
            AddItem(new Server.Items.FancyShirt(0x481)); ;
            AddItem(new Server.Items.ThighBoots(0x1));
            AddItem(new Server.Items.FeatheredHat(0x1));
            AddItem(new Server.Items.Cloak(0x485));

            Blessed = true;
        }

        public override void OnTalk(PlayerMobile player, bool contextMenu)
        {
            //Look at the player
            this.Direction = GetDirectionTo(player);

            //Look for the quest state
            LoggedQuestState state = LoggedQuestSystem.getQuestState(player, "Bunny Attack Quest");

            //Switch on the state 
            switch (state)
            {
                case LoggedQuestState.DECLINED:
                case LoggedQuestState.CANCELED:
                case LoggedQuestState.NOTINVOLVED:
                    //New quest proposal
                    //Create the quest
                    QuestSystem newQuest = new BobQuest(player);

                    if (player.Quest == null && QuestSystem.CanOfferQuest(player, typeof(BobQuest)))
                    {
                        newQuest.SendOffer();
                    }
                    else
                    {
                        newQuest.AddConversation(
                            new GenericConversation(
                                "I'm sorry but you are already occupied, come back later<br><br>" +
                                "And I'll have a quest for you"
                                )
                            );
                    }
                    break;
                case LoggedQuestState.ACCEPTED:
                    BobQuest bq = player.Quest as BobQuest;

                    //Being paranoiac, normally bq can't be null be let's be sure
                    if (bq != null)
                    {
                        //Ok we know we have a quest, are we in progress killing bunnies ?
                        if (bq.IsObjectiveInProgress(typeof(KillBunniesObjective)))
                        {
                            bq.AddConversation(new GenericConversation("You have not finished your quest"));
                        }
                        //No then we are wanting the reward
                        else
                        {
                            QuestObjective lastObj = bq.FindObjective(typeof(ReturnAfterKillsObjective));

                            //Ok normally it's got to be non null and it shouldn't be completed
                            //Yet another paranoiac test
                            if (lastObj != null && !lastObj.Completed)
                            {
                                //Give some gold
                                bool gold = true;

                                BobQuest.GiveRewardTo(player, ref gold);

                                //Check if we gave it all
                                if (!gold)
                                {
                                    //Last check, has the player done the Bob's quest ?
                                    state = LoggedQuestSystem.getQuestState(player, "Brian's Quest");
                                    if (state == LoggedQuestState.FINISHED)
                                    {
                                        bq.AddConversation(new GenericConversation("As you leave bob, you can't help to think that you have "
                                            + "killed 5 innocent rabbits... <br><br>" 
                                            +"He looks at you and says : \"Ahhhh, I see you have also helped Alfred and Brian! <br><br>" +
                                            "Why don't you go see David, he'll have a special compensation for you\""));
                                    }
                                    else
                                    {
                                        bq.AddConversation(new GenericConversation(
                                            "As you leave bob, you can't help to think that you have "
                                            + "killed 5 innocent rabbits..."
                                            )
                                        );
                                    }

                                    lastObj.Complete();
                                }
                                else //If not
                                {
                                    bq.AddConversation(new GenericConversation("Your backpack is full, make room!"));
                                }
                            }
                        }
                    }
                    else
                    {
                        //Write to console, if you see this, you made a mistake in the quest name...
                        Console.WriteLine("Quest should not be null, what happenned?");
                    }
                    break;
                case LoggedQuestState.FINISHED:
                    this.Say("You have already finished this quest, move along");
                    break;

                default:
                    break;
            }

        }

        public Bob(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
}
