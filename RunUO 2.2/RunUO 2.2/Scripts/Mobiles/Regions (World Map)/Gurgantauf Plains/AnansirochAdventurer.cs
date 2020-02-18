using System;
using System.Collections;
using Server.ContextMenus;
using Server.Items;
using Server.Misc;
using Server.Network;
using Server.Targeting;

namespace Server.Mobiles
{
	public class AnansirochAdventurer : BaseGuardian
	{
		public override WeaponAbility GetWeaponAbility()
		{
			switch ( Utility.Random( 3 ) )
			{
				default:
				case 0: return WeaponAbility.DoubleStrike;
				case 1: return WeaponAbility.WhirlwindAttack;
				case 2: return WeaponAbility.CrushingBlow;
			}
		}

		[Constructable]
		public AnansirochAdventurer() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Title = "the Anansiroch Adventurer"; 
			Hue = Utility.RandomList( 3,28,38,48,53,58,63,88,93 );

			SetStr( 128, 160 );
			SetDex( 87, 100 );
			SetInt( 32, 35 );

			SetHits( 225, 350 );

			SetSkill( SkillName.Anatomy, 20.0 );
			SetSkill( SkillName.Fencing, 82.6, 93.1 );
			SetSkill( SkillName.Healing, 48.2, 61.9 );
			SetSkill( SkillName.Macing, 82.6, 93.1 );
			SetSkill( SkillName.MagicResist, 38.9, 45.5 );
			SetSkill( SkillName.Swords, 82.6, 93.1 );
			SetSkill( SkillName.Tactics, 48.2, 61.9 );
			SetSkill( SkillName.Wrestling, 49.4, 57.6 );

			Fame = -2500;
			Karma = 2500;

			if ( this.Female = Utility.RandomBool() )
			{
				Body = 386;
				Name = NameList.RandomName( "female" );
			}
			else
			{
				Body = 390;
				Name = NameList.RandomName( "male" );
			}

			switch ( Utility.Random( 8 ))
			{
				case 0: AddItem( new Cutlass() ); break;
				case 1: AddItem( new Kryss() ); break;
				case 2: AddItem( new Longsword() ); break;
				case 3: AddItem( new Scimitar() ); break;
				case 4: AddItem( new VikingSword() ); break;
				case 5: AddItem( new Club() ); break;
				case 6: AddItem( new Mace() ); break;
				case 7: AddItem( new WarMace() ); break;
			}

			switch ( Utility.Random( 6 ))
			{
				case 0: AddItem( new BronzeShield() ); break;
				case 1: AddItem( new Buckler() ); break;
				case 2: AddItem( new MetalKiteShield() ); break;
				case 3: AddItem( new MetalShield() ); break;
				case 4: AddItem( new WoodenKiteShield() ); break;
				case 5: AddItem( new WoodenShield() ); break;
			}

			Container pack = new Backpack();

			pack.DropItem( new Pitcher( BeverageType.Water ) );
			pack.DropItem( new Gold( Utility.RandomMinMax( 5, 8 ) ) );
			pack.DropItem( new Bandage( Utility.RandomMinMax( 5, 10 ) ) );

			PackItem( pack );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Potions );
		}

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			if ( from != null && !willKill && amount > 5 && from.Player && 5 > Utility.Random( 100 ) )
			{
				string[] toSay = new string[]
					{
						"{0}!!  I have had enough of you foolish fiend!",
						"{0}!!  Prepare to face my fury!",
						"{0}!!  Why won't you die?!",
						"{0}!!  Stay and face me fool!"
					};

				this.Say( true, String.Format( toSay[Utility.Random( toSay.Length )], from.Name ) );
			}

			base.OnDamage( amount, from, willKill );
		}


		public AnansirochAdventurer( Serial serial ) : base( serial )
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