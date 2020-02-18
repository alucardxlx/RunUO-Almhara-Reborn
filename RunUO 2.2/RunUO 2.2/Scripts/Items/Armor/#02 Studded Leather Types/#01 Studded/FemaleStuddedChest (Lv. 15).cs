using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 0x1c02, 0x1c03 )]
	public class FemaleStuddedChest : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 6; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 37; } }

		public override int AosStrReq{ get{ return 25; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Studded; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.Half; } }

		public override bool AllowMaleWearer{ get{ return false; } }

		[Constructable]
		public FemaleStuddedChest() : base( 0x1C02 )
		{
                        Name = "Female Studded Chest - (Lv. 15)";
			Weight = 6.0;
			Attributes.BonusHits = 7;
		}

		public override bool CanEquip( Mobile from )
		{
			PlayerMobile pm = from as PlayerMobile;

                        if ( pm.Level >= 15 )
			{
				return true;
			} 
			else
			{
				from.SendMessage( "You must reach at least level 15 in order to equip this." );
				return false;
			}
		}

		public FemaleStuddedChest( Serial serial ) : base( serial )
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