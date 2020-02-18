using System;
using Server;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
	[FlipableAttribute( 15381, 15382 )]
	public class SkeletalSpear : BaseSpear
	{
		public override int AosStrengthReq{ get{ return 30; } }
		public override int AosMinDamage{ get{ return 32; } }
		public override int AosMaxDamage{ get{ return 70; } }
		public override int AosSpeed{ get{ return 46; } }
		public override float MlSpeed{ get{ return 2.75f; } }

		public override int InitMinHits{ get{ return 15; } }
		public override int InitMaxHits{ get{ return 25; } }

		public override int DefMaxRange{ get{ return 2; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Pierce1H; } }

		[Constructable]
		public SkeletalSpear() : base( 15381 )
		{
                        Name = "Skeletal Spear - (Lv. 35)";
			Weight = 7.0;
			Layer = Layer.TwoHanded;
		}

		public override bool CanEquip( Mobile from )
		{
			PlayerMobile pm = from as PlayerMobile;

                        if ( pm.Level >= 35 )
			{
				return true;
			} 
			else
			{
				from.SendMessage( "You must reach at least level 35 in order to equip this." );
				return false;
			}
		}

		public SkeletalSpear( Serial serial ) : base( serial )
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