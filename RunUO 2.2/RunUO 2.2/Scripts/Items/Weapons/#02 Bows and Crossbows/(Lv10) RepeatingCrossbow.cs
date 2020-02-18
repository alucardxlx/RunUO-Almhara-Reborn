using System;
using Server;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
	[FlipableAttribute( 0x26C3, 0x26CD )]
	public class RepeatingCrossbow : BaseRanged
	{
		public override Type TypeUsed{ get{ return typeof( Bolt ); } }
		
		public override int EffectID{ get{ return 0x1BFE; } }
		public override Type AmmoType{ get{ return TypeSelected( TypeUsed ); } }		
		public override Item Ammo{ get{ return AmmoSelected( TypeUsed ); } }

		public override int AosStrengthReq{ get{ return 30; } }
		public override int AosMinDamage{ get{ return 8; } }
		public override int AosMaxDamage{ get{ return 20; } }
		public override int AosSpeed{ get{ return 41; } }
		public override float MlSpeed{ get{ return 2.75f; } }

		public override int DefMaxRange{ get{ return 7; } }

		public override int InitMinHits{ get{ return 15; } }
		public override int InitMaxHits{ get{ return 25; } }

		[Constructable]
		public RepeatingCrossbow() : base( 0x26C3 )
		{
                        Name = "Repeating Crossbow - (Lv. 10)";
			Weight = 6.0;
		}

		public override bool CanEquip( Mobile from )
		{
			PlayerMobile pm = from as PlayerMobile;

                        if ( pm.Level >= 10 )
			{
				return true;
			} 
			else
			{
				from.SendMessage( "You must reach at least level 10 in order to equip this." );
				return false;
			}
		}

		public RepeatingCrossbow( Serial serial ) : base( serial )
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