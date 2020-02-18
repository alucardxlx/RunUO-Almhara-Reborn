using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class SewerBeef : Food
	{
		[Constructable]
		public SewerBeef() : this( 1 )
		{
		}

		[Constructable]
		public SewerBeef( int amount ) : base( amount, 0x9F2 )
		{
			this.Name = "Sewer Beef";
			this.Hue = 2126;
                        this.Stackable = true;
                        this.Amount = amount;

			this.Weight = 1.0;
			this.FillFactor = 1;
                        this.Poison = Poison.Lesser;
		}

		public SewerBeef( Serial serial ) : base( serial )
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