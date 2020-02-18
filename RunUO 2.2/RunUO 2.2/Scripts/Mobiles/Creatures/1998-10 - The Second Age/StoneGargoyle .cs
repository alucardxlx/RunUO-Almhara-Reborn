using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a gargoyle corpse" )]
	public class StoneGargoyle : BaseCreature
	{
		[Constructable]
		public StoneGargoyle() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a stone gargoyle";
			Body = 67;
			BaseSoundID = 0x174;

			SetStr( 246, 275 );
			SetDex( 76, 95 );
			SetInt( 81, 105 );

			SetHits( 296, 330 );

			SetDamage( 11, 17 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 50 );
			SetResistance( ResistanceType.Fire, 20 );
			SetResistance( ResistanceType.Cold, 10 );
			SetResistance( ResistanceType.Poison, 30 );
			SetResistance( ResistanceType.Energy, 30 );

			SetSkill( SkillName.MagicResist, 85.1, 100.0 );
			SetSkill( SkillName.Tactics, 80.1, 100.0 );
			SetSkill( SkillName.Wrestling, 60.1, 100.0 );

			Fame = 4000;
			Karma = -4000;

			PackGold( 23, 29 );

			PackItem( new IronIngot( 12 ) );

			if ( 0.05 > Utility.RandomDouble() )
				PackItem( new GargoylesPickaxe() );

			PackItem( new Peridot() );
			PackItem( new FishScale( Utility.RandomMinMax( 12, 17 ) ) );

 			if ( Utility.RandomDouble() < 0.10 )
				PackItem( new BlessScroll() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average, 2 );
			AddLoot( LootPack.Potions );
		}

		public StoneGargoyle( Serial serial ) : base( serial )
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