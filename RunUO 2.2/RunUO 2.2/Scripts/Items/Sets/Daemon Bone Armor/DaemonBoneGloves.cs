using System;
using Server.Items;

namespace Server.Items
{
	public class DaemonBoneGloves : BoneGloves
	{
		public override int BasePhysicalResistance{ get{ return 10; } }
		public override int BaseFireResistance{ get{ return 10; } }
		public override int BaseColdResistance{ get{ return 10; } }
		public override int BasePoisonResistance{ get{ return 10; } }
		public override int BaseEnergyResistance{ get{ return 10; } }

		[Constructable]
		public DaemonBoneGloves() 
		{
                        Name = "Daemon Bone Gloves";
			Hue = 0x648;

			SkillBonuses.SetValues( 0, SkillName.MagicResist, 1.0 );
		}

		public DaemonBoneGloves( Serial serial ) : base( serial )
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