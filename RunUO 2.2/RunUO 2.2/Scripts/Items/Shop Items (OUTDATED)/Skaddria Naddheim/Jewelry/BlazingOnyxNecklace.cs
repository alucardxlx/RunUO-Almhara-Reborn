using System;
using Server;

namespace Server.Items
{
	public class BlazingOnyxNecklace : BaseNecklace
	{
		[Constructable]
		public BlazingOnyxNecklace() : base( 0x1085 )
		{
                        Name = "Blazing Onyx Necklace";
                        Hue = 1161;
			Weight = 0.1;

			Resistances.Fire = 5;
		}

		public BlazingOnyxNecklace( Serial serial ) : base( serial )
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