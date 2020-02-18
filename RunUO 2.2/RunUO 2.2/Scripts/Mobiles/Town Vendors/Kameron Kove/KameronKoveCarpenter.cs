using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Targeting;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Network;
using Server.Spells;
using Server.Mobiles;
using Server.Engines.BulkOrders;

namespace Server.Mobiles 
{ 
	public class KameronKoveCarpenter : BaseVendor 
	{ 
		private List<SBInfo> m_SBInfos = new List<SBInfo>(); 
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } } 

		public override bool InitialInnocent{ get{ return true; } }
		public override bool CanTeach { get { return true; } }

		[Constructable]
		public KameronKoveCarpenter() : base( "the carpenter" ) 
		{ 
		} 

		public override void InitSBInfo() 
		{ 
			m_SBInfos.Add( new SBKameronKoveCarpenter() ); 
		}

		public override void InitOutfit()
		{
			Hue = Utility.RandomList( 3,28,38,48,53,58,63,88,93 );

			SetStr( 117 );
			SetDex( 96 );
			SetInt( 54 );

			SetSkill( SkillName.Carpentry, 85.0, 100.0 );
			SetSkill( SkillName.Lumberjacking, 60.0, 83.0 );

			PackGold( 41, 82 );

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
                }

		#region Bulk Orders
		public override Item CreateBulkOrder( Mobile from, bool fromContextMenu )
		{
			PlayerMobile pm = from as PlayerMobile;

			if ( pm != null && pm.NextCarpentryBulkOrder == TimeSpan.Zero && (fromContextMenu || 0.2 > Utility.RandomDouble()) )
			{
				double theirSkill = pm.Skills[SkillName.Carpentry].Base;

				if ( theirSkill >= 70.1 )
					pm.NextCarpentryBulkOrder = TimeSpan.FromHours( 3.0 );
				else if ( theirSkill >= 50.1 )
					pm.NextCarpentryBulkOrder = TimeSpan.FromHours( 2.0 );
				else
					pm.NextCarpentryBulkOrder = TimeSpan.FromHours( 1.0 );

				return SmallCarpentryBOD.CreateRandomFor( from );
			}

			return null;
		}

		public override bool IsValidBulkOrder( Item item )
		{
			return ( item is SmallCarpentryBOD || item is LargeCarpentryBOD );
		}

		public override bool SupportsBulkOrders( Mobile from )
		{
			return ( from is PlayerMobile && from.Skills[SkillName.Carpentry].Base > 0 );
		}

		public override TimeSpan GetNextBulkOrder( Mobile from )
		{
			if ( from is PlayerMobile )
				return ((PlayerMobile)from).NextCarpentryBulkOrder;

			return TimeSpan.Zero;
		}

		public override void OnSuccessfulBulkOrderReceive( Mobile from )
		{
			if( Core.SE && from is PlayerMobile )
				((PlayerMobile)from).NextCarpentryBulkOrder = TimeSpan.Zero;
		}
		#endregion

		public KameronKoveCarpenter( Serial serial ) : base( serial ) 
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

	public class SBKameronKoveCarpenter : SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBKameronKoveCarpenter() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 	
				Add( new GenericBuyInfo( "Board", typeof( Board ), 5, 999, 0x1BD7, 0 ) );

				Add( new GenericBuyInfo( typeof( Hatchet ), 200, 50, 0xF43, 0 ) );

				Add( new GenericBuyInfo( typeof( Nails ), 18, 200, 0x102E, 0 ) );
				Add( new GenericBuyInfo( typeof( Axle ), 8, 200, 0x105B, 0 ) );
				Add( new GenericBuyInfo( typeof( DrawKnife ), 200, 200, 0x10E4, 0 ) );
				Add( new GenericBuyInfo( typeof( Froe ), 200, 200, 0x10E5, 0 ) );
				Add( new GenericBuyInfo( typeof( Scorp ), 200, 200, 0x10E7, 0 ) );
				Add( new GenericBuyInfo( typeof( Inshave ), 200, 200, 0x10E6, 0 ) );
				Add( new GenericBuyInfo( typeof( DovetailSaw ), 244, 200, 0x1028, 0 ) );
				Add( new GenericBuyInfo( typeof( Saw ), 250, 200, 0x1034, 0 ) );
				Add( new GenericBuyInfo( typeof( Hammer ), 200, 200, 0x102A, 0 ) );
				Add( new GenericBuyInfo( typeof( MouldingPlane ), 50, 200, 0x102C, 0 ) );
				Add( new GenericBuyInfo( typeof( SmoothingPlane ), 50, 200, 0x1032, 0 ) );
				Add( new GenericBuyInfo( typeof( JointingPlane ), 50, 200, 0x1030, 0 ) ); 
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				Add( typeof( WoodenBox ), 7 );
				Add( typeof( SmallCrate ), 5 );
				Add( typeof( MediumCrate ), 6 );
				Add( typeof( LargeCrate ), 7 );
				Add( typeof( WoodenChest ), 15 );
              
				Add( typeof( LargeTable ), 10 );
				Add( typeof( Nightstand ), 7 );
				Add( typeof( YewWoodTable ), 10 );

				Add( typeof( Throne ), 24 );
				Add( typeof( WoodenThrone ), 6 );
				Add( typeof( Stool ), 6 );
				Add( typeof( FootStool ), 6 );

				Add( typeof( FancyWoodenChairCushion ), 12 );
				Add( typeof( WoodenChairCushion ), 10 );
				Add( typeof( WoodenChair ), 8 );
				Add( typeof( BambooChair ), 6 );
				Add( typeof( WoodenBench ), 6 );

				Add( typeof( Saw ), 9 );
				Add( typeof( Scorp ), 6 );
				Add( typeof( SmoothingPlane ), 6 );
				Add( typeof( DrawKnife ), 6 );
				Add( typeof( Froe ), 6 );
				Add( typeof( Hammer ), 14 );
				Add( typeof( Inshave ), 6 );
				Add( typeof( JointingPlane ), 6 );
				Add( typeof( MouldingPlane ), 6 );
				Add( typeof( DovetailSaw ), 7 );
				Add( typeof( Board ), 5 );
				Add( typeof( Axle ), 5 );

				Add( typeof( Log ), 5 );
			} 
		} 
	} 
}
