//Customized By Mrs Death
using System;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class MogSash : BodySash
  {
public override int ArtifactRarity{ get{ return 6; } }


      
      [Constructable]
		public MogSash()
		{
          Name = "[FF6] Mog's Sash";
          Hue = 1150;
      Attributes.AttackChance = 20;
      Attributes.BonusHits = 15;
      Attributes.CastRecovery = 2;
      Attributes.CastSpeed = 2;
      Attributes.DefendChance = 20;
      Attributes.LowerManaCost = 15;
      Attributes.LowerRegCost = 40;
      Attributes.Luck = 500;
      Attributes.NightSight = 1;
      Attributes.ReflectPhysical = 15;
      Attributes.RegenHits = 2;
      Attributes.RegenMana = 2;
      Attributes.RegenStam = 2;
      Attributes.SpellDamage = 15;
		}

		public MogSash( Serial serial ) : base( serial )
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
