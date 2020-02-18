using System;
using Server;
using Server.Items;

namespace Server.Items
{
	[Flipable( 0x1516, 0x1531 )]
	public class Skirt : BaseOuterLegs
	{
		public override int InitMinHits{ get{ return 21; } }
		public override int InitMaxHits{ get{ return 24; } }

		[Constructable]
		public Skirt() : this( 0 )
		{
		}

		[Constructable]
		public Skirt( int hue ) : base( 0x1516, hue )
		{
			Weight = 4.0;
		}

		public Skirt( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}