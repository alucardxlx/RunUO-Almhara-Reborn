using System;
using Server.Items;

namespace Server.Items
{
	public class FemaleGargishLeatherChest : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 3; } }

		public override int InitMinHits{ get{ return 30; } }
		public override int InitMaxHits{ get{ return 40; } }

		public override int AosStrReq{ get{ return 15; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public FemaleGargishLeatherChest() : base( 0x0303 )
		{
                  Name = "Female Gargish Leather Chest";
			Weight = 6.0;
			Attributes.BonusStam = 15;
		}

		public override bool CanEquip( Mobile from )
		{
                  if ( from.Female == true && from.BodyValue == 667 )
			{
				return true;
			} 

			else
			{
				from.SendMessage( "Only a female gargoyle can equip this." );
				return false;
			}
		}

		public FemaleGargishLeatherChest( Serial serial ) : base( serial )
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

			if ( Weight == 1.0 )
				Weight = 6.0;
		}
	}
}