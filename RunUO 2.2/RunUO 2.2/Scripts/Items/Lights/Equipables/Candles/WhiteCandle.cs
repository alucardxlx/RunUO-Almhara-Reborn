using System;
using Server;

namespace Server.Items
{
	public class WhiteCandle : BaseEquipableLight
	{
		public override int LitItemID{ get { return 0xA0F; } }
		public override int UnlitItemID{ get { return 0xA28; } }

		[Constructable]
		public WhiteCandle() : base( 0xA28 )
		{
			if ( Burnout )
				Duration = TimeSpan.FromMinutes( 20 );
			else
				Duration = TimeSpan.Zero;

			Burning = false;
			Light = LightType.Circle150;
                        Name = "White Candle";
			Weight = 1.0;
                        Hue = 1150;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			chaos = direct = phys = fire = pois = nrgy = 0;
                        cold = 100;
		}

		public override void OnHit( Mobile attacker, Mobile defender, double damageBonus )
		{
			base.OnHit( attacker, defender, damageBonus );

                        defender.PlaySound( 0x208 );
                        Effects.SendTargetEffect( defender, 0x3709, 10, 30, 1150, 0 );
		}

		public WhiteCandle( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}