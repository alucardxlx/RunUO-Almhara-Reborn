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

namespace Server.Mobiles
{
	public class RedDaggerKeepArmorMerchant : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override bool InitialInnocent{ get{ return true; } }
		public override bool CanTeach { get { return true; } }

		[Constructable]
		public RedDaggerKeepArmorMerchant() : base( "the armor merchant" )
		{
                }

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBRedDaggerKeepArmorMerchant() );
		}

		public override void InitOutfit()
		{
			SetStr( 143 );
			SetDex( 100 );
			SetInt( 42 );

			SetSkill( SkillName.Anatomy, 60.0, 83.0 );
			SetSkill( SkillName.ArmsLore, 64.0, 100.0 );
			SetSkill( SkillName.Tactics, 60.0, 83.0 );
			SetSkill( SkillName.Parry, 61.0, 93.0 );
			SetSkill( SkillName.Wrestling, 60.0, 83.0 );

			PackGold( 13, 27 );

			AddItem( new Boots() );

			StuddedArms arms = new StuddedArms();
			arms.Movable = true;
			AddItem( arms );

			StuddedChest chest = new StuddedChest();
			chest.Movable = true;
			AddItem( chest );

			StuddedGloves gloves = new StuddedGloves();
			gloves.Movable = true;
			AddItem( gloves );

			StuddedGorget gorget = new StuddedGorget();
			gorget.Movable = true;
			AddItem( gorget );

			StuddedLegs legs = new StuddedLegs();
			legs.Movable = true;
			AddItem( legs );

			PackGold( 23, 35 );

			if ( this.Female = Utility.RandomBool() ) 
			{ 
				this.Body = 0x191; 
				this.Name = NameList.RandomName( "female" );
			        this.Hue = Utility.RandomSkinHue(); 

                                this.HairHue = Utility.RandomList( 26,44,81,1102,1107,1108,1109,1116,1117,1122,1138,1140,1141,1146,1148,1149,1153 );
                                this.HairItemID = Utility.RandomList( 8251,8252,8253,8260,8261,8262,8263,8264,8265 );

			        switch ( Utility.Random( 8 ) )
			        {
				       case 0: AddItem( new Boots( Utility.RandomNeutralHue() ) ); break;
				       case 1: AddItem( new FurBoots( Utility.RandomNeutralHue() ) ); break;
				       case 2: AddItem( new LightBoots( Utility.RandomNeutralHue() ) ); break;
				       case 3: AddItem( new Sandals( Utility.RandomNeutralHue() ) ); break;
				       case 4: AddItem( new ShortBoots( Utility.RandomNeutralHue() ) ); break;
				       case 5: AddItem( new ThighBoots( Utility.RandomNeutralHue() ) ); break;
			        }

			        if ( 0.05 > Utility.RandomDouble() )
                                {
			              SilverBracelet bracelet = new SilverBracelet();
                                      bracelet.Hue = Utility.RandomList( 26,44,81,1102,1107,1108,1109,1116,1117,1122,1138,1140,1141,1146,1148,1149,1153 );
			              bracelet.Movable = true;
			              AddItem( bracelet );
                                }

			        if ( 0.05 > Utility.RandomDouble() )
                                {
			              SilverNecklace necklace = new SilverNecklace();
                                      necklace.Hue = Utility.RandomList( 26,44,81,1102,1107,1108,1109,1116,1117,1122,1138,1140,1141,1146,1148,1149,1153 );
			              necklace.Movable = true;
			              AddItem( necklace );
                                }

			        if ( 0.05 > Utility.RandomDouble() )
                                {
			              SilverEarrings earrings = new SilverEarrings();
                                      earrings.Hue = Utility.RandomList( 26,44,81,1102,1107,1108,1109,1116,1117,1122,1138,1140,1141,1146,1148,1149,1153 );
			              earrings.Movable = true;
			              AddItem( earrings );
                                }
			}
			else 
			{ 
				this.Body = 0x190; 
				this.Name = NameList.RandomName( "male" );
			        this.Hue = Utility.RandomSkinHue();

                                this.HairHue = Utility.RandomList( 26,44,81,1102,1107,1108,1109,1116,1117,1122,1138,1140,1141,1146,1148,1149,1153 );
                                this.HairItemID = Utility.RandomList( 8251,8252,8253,8260,8261,8262,8263,8264,8265 );
                                this.FacialHairHue = Utility.RandomList( 26,44,81,1102,1107,1108,1109,1116,1117,1122,1138,1140,1141,1146,1148,1149,1153 );
                                this.FacialHairItemID = Utility.RandomList( 8254,8255,8256,8257,8267,8268,8269 );

			        switch ( Utility.Random( 6 ) )
			        {
				       case 0: AddItem( new Boots( Utility.RandomNeutralHue() ) ); break;
				       case 1: AddItem( new HeavyBoots( Utility.RandomNeutralHue() ) ); break;
				       case 2: AddItem( new HighBoots( Utility.RandomNeutralHue() ) ); break;
				       case 3: AddItem( new Shoes( Utility.RandomNeutralHue() ) ); break;
				       case 4: AddItem( new ShortBoots( Utility.RandomNeutralHue() ) ); break;
				       case 5: AddItem( new ThighBoots( Utility.RandomNeutralHue() ) ); break;
                                }

			        if ( 0.05 > Utility.RandomDouble() )
                                {
			              SilverBracelet bracelet = new SilverBracelet();
                                      bracelet.Hue = Utility.RandomList( 26,44,81,1102,1107,1108,1109,1116,1117,1122,1138,1140,1141,1146,1148,1149,1153 );
			              bracelet.Movable = true;
			              AddItem( bracelet );
                                }

			        if ( 0.05 > Utility.RandomDouble() )
                                {
			              SilverEarrings earrings = new SilverEarrings();
                                      earrings.Hue = Utility.RandomList( 26,44,81,1102,1107,1108,1109,1116,1117,1122,1138,1140,1141,1146,1148,1149,1153 );
			              earrings.Movable = true;
			              AddItem( earrings );
                                }
			} 
                }

		public RedDaggerKeepArmorMerchant( Serial serial ) : base( serial )
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

	public class SBRedDaggerKeepArmorMerchant : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBRedDaggerKeepArmorMerchant()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
