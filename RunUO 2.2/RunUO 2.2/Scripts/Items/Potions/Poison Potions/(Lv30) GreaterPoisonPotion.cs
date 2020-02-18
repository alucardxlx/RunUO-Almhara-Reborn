using System;
using Server;

namespace Server.Items
{
	public class GreaterPoisonPotion : BasePoisonPotion
	{
		public override Poison Poison{ get{ return Poison.Greater; } }

		public override double MinPoisoningSkill{ get{ return 50.0; } }
		public override double MaxPoisoningSkill{ get{ return 90.0; } }

		[Constructable]
		public GreaterPoisonPotion() : base( PotionEffect.PoisonGreater )
		{
                	Name = "Greater Green Potion";
		}

		public GreaterPoisonPotion( Serial serial ) : base( serial )
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