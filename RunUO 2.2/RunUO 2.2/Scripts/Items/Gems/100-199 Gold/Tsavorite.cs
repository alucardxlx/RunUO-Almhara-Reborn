using System;
using Server;

namespace Server.Items
{
	public class Tsavorite : Item
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public Tsavorite() : this( 1 )
		{
		}

		[Constructable]
		public Tsavorite( int amount ) : base( 0xF1C )
		{
			Name = "Tsavorite";
			Stackable = true;
			Amount = amount;
			Hue = 1594;
		}

		public Tsavorite( Serial serial ) : base( serial )
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