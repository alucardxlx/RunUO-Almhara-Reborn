using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{
	public class SuperAgilityPotion : BaseAgilityPotion
	{
		public override int DexOffset{ get{ return 30; } }
		public override TimeSpan Duration{ get{ return TimeSpan.FromMinutes( 5.0 ); } }

		private int m_RequiredLevel = 30;

		[CommandProperty( AccessLevel.GameMaster )]
		public override int RequiredLevel
		{ 
			get{ return m_RequiredLevel; }
			set {m_RequiredLevel = value; InvalidateProperties();}
		}

		[Constructable]
		public SuperAgilityPotion() : base( PotionEffect.AgilityGreater )
		{
                	Name = "Super Blue Potion";
		}

		public SuperAgilityPotion( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version

			writer.Write( m_RequiredLevel );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			m_RequiredLevel = reader.ReadInt();
		}
	}
}