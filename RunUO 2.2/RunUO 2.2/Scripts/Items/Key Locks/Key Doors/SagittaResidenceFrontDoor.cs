using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	public class SagittaResidenceFrontDoor : StrongWoodDoor, ILockpickable
	{
		private int m_LockLevel, m_MaxLockLevel, m_RequiredSkill;
		private Mobile m_Picker;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Picker
		{
			get { return m_Picker; }
			set { m_Picker = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int LockLevel
		{
			get { return m_LockLevel; }
			set { m_LockLevel = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int MaxLockLevel
		{
			get { return m_MaxLockLevel; }
			set { m_MaxLockLevel = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int RequiredSkill
		{
			get { return m_RequiredSkill; }
			set { m_RequiredSkill = value; }
		}
		
		public virtual void LockPick( Mobile from )
		{
			Picker = from;
			Locked = false;
                        m_Unlocked = DateTime.Now;		
		}
		
		[Constructable]
		public SagittaResidenceFrontDoor( DoorFacing facing ) : base( facing )
		{
			m_LockLevel = 50;
			m_MaxLockLevel = 55; 
			m_RequiredSkill = 50;
		}

		private DateTime m_Unlocked;

		[ CommandProperty( AccessLevel.GameMaster ) ]
		public DateTime Unlocked
		{
			get { return m_Unlocked; }
			set { m_Unlocked = value; }
		}

		private string m_Message = null;

		[CommandProperty( AccessLevel.GameMaster )]
		public string Message
		{
			get{ return m_Message; }
			set{ m_Message = value; }
		}

		private TimeSpan m_RelockTime = TimeSpan.FromMinutes( 30.0 );

		[ CommandProperty( AccessLevel.GameMaster ) ]
		public TimeSpan RelockTime
		{
			get { return m_RelockTime; }
			set { m_RelockTime = value; }
		}

		public override void Use( Mobile from )
		{
			if ( DateTime.Now > m_Unlocked + m_RelockTime )
			{
				Locked = true;
				from.SendMessage( "The door requires either the house key or a skill of 50 in order to unlock." );
				return;
			}
			if ( m_Message != null && m_Message.Length > 0 )
				from.SendMessage( m_Message );
			
			base.Use( from );
		}

		public SagittaResidenceFrontDoor( Serial serial ) : base( serial )
		{
			
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 );
			
			writer.Write( m_Unlocked );
			writer.Write( m_RelockTime );
			writer.Write( m_Message );
			writer.Write( (int) m_RequiredSkill );
			writer.Write( (int) m_MaxLockLevel );
			writer.Write( (int) m_LockLevel );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			m_Unlocked = reader.ReadDateTime();
			m_RelockTime = reader.ReadTimeSpan();
			m_Message = reader.ReadString();
			m_RequiredSkill = reader.ReadInt();
			m_MaxLockLevel = reader.ReadInt();
			m_LockLevel = reader.ReadInt();
		}
	}
}