////////////////////////////////////////////////////// Level 1
				Add( new GenericBuyInfo( typeof( FemaleLeatherChest ), 100, 500, 0x1C06, 0 ) );
				Add( new GenericBuyInfo( typeof( LeatherArms ), 50, 500, 0x13CD, 0 ) );
				Add( new GenericBuyInfo( typeof( LeatherBustierArms ), 100, 500, 0x1C0A, 0 ) );
				Add( new GenericBuyInfo( typeof( LeatherCap ), 20, 500, 0x1DB9, 0 ) );
				Add( new GenericBuyInfo( typeof( LeatherChest ), 100, 500, 0x13CC, 0 ) );
				Add( new GenericBuyInfo( typeof( LeatherGloves ), 20, 500, 0x13C6, 0 ) );
				Add( new GenericBuyInfo( typeof( LeatherGorget ), 40, 500, 0x13C7, 0 ) );
				Add( new GenericBuyInfo( typeof( LeatherLegs ), 80, 500, 0x13CB, 0 ) );
				Add( new GenericBuyInfo( typeof( LeatherShorts ), 80, 500, 0x1C00, 0 ) );
				Add( new GenericBuyInfo( typeof( LeatherSkirt ), 80, 500, 0x1C08, 0 ) );

////////////////////////////////////////////////////// Level 3
				Add( new GenericBuyInfo( typeof( FemaleLeafChest ), 200, 50, 0x2FCB, 0 ) );
				Add( new GenericBuyInfo( typeof( LeafArms ), 100, 500, 0x2FC8, 0 ) );
				Add( new GenericBuyInfo( typeof( LeafChest ), 200, 500, 0x2FC5, 0 ) );
				Add( new GenericBuyInfo( typeof( LeafGloves ), 40, 500, 0x2FC6, 0 ) );
				Add( new GenericBuyInfo( typeof( LeafGorget ), 80, 500, 0x2FC7, 0 ) );
				Add( new GenericBuyInfo( typeof( LeafLegs ), 160, 500, 0x2FC9, 0 ) );
				Add( new GenericBuyInfo( typeof( LeafTonlet ), 160, 500, 0x2FCA, 0 ) );

