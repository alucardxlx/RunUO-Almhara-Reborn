using System;
using Server;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
	public class CrackedHitLeechStamGem : BaseHitLeechStamSocketGem
	{
		private int m_RequiredLevel = 1;
		private int m_HitLeechStam = 5;

		[CommandProperty( AccessLevel.GameMaster )]
		public override int RequiredLevel
		{ 
			get{ return m_RequiredLevel; }
			set {m_RequiredLevel = value; InvalidateProperties();}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public override int HitLeechStam
		{ 
			get{ return m_HitLeechStam; }
			set {m_HitLeechStam = value; InvalidateProperties();}
		}

		[Constructable]
		public CrackedHitLeechStamGem() : base( 0x3198 )
		{
			Name = "Cracked Hit Leech Stam Gem";
			Weight = 1.0;
			Hue = 271;
		}

		public CrackedHitLeechStamGem( Serial serial ) : base( serial )
		{
		}

		private string m_name ="Cracked Hit Leech Stam Gem";

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( m_name );
			writer.Write( m_RequiredLevel );
			writer.Write( m_HitLeechStam );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_name = reader.ReadString();
			m_RequiredLevel = reader.ReadInt();
			m_HitLeechStam = reader.ReadInt();
		}

		public override void OnDoubleClick( Mobile from )
		{
			PlayerMobile pm = from as PlayerMobile;

                        if ( pm.Level < RequiredLevel )
			{
				from.SendMessage( "Your level isn't high enough to use this gem." );
				return;
			}

			if ( IsChildOf( from.Backpack ) )
			{
				from.Target = new InternalTarget( this );
			}
                        else
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
		}

		private class InternalTarget : Target
		{
			private CrackedHitLeechStamGem m_deed;

			public InternalTarget( CrackedHitLeechStamGem deed ) : base( 3, false, TargetFlags.None )
			{
				m_deed = deed;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted == null )
					return;

				if (!(targeted is Item))
				{
					from.SendMessage("This gem can only be used on weapons.");
					return;
				}
				
				if ( m_deed.Deleted )
					return;
				
				if(!(targeted as Item).IsChildOf(from.Backpack))
				{
					from.SendMessage("You can use this gem only on items in your backpack");
					return;
				}
				
				if ( targeted is BaseWeapon )
				{
					BaseWeapon w = targeted as BaseWeapon;

					if ( w !=null )
					{
						if ( w.WeaponAttributes.HitLeechStam!=0 )
						{
							w.WeaponAttributes.HitLeechStam=0;
							from.SendMessage("Gem absorbed items glow, and disintegrated with bright flash");
						}
                                                else
						{
							w.WeaponAttributes.HitLeechStam=5;
							from.SendMessage("Glow from gem was transfered to item, gem vanished into thin air");
						}
					}
					
					Effects.PlaySound( from.Location, from.Map, 0x243 );
					Effects.SendMovingParticles( new Entity( Serial.Zero, new Point3D( from.X - 6, from.Y - 4, from.Z + 15 ), from.Map ), from, 0x36D4, 7, 0, false, true, 0x497, 0, 9502, 1, 0, (EffectLayer)255, 0x100 );
					Effects.SendTargetParticles( from, 0x375A, 35, 90, 0x00, 0x00, 9502, (EffectLayer)255, 0x100 );
					Effects.SendTargetParticles( from, 0x375A, 35, 90, 0x00, 0x00, 9502, (EffectLayer)255, 0x100 );
					m_deed.Delete();
				}
                                else
				{
					from.SendMessage("Gem did not react to targeted object");
				}
			}
		}
	}
}
