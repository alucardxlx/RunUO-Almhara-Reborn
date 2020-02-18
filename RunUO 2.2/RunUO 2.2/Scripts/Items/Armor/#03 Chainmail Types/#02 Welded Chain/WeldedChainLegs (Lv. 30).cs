using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 15343, 15344 )]
	public class WeldedChainLegs : BaseArmor, IDyable
	{
		public override int BasePhysicalResistance{ get{ return 11; } }

		public override int InitMinHits{ get{ return 45; } }
		public override int InitMaxHits{ get{ return 60; } }

		public override int AosStrReq{ get{ return 50; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Chainmail; } }

		[Constructable]
		public WeldedChainLegs() : base( 15343 )
		{
                        Name = "Welded Chain Legs - (Lv. 30)";
			Weight = 7.0;
			Layer = Layer.Pants;
			Attributes.BonusHits = 18;
		}

		public override bool CanEquip( Mobile from )
		{
			PlayerMobile pm = from as PlayerMobile;

                        if ( pm.Level >= 30 )
			{
				return true;
			} 
			else
			{
				from.SendMessage( "You must reach at least level 30 in order to equip this." );
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

		public WeldedChainLegs( Serial serial ) : base( serial )
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