////////////////////////////////////////////////////// Level 6
				Add( new GenericBuyInfo( typeof( LeatherDo ), 300, 500, 0x277B, 0 ) );
				Add( new GenericBuyInfo( typeof( LeatherHaidate ), 300, 500, 0x278A, 0 ) );
				Add( new GenericBuyInfo( typeof( LeatherHiroSode ), 300, 500, 0x277E, 0 ) );
				Add( new GenericBuyInfo( typeof( LeatherJingasa ), 300, 500, 0x2776, 0 ) );
				Add( new GenericBuyInfo( typeof( LeatherMempo ), 300, 500, 0x277A, 0 ) );
				Add( new GenericBuyInfo( typeof( LeatherNinjaHood ), 300, 500, 0x278E, 0 ) );
				Add( new GenericBuyInfo( typeof( LeatherNinjaJacket ), 300, 500, 0x2793, 0 ) );
				Add( new GenericBuyInfo( typeof( LeatherNinjaMitts ), 300, 500, 0x2792, 0 ) );
				Add( new GenericBuyInfo( typeof( LeatherNinjaPants ), 300, 500, 0x2791, 0 ) );
				Add( new GenericBuyInfo( typeof( LeatherSuneate ), 300, 500, 0x2786, 0 ) );

////////////////////////////////////////////////////// Level 9
				Add( new GenericBuyInfo( typeof( EbonsilkArms ), 400, 500, 15852, 0 ) );
				Add( new GenericBuyInfo( typeof( EbonsilkChest ), 400, 500, 15855, 0 ) );
				Add( new GenericBuyInfo( typeof( EbonsilkGloves ), 400, 500, 15856, 0 ) );
				Add( new GenericBuyInfo( typeof( EbonsilkGorget ), 400, 500, 15859, 0 ) );
				Add( new GenericBuyInfo( typeof( EbonsilkLegs ), 400, 500, 15860, 0 ) );
				Add( new GenericBuyInfo( typeof( EbonsilkTiara ), 400, 500, 15862, 0 ) );

////////////////////////////////////////////////////// Level 12
				Add( new GenericBuyInfo( typeof( ChitinArms ), 500, 500, 15329, 0 ) );
				Add( new GenericBuyInfo( typeof( ChitinChest ), 500, 500, 15332, 0 ) );
				Add( new GenericBuyInfo( typeof( ChitinGloves ), 500, 500, 15333, 0 ) );
				Add( new GenericBuyInfo( typeof( ChitinGorget ), 500, 500, 15335, 0 ) );
				Add( new GenericBuyInfo( typeof( ChitinHelmet ), 500, 500, 15336, 0 ) );
				Add( new GenericBuyInfo( typeof( ChitinLegs ), 500, 500, 15337, 0 ) );

////////////////////////////////////////////////////// Level 15
				Add( new GenericBuyInfo( typeof( FemaleStuddedChest ), 600, 500, 0x1C02, 0 ) );
				Add( new GenericBuyInfo( typeof( StuddedArms ), 600, 500, 0x13DC, 0 ) );
				Add( new GenericBuyInfo( typeof( StuddedBustierArms ), 600, 500, 0x1C0C, 0 ) );
				Add( new GenericBuyInfo( typeof( StuddedChest ), 600, 500, 0x13DB, 0 ) );
				Add( new GenericBuyInfo( typeof( StuddedGloves ), 600, 500, 0x13D5, 0 ) );
				Add( new GenericBuyInfo( typeof( StuddedGorget ), 600, 500, 0x13D6, 0 ) );
				Add( new GenericBuyInfo( typeof( StuddedLegs ), 600, 500, 0x13DA, 0 ) );

