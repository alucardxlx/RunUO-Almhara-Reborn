using System;
using Server;

namespace Server.Items
{
	public class BigColourfulMarble : Item
	{
		[Constructable]
		public BigColourfulMarble() : this( null )
		{
		}

		[Constructable]
		public BigColourfulMarble( string name ) : base( 0x1870 )
		{
			Name = "a big colourful marble";
			Weight = 1.0;
                        Hue = Utility.RandomBirdHue();
		}

		public BigColourfulMarble( Serial serial ) : base( serial )
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