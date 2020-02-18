using Server;
using System;
using Server.Items;

namespace Server.Items
{
	public class ApprenticeSleeves : LeatherArms
	{
		public override int InitMinHits{ get{ return 50; } }
		public override int InitMaxHits{ get{ return 50; } }
		
		public override int BasePhysicalResistance{ get{ return 5; } }
		public override int BaseFireResistance{ get{ return 4; } }
		public override int BaseColdResistance{ get{ return 4; } }
		public override int BasePoisonResistance{ get{ return 3; } }
		public override int BaseEnergyResistance{ get{ return 4; } }

		public override int AosStrReq{ get{ return 5; } }
		
		[Constructable]
		public ApprenticeSleeves() 
		{
			Name = "Apprentice Sleeves";
			Hue = 57;
			LootType = LootType.Blessed;
			
			Attributes.BonusHits = 10;
			Attributes.BonusMana = 5;
			Attributes.Luck = 5;
		} 

		public ApprenticeSleeves( Serial serial ) : base( serial )
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
