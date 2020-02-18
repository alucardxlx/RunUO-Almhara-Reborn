using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 15898, 15899 )]
	public class DemonscaleLegs : BaseArmor, IDyable
	{
		public override int BasePhysicalResistance{ get{ return 21; } }

		public override int InitMinHits{ get{ return 50; } }
		public override int InitMaxHits{ get{ return 65; } }

		public override int AosStrReq{ get{ return 115; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public DemonscaleLegs() : base( 15898 )
		{
                        Name = "Demonscale Legs - (Lv. 70)";
			Weight = 7.0;
			Layer = Layer.Pants;

			Attributes.BonusHits = 150;
		}

		public override bool CanEquip( Mobile from )
		{
			PlayerMobile pm = from as PlayerMobile;

                        if ( pm.Level >= 70 )
			{
				return true;
			} 
			else
			{
				from.SendMessage( "You must reach at least level 70 in order to equip this." );
				return false;
			}
		}

		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;

			Hue = sender.DyedHue;

			return true;
		}

		public DemonscaleLegs( Serial serial ) : base( serial )
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








