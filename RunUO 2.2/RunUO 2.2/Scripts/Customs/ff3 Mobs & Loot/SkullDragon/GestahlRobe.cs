//Customized By Mrs Death
using System;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class GestahlRobe : Robe
  {
public override int ArtifactRarity{ get{ return 6; } }


      
      [Constructable]
		public GestahlRobe()
		{
			Weight = 5;
          Name = "[FF6] Gestahl's Robe";
      Hue = 34;
      Attributes.AttackChance = 13;
      Attributes.BonusDex = 13;
  
      Attributes.BonusInt = 13;
      Attributes.BonusMana = 13;
      Attributes.BonusStam = 13;
      Attributes.DefendChance = 13;
      Attributes.LowerManaCost = 13;
      Attributes.LowerRegCost = 13;
      Attributes.Luck = 666;
      Attributes.ReflectPhysical = 13;
      Attributes.WeaponDamage = 13;
      Attributes.BonusMana = 13;
		}

		public GestahlRobe( Serial serial ) : base( serial )
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