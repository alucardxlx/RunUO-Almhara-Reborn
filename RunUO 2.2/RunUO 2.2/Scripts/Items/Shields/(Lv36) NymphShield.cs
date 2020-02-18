using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
      [FlipableAttribute( 14400, 14401 )]
	public class NymphShield : BaseShield, IDyable
	{
		public override int BasePhysicalResistance{ get{ return 13; } }

		public override int InitMinHits{ get{ return 40; } }
		public override int InitMaxHits{ get{ return 45; } }

		public override int AosStrReq{ get{ return 65; } }

		[Constructable]
		public NymphShield() : base( 14400 )
		{
                        Name = "Nymph Shield - (Lv. 36)";
			Weight = 4.0;
			Layer = Layer.TwoHanded;
			Attributes.DefendChance = 13;
		}

		public override bool CanEquip( Mobile from )
		{
			PlayerMobile pm = from as PlayerMobile;

                        if ( pm.Level >= 36 )
			{
				return true;
			} 
			else
			{
				from.SendMessage( "You must reach at least level 36 in order to equip this." );
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

		public NymphShield( Serial serial ) : base(serial)
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}
	}
}