////////////////////////////////////////////////////// Level 18
				Add( new GenericBuyInfo( typeof( HideFemaleChest ), 700, 500, 0x2B79, 0 ) );
				Add( new GenericBuyInfo( typeof( HideChest ), 700, 500, 0x2B74, 0 ) );
				Add( new GenericBuyInfo( typeof( HideGloves ), 700, 500, 0x2B75, 0 ) );
				Add( new GenericBuyInfo( typeof( HideGorget ), 700, 500, 0x2B76, 0 ) );
				Add( new GenericBuyInfo( typeof( HidePants ), 700, 500, 0x2B78, 0 ) );
				Add( new GenericBuyInfo( typeof( HidePauldrons ), 700, 500, 0x2B77, 0 ) );

////////////////////////////////////////////////////// Level 21
				Add( new GenericBuyInfo( typeof( StuddedDo ), 800, 500, 0x277C, 0 ) );
				Add( new GenericBuyInfo( typeof( StuddedHaidate ), 800, 500, 0x278B, 0 ) );
				Add( new GenericBuyInfo( typeof( StuddedHiroSode ), 800, 500, 0x277F, 0 ) );
				Add( new GenericBuyInfo( typeof( StuddedMempo ), 800, 500, 0x279D, 0 ) );
				Add( new GenericBuyInfo( typeof( StuddedSuneate ), 800, 500, 0x2787, 0 ) );

////////////////////////////////////////////////////// Level 24
				Add( new GenericBuyInfo( typeof( VikingStuddedArms ), 900, 500, 15345, 0 ) );
				Add( new GenericBuyInfo( typeof( VikingStuddedCap ), 900, 500, 15348, 0 ) );
				Add( new GenericBuyInfo( typeof( VikingStuddedChest ), 900, 500, 15349, 0 ) );
				Add( new GenericBuyInfo( typeof( VikingStuddedLegs ), 900, 500, 15352, 0 ) );

////////////////////////////////////////////////////// Level 27
				Add( new GenericBuyInfo( typeof( ChainChest ), 1000, 500, 0x13BF, 0 ) );
				Add( new GenericBuyInfo( typeof( ChainCoif ), 1000, 500, 0x13BB, 0 ) );
				Add( new GenericBuyInfo( typeof( ChainLegs ), 1000, 500, 0x13BE, 0 ) );

////////////////////////////////////////////////////// Level 30
				Add( new GenericBuyInfo( typeof( WeldedChainArms ), 1100, 500, 15339, 0 ) );
				Add( new GenericBuyInfo( typeof( WeldedChainChest ), 1100, 500, 15342, 0 ) );
				Add( new GenericBuyInfo( typeof( WeldedChainLegs ), 1100, 500, 15343, 0 ) );

////////////////////////////////////////////////////// Level 33
				Add( new GenericBuyInfo( typeof( ElvenChainArms ), 1200, 500, 15317, 0 ) );
				Add( new GenericBuyInfo( typeof( ElvenChainChest ), 1200, 500, 15320, 0 ) );
				Add( new GenericBuyInfo( typeof( ElvenChainGloves ), 1200, 500, 15321, 0 ) );
				Add( new GenericBuyInfo( typeof( ElvenChainGorget ), 1200, 500, 15323, 0 ) );
				Add( new GenericBuyInfo( typeof( ElvenChainHelmet ), 1200, 500, 15326, 0 ) );
				Add( new GenericBuyInfo( typeof( ElvenChainLegs ), 1200, 500, 15327, 0 ) );

