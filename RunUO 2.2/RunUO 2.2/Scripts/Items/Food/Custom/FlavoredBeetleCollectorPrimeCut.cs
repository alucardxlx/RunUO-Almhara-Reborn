using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Network;
using Server.Multis;
using Server.ContextMenus;

namespace Server.Items
{
	public class FlavoredBeetleCollectorPrimeCut : Food
	{
                public override double DefaultWeight
                {
                    get { return 1.0; }
                }

		[Constructable]
		public FlavoredBeetleCollectorPrimeCut() : this( 1 )
		{
		}

		[Constructable]
		public FlavoredBeetleCollectorPrimeCut( int amount ) : base( amount, 0x4005 )
		{
                        this.Name = "Flavored Beetle Collector Prime Cut";
			this.Hue = 182;
                        this.Stackable = true;
                        this.Amount = amount;
			this.FillFactor = 1;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
				return;

			if ( from.InRange( this.GetWorldLocation(), 1 ) )
			{
				Eat( from );
			}

			if ( from.Hits < from.HitsMax )
			{
				from.Hits = Math.Min( from.Hits + 8, from.HitsMax);

				from.FixedParticles( 0x375A, 9, 16, 5007, EffectLayer.Waist );

				from.PlaySound( Utility.Random( 0x3A, 3 ) );
				from.PlaySound( 0x1EE );
				from.SendMessage("You've recovered 8 hit points.");
			}
			else
			{
				from.Say( "*burp!*" );
				from.PlaySound( from.Female ? 782 : 1053 );
			}
		}

		public FlavoredBeetleCollectorPrimeCut( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}