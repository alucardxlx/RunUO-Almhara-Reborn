using System;
using Server;

namespace Server.Items
{
	public class SkeletalAxe : Hatchet
	{
		[Constructable]
		public SkeletalAxe() 
		{
                      Name = "Skeletal Axe";
                      Hue = 966;

		      Slayer = SlayerName.Silver;
		      Attributes.WeaponSpeed = 5;
		}

		public override bool OnEquip( Mobile m )
		{
		this.ItemID = 0xF43;
		return true;
		}

		public override void OnRemoved( object parent )
		{
		this.ItemID = 0x255F;
		}

		public SkeletalAxe( Serial serial ) : base( serial )
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