////////////////////////////////////////////////////// Level 36
				Add( new GenericBuyInfo( typeof( RingmailArms ), 1300, 500, 0x13EE, 0 ) );
				Add( new GenericBuyInfo( typeof( RingmailChest ), 1300, 500, 0x13ec, 0 ) );
				Add( new GenericBuyInfo( typeof( RingmailGloves ), 1300, 500, 0x13eb, 0 ) );
				Add( new GenericBuyInfo( typeof( RingmailLegs ), 1300, 500, 0x13F0, 0 ) );

////////////////////////////////////////////////////// Level 39
				Add( new GenericBuyInfo( typeof( ScalemailArms ), 1400, 500, 15309, 0 ) );
				Add( new GenericBuyInfo( typeof( ScalemailChest ), 1400, 500, 15312, 0 ) );
				Add( new GenericBuyInfo( typeof( ScalemailGloves ), 1400, 500, 15313, 0 ) );
				Add( new GenericBuyInfo( typeof( ScalemailGorget ), 1400, 500, 15314, 0 ) );
				Add( new GenericBuyInfo( typeof( ScalemailLegs ), 1400, 500, 15315, 0 ) );

////////////////////////////////////////////////////// Level 42
				Add( new GenericBuyInfo( typeof( BoneArms ), 1500, 500, 0x144e, 0 ) );
				Add( new GenericBuyInfo( typeof( BoneChest ), 1500, 500, 0x1454, 0 ) );
				Add( new GenericBuyInfo( typeof( BoneGloves ), 1500, 500, 0x1450, 0 ) );
				Add( new GenericBuyInfo( typeof( BoneHelm ), 1500, 500, 0x1456, 0 ) );
				Add( new GenericBuyInfo( typeof( BoneLegs ), 1500, 500, 0x1452, 0 ) );

////////////////////////////////////////////////////// Level 45
				Add( new GenericBuyInfo( typeof( Bascinet ), 1600, 500, 0x140C, 0 ) );
				Add( new GenericBuyInfo( typeof( CloseHelm ), 1600, 500, 0x1408, 0 ) );
				Add( new GenericBuyInfo( typeof( Helmet ), 1600, 500, 0x140A, 0 ) );
				Add( new GenericBuyInfo( typeof( NorseHelm ), 1600, 500, 0x140E, 0 ) );

				Add( new GenericBuyInfo( typeof( FemalePlateChest ), 1600, 500, 0x1C04, 0 ) );
				Add( new GenericBuyInfo( typeof( PlateArms ), 1600, 500, 0x1410, 0 ) );
				Add( new GenericBuyInfo( typeof( PlateChest ), 1600, 500, 0x1415, 0 ) );
				Add( new GenericBuyInfo( typeof( PlateGloves ), 1600, 500, 0x1414, 0 ) );
				Add( new GenericBuyInfo( typeof( PlateGorget ), 1600, 500, 0x1413, 0 ) );
				Add( new GenericBuyInfo( typeof( PlateLegs ), 1600, 500, 0x1411, 0 ) );

////////////////////////////////////////////////////// Level 48
				Add( new GenericBuyInfo( typeof( WoodlandArms ), 1700, 500, 0x2B6C, 0 ) );
				Add( new GenericBuyInfo( typeof( WoodlandChest ), 1700, 500, 0x2B67, 0 ) );
				Add( new GenericBuyInfo( typeof( WoodlandGloves ), 1700, 500, 0x2B6A, 0 ) );
				Add( new GenericBuyInfo( typeof( WoodlandGorget ), 1700, 500, 0x2B69, 0 ) );
				Add( new GenericBuyInfo( typeof( WoodlandLegs ), 1700, 500, 0x2B6B, 0 ) );

