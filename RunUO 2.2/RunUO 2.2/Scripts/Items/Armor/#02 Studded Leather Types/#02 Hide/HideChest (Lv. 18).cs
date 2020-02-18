using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 0x2B74, 0x316B )]
	public class HideChest : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 7; } }

		public override int InitMinHits{ get{ return 35; } }
		public override int InitMaxHits{ get{ return 45; } }

		public override int AosStrReq{ get{ return 30; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Studded; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.Half; } }

		[Constructable]
		public HideChest() : base( 0x2B74 )
		{
                        Name = "Hide Chest - (Lv. 18)";
			Weight = 6.0;
			Attributes.BonusHits = 10;
		}

		public override bool CanEquip( Mobile from )
		{
			PlayerMobile pm = from as PlayerMobile;

                        if ( pm.Level >= 18 )
			{
				return true;
			} 
			else
			{
				from.SendMessage( "You must reach at least level 18 in order to equip this." );
				return false;
			}
		}

		public HideChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
}