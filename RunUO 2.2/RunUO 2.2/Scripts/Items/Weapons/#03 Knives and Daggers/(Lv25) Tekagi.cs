using System;
using Server;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
	[FlipableAttribute( 0x27Ab, 0x27F6 )]
	public class Tekagi : BaseKnife
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.DualWield; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.TalonStrike; } }

		public override int AosStrengthReq{ get{ return 10; } }
		public override int AosMinDamage{ get{ return 25; } }
		public override int AosMaxDamage{ get{ return 30; } }
		public override int AosSpeed{ get{ return 53; } }
		public override float MlSpeed{ get{ return 2.00f; } }

		public override int DefHitSound{ get{ return 0x238; } }
		public override int DefMissSound{ get{ return 0x232; } }

		public override int InitMinHits{ get{ return 15; } }
		public override int InitMaxHits{ get{ return 25; } }

		public override SkillName DefSkill{ get{ return SkillName.Fencing; } }
		public override WeaponType DefType{ get{ return WeaponType.Piercing; } }
		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Pierce1H; } }

		[Constructable]
		public Tekagi() : base( 0x27AB )
		{
                        Name = "Tekagi - (Lv. 25)";
			Weight = 5.0;
			Layer = Layer.TwoHanded;
		}

		public override bool CanEquip( Mobile from )
		{
			PlayerMobile pm = from as PlayerMobile;

                        if ( pm.Level >= 25 )
			{
				return true;
			} 
			else
			{
				from.SendMessage( "You must reach at least level 25 in order to equip this." );
				return false;
			}
		}

		public Tekagi( Serial serial ) : base( serial )
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