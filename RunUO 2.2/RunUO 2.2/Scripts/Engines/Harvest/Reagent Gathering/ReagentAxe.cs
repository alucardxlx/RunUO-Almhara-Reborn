using System;
using Server.Items;
using Server.Network;
using Server.Engines.Harvest;

namespace Server.Items
{
	[FlipableAttribute( 0xF43, 0xF44 )]
	public class ReagentAxe : BaseAxe, IUsesRemaining
	{
                public override HarvestSystem HarvestSystem { get { return ReagentGathering.System; } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ArmorIgnore; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Disarm; } }

		public override int AosStrengthReq{ get{ return 15; } }
		public override int AosMinDamage{ get{ return 2; } }
		public override int AosMaxDamage{ get{ return 17; } }
		public override int AosSpeed{ get{ return 40; } }
		public override float MlSpeed{ get{ return 2.75f; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 80; } }

		[Constructable]
		public ReagentAxe() : base( 0xF43 )
		{
                        Name = "Reagent Gathering Axe";
                        Hue = 64;
                        Weight = 4.0;
                        UsesRemaining = 100;
                        ShowUsesRemaining = true;
		}

		public ReagentAxe( Serial serial ) : base( serial )
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
                        ShowUsesRemaining = true;
		}
	}
}