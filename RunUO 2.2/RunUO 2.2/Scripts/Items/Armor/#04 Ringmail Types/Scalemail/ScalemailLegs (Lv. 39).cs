using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 15315, 15316 )]
	public class ScalemailLegs : BaseArmor, IDyable
	{
		public override int BasePhysicalResistance{ get{ return 14; } }

		public override int InitMinHits{ get{ return 40; } }
		public override int InitMaxHits{ get{ return 50; } }

		public override int AosStrReq{ get{ return 65; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Ringmail; } }

		[Constructable]
		public ScalemailLegs() : base( 15315 )
		{
                        Name = "Scalemail Legs - (Lv. 39)";
			Weight = 15.0;
			Layer = Layer.Pants;
			Attributes.BonusHits = 35;
		}

		public override bool CanEquip( Mobile from )
		{
			PlayerMobile pm = from as PlayerMobile;

                        if ( pm.Level >= 39 )
			{
				return true;
			} 
			else
			{
				from.SendMessage( "You must reach at least level 39 in order to equip this." );
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

		public ScalemailLegs( Serial serial ) : base( serial )
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