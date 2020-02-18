using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a lizardman's corpse" )]
	public class Shezothin : BaseCreature
	{
		[Constructable]
		public Shezothin() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Shezothin";
			Body = 35;
			BaseSoundID = 417;

			SetStr( 42, 54 );
			SetDex( 46, 65 );
			SetInt( 16, 30 );

			SetHits( 138, 143 );
			SetMana( 0 );

			SetDamage( 9, 12 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 27, 32 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Poison, 15, 25 );

			SetSkill( SkillName.MagicResist, 32.1, 37.0 );
			SetSkill( SkillName.Tactics, 42.3, 56.0 );
			SetSkill( SkillName.Wrestling, 62.3, 76.0 );

			Fame = 900;
			Karma = -900;
		}
		
		public override bool AlwaysMurderer{ get{ return true; } }

		public Shezothin( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}