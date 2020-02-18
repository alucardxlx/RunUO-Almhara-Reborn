using System;
using Server;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Network;
using Server.Spells;
using Server.Spells.Tome;
using Server.Targeting;

namespace Server.Items
{
	public class SummonPlantScroll : Item
	{
		[Constructable]
		public SummonPlantScroll() : this( 1 )
		{
		}

		[Constructable]
		public SummonPlantScroll( int amount ) : base( 0x2261 )
		{
			Name = "Summon Plant - 6th Circle";
			Stackable = true;
			Amount = amount;

			Weight = 0.1;
                        Hue = 83;

			LootType = LootType.Blessed;
		}

		public override void OnDoubleClick( Mobile from )
		{
                        new SummonPlantSpell ( from, this ).Cast();
		}

		public SummonPlantScroll( Serial serial ) : base( serial )
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

namespace Server.Spells.Tome
{
	public class SummonPlantSpell : TomeSpell 
	{ 
		private static SpellInfo m_Info = new SpellInfo( 
				"Summon Plant", "summon plant",
				-1,
				9002
			); 

                public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }
                public override SpellCircle Circle { get { return SpellCircle.Sixth; } }
                public override double RequiredSkill { get { return 50.0; } }
                public override int RequiredMana { get { return 60; } }

		public SummonPlantSpell( Mobile caster, Item scroll ): base( caster, scroll, m_Info )
		{ 
		} 
		
		// NOTE: Creature list based on 1hr of summon/release on OSI.

		private static Type[] m_Types = new Type[]
			{
				typeof( DarkRose ),
				typeof( DesertRose ),
				typeof( Quagmire ),
				typeof( Umdhlebi )
			};

		public override bool CheckCast()
		{
			if ( !base.CheckCast() )
				return false;

			if ( (Caster.Followers + 2) > Caster.FollowersMax )
			{
				Caster.SendLocalizedMessage( 1049645 ); // You have too many followers to summon that creature.
				return false;
			}

			return true;
		}

		public override void OnCast()
		{
			if ( CheckSequence() )
			{
				try
				{
					BaseCreature creature = (BaseCreature)Activator.CreateInstance( m_Types[Utility.Random( m_Types.Length )] );

					//creature.ControlSlots = 2;

					TimeSpan duration;

					duration = TimeSpan.FromHours( 1.0 );

					SpellHelper.Summon( creature, Caster, 0x215, duration, false, false );
				}
				catch
				{
				}
			}

			FinishSequence();
		}

		public override TimeSpan GetCastDelay()
		{
			if ( Core.AOS )
				return TimeSpan.FromTicks( base.GetCastDelay().Ticks * 5 );

			return base.GetCastDelay() + TimeSpan.FromSeconds( 6.0 );
		}
	}
}