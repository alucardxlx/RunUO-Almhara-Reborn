using System;
using Server.Network;
using Server.Prompts;
using Server.Items;

namespace Server.Items
{
	public class CrimsonGear : Item
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public CrimsonGear() : this( 1 )
		{
		}

		[Constructable]
		public CrimsonGear( int amount ) : base( 0x1053 )
		{
			Name = "Crimson Gear";
                  Hue = 1608;
			Stackable = true;
			Amount = amount;
		}

		public CrimsonGear( Serial serial ) : base( serial )
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

		public override void OnDoubleClick( Mobile m )
		{
			m.SendMessage( "an ingredient used for gem crafting" );
		}
	}
}