////////////////////////////////////////////////////// Level 51
				Add( new GenericBuyInfo( typeof( DecorativePlateKabuto ), 1800, 500, 0x2778, 0 ) );
				Add( new GenericBuyInfo( typeof( HeavyPlateJingasa ), 1800, 500, 0x2777, 0 ) );
				Add( new GenericBuyInfo( typeof( PlateDo ), 1800, 500, 0x277D, 0 ) );
				Add( new GenericBuyInfo( typeof( PlateHaidate ), 1800, 500, 0x278D, 0 ) );
				Add( new GenericBuyInfo( typeof( PlateHatsuburi ), 1800, 500, 0x2775, 0 ) );
				Add( new GenericBuyInfo( typeof( PlateHiroSode ), 1800, 500, 0x2780, 0 ) );
				Add( new GenericBuyInfo( typeof( PlateSuneate ), 1800, 500, 0x2788, 0 ) );

////////////////////////////////////////////////////// Shields
				Add( new GenericBuyInfo( typeof( Buckler ), 100, 500, 0x1B73, 0 ) ); // Level 1
				Add( new GenericBuyInfo( typeof( WoodenShield ), 200, 500, 0x1B7A, 0 ) ); // Level 3
				Add( new GenericBuyInfo( typeof( AmmoniteShield ), 300, 500, 14404, 0 ) ); // Level 6
				Add( new GenericBuyInfo( typeof( BronzeShield ), 400, 500, 0x1B72, 0 ) ); // Level 9
				Add( new GenericBuyInfo( typeof( MetalShield ), 500, 500, 0x1B7B, 0 ) ); // Level 12
				Add( new GenericBuyInfo( typeof( WoodenKiteShield ), 600, 500, 0x1B78, 0 ) ); // Level 15
				Add( new GenericBuyInfo( typeof( MetalKiteShield ), 700, 500, 0x1B74, 0 ) ); // Level 18
				Add( new GenericBuyInfo( typeof( ElvenShield ), 800, 500, 16361, 0 ) ); // Level 21
				Add( new GenericBuyInfo( typeof( InfantryShield ), 900, 500, 16359, 0 ) ); // Level 24
				Add( new GenericBuyInfo( typeof( SpiderShield ), 1000, 500, 14408, 0 ) ); // Level 27
				Add( new GenericBuyInfo( typeof( GrassShield ), 1100, 500, 14413, 0 ) ); // Level 30
				Add( new GenericBuyInfo( typeof( MercenaryShield ), 1200, 500, 14406, 0 ) ); // Level 33
				Add( new GenericBuyInfo( typeof( NymphShield ), 1300, 500, 14401, 0 ) ); // Level 36
				Add( new GenericBuyInfo( typeof( ScarabShield ), 1400, 500, 14414, 0 ) ); // Level 39
				Add( new GenericBuyInfo( typeof( BoneShield ), 1500, 500, 16365, 0 ) ); // Level 42
				Add( new GenericBuyInfo( typeof( UnicornShield ), 1600, 500, 14403, 0 ) ); // Level 45
				Add( new GenericBuyInfo( typeof( CentaurShield ), 1700, 500, 14410, 0 ) ); // Level 48
				Add( new GenericBuyInfo( typeof( WoodenDragonShield ), 1800, 500, 16363, 0 ) ); // Level 51

////////////////////////////////////////////////////// Unique Helmets
				Add( new GenericBuyInfo( typeof( RavenHelm ), 500, 500, 0x2B71, 0 ) );
				Add( new GenericBuyInfo( typeof( VultureHelm ), 500, 500, 0x2B72, 0 ) );
				Add( new GenericBuyInfo( typeof( WingedHelm ), 500, 500, 0x2B73, 0 ) );

				Add( new GenericBuyInfo( typeof( Circlet ), 800, 500, 0x2B6E, 0 ) );
				Add( new GenericBuyInfo( typeof( RoyalCirclet ), 800, 500, 0x2B6F, 0 ) );
				Add( new GenericBuyInfo( typeof( GemmedCirclet ), 800, 500, 0x2B70, 0 ) );
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{
			} 
		} 
	} 
}
