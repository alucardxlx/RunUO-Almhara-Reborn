using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class HamPineapplePizza : Food
	{
		[Constructable]
		public HamPineapplePizza() : this( 1 )
		{
		}

		[Constructable]
		public HamPineapplePizza( int amount ) : base( amount, 0x1040 )
		{
			this.Weight = 1.0;
			this.FillFactor = 3;
                        this.Name = "Ham and Pineapple Pizza";
		}

		public HamPineapplePizza( Serial serial ) : base( serial )
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