using System;
using Server;
using Server.Network;
using Server.Items;
using System.Collections;

namespace Server.Items
{
	public class DosLlamas : BeverageBottle
	{		
		public override int MaxQuantity { get { return 1; } }

		[Constructable]
		public DosLlamas() : base( BeverageType.Wine )
		{
                  Name = "a bottle of dos llamas";
			Hue = 45;
		}

		public DosLlamas( Serial serial ) : base( serial )
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

            public override void OnDoubleClick( Mobile from )
            {
			if ( from.BeginAction( typeof( BaseHealPotion ) ) )
			{

                          from.AddSkillMod(new TimedSkillMod(SkillName.AnimalTaming, true, 9.0, TimeSpan.FromMinutes(3.0)));
			  from.SendMessage( "The dos llamas temporary boosts your Animal Taming skill." );

			  from.PlaySound( 1014 );

			  this.Delete();

					Timer.DelayCall( TimeSpan.FromMinutes( 3.0 ), new TimerStateCallback( ReleaseHealLock ), from );
				}
				else
				{
					from.SendMessage( "Oui matey, you should wait 3 minutes before you drink another one." );
				}
			}
	
		private static void ReleaseHealLock( object state )
		{
			((Mobile)state).EndAction( typeof( BaseHealPotion ) );
		}
	}
}
