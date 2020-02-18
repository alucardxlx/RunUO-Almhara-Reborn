using System;
using System.Collections.Generic;
using System.Text;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Engines.MLQuests.Items
{
	[FlipableAttribute( 0x13ee, 0x13ef )]
	public class SerpentineJadeRingmailSleeves : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 13; } }
		public override int BasePoisonResistance{ get{ return 11; } }

		public override int InitMinHits{ get{ return 40; } }
		public override int InitMaxHits{ get{ return 50; } }

		public override int AosStrReq{ get{ return 10; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Ringmail; } }

		[Constructable]
		public SerpentineJadeRingmailSleeves() : base( 0x13EE )
		{
			Name = "Serpentine Jade Ringmail Sleeves - (Lv. 5)";
			Weight = 15.0;
			Hue = 61;
			Attributes.BonusHits = 28;

			LootType = LootType.Blessed;
		}

		public override bool CanEquip( Mobile from )
		{
			PlayerMobile pm = from as PlayerMobile;

                        if ( pm.Level >= 5 )
			{
				return true;
			} 
			else
			{
				from.SendMessage( "You must reach at least level 5 in order to equip this." );
				return false;
			}
		}

		public SerpentineJadeRingmailSleeves( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}