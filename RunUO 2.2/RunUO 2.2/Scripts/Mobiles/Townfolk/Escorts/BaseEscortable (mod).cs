using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Network;
using Server.ContextMenus;
using EDI = Server.Mobiles.EscortDestinationInfo;
using Server.Engines.MLQuests;
using Server.Engines.MLQuests.Definitions;
using Server.Engines.MLQuests.Objectives;

namespace Server.Mobiles
{
	public class BaseEscortable : BaseCreature
	{
		public static readonly TimeSpan EscortDelay = TimeSpan.FromMinutes( 5.0 );
		public static readonly TimeSpan AbandonDelay = MLQuestSystem.Enabled ? TimeSpan.FromMinutes( 1.0 ) : TimeSpan.FromMinutes( 2.0 );
		public static readonly TimeSpan DeleteTime = MLQuestSystem.Enabled ? TimeSpan.FromSeconds( 100 ) : TimeSpan.FromSeconds( 30 );

		public override bool StaticMLQuester { get { return false; } } // Suppress automatic quest registration on creation/deserialization

		private MLQuest m_MLQuest;

		protected override List<MLQuest> ConstructQuestList()
		{
			if ( m_MLQuest == null )
			{
				Region reg = Region;
				Type[] list = reg.IsPartOf( "Haven Island" ) ? m_MLQuestTypesNH : m_MLQuestTypes;

				int randomIdx = Utility.Random( list.Length );

				for ( int i = 0; i < list.Length; ++i )
				{
					Type questType = list[randomIdx];

					MLQuest quest = MLQuestSystem.FindQuest( questType );

					if ( quest != null )
					{
						bool okay = true;

						foreach ( BaseObjective obj in quest.Objectives )
						{
							if ( obj is EscortObjective && ( (EscortObjective)obj ).Destination.Contains( reg ) )
							{
								okay = false; // We're already there!
								break;
							}
						}

						if ( okay )
						{
							m_MLQuest = quest;
							break;
						}
					}
					else if ( MLQuestSystem.Debug )
					{
						Console.WriteLine( "Warning: Escortable cannot be assigned quest type '{0}', it is not registered", questType.Name );
					}

					randomIdx = ( randomIdx + 1 ) % list.Length;
				}

				if ( m_MLQuest == null )
				{
					if ( MLQuestSystem.Debug )
						Console.WriteLine( "Warning: No suitable quest found for escort {0}", Serial );

					return null;
				}
			}

			List<MLQuest> result = new List<MLQuest>();
			result.Add( m_MLQuest );

			return result;
		}

		public override bool CanShout { get { return ( !Controlled && !IsBeingDeleted ); } }

		public override void Shout( PlayerMobile pm )
		{
			/*
			 * 1072301 - You there!  Care to hear how to earn some easy gold?
			 * 1072302 - Adventurer!  I have an offer for you.
			 * 1072303 - Wait!  I have an opportunity for you to make some gold!
			 */
			MLQuestSystem.Tell( this, pm, Utility.Random( 1072301, 3 ) );
		}

		private EDI m_Destination;
		private string m_DestinationString;

		private DateTime m_DeleteTime;
		private Timer m_DeleteTimer;

		private bool m_DeleteCorpse = false;

		public bool IsBeingDeleted
		{
			get { return ( m_DeleteTimer != null ); }
		}

		public override bool Commandable { get { return false; } } // Our master cannot boss us around!
		public override bool DeleteCorpseOnDeath { get { return m_DeleteCorpse; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public string Destination
		{
			get { return m_Destination == null ? null : m_Destination.Name; }
			set { m_DestinationString = value; m_Destination = EDI.Find(value); }
		}

		private static string[] m_TownNames = new string[]
		{
                        "Hammerhill", 
                        "Elmhaven", 
                        "Elandrim Nur Shaz",
                        "Old Punderer's Haven", 
                        "Coven's Landing", 
                        "Guardians Horizon",
                        "Red Dagger Keep", 
                        "Skaddria Naddheim", 
                        "Elmhaven Library",
                        "Alytharr Tavern", 
                        "Oh Thank Heaven Pub", 
                        "Seven Deadly Sins Tavern",
                        "Zaythalor Tavern", 
                        "Guardians Tavern", 
                        "Dawnguard Cathedral",
                        "First State Bank of Skaddria Naddheim"
		};

		private static string[] m_MLTownNames = new string[]
		{
                        "Hammerhill", "Elmhaven", "Elandrim Nur Shaz",
                        "Old Punderer's Haven", "Coven's Landing", "Guardians Horizon",
                        "Red Dagger Keep", "Skaddria Naddheim", "Elmhaven Library",
                        "Alytharr Tavern", "Oh Thank Heaven Pub", "Seven Deadly Sins Tavern",
                        "Zaythalor Tavern", "Guardians Tavern", "Dawnguard Cathedral",
                        "First State Bank of Skaddria Naddheim"
		};

		private static string[] m_DungeonRegion = new string[]
		{
			"Amul Seketsi Royal Tomb", "Bearstein Caverns", "Bharlim Passage",
			"Black Widow Pit", "Bleak Wind Tunnels", "Fungully Grotto",
			"Hammerhill Sewers", "Iguana Cove", "Mongbat Hideout",
                        "Nimaku Lava Basin", "Oboru Burial Grounds", "Stone Burrow Mines",
			"Swampwater Solitude", "Whispering Hollow"
		};

		private static string[] m_DungeonEntranceRegion = new string[]
		{
			"Amul Seketsi Royal Tomb Entrance", "Black Widow Pit Entrance", "Bleak Wind Tunnels Entrance", 
                        "Elmhaven Mineshaft Entrance", "Everstar Estuary Entrance", "Fortress Calcifina Entrance",
                        "Fungully Grotto Entrance", "Ghuul Sanctuary Entrance", "Hammerhill Sewers Entrance", 
                        "Iguana Cove Entrance", "Kal-Berbessia Entrance", "Mongbat Hideout Entrance", 
                        "Murkmere Dwelling Entrance", "Nimaku Lava Basin Entrance", "Oboru Burial Grounds Entrance", 
                        "Passage of Lost Souls Entrance", "Rockbitter Vault Entrance", "Stone Burrow Mines Entrance",
			"Swampwater Solitude Entrance", "Tarnithok Fortress Entrance", "Whispering Hollow Entrance"
		};

		private static string[] m_SkaddriaTownShopRegion = new string[]
		{
			"Eagle Eyes in Skaddria Naddheim",
			"Muffin Mayhem in Skaddria Naddheim",
			"Mystic Ark in Skaddria Naddheim",
			"Resource Management in Skaddria Naddheim",
			"Ruby Tuesday in Skaddria Naddheim",
			"Ruffneck Ironworks in Skaddria Naddheim",
			"Stag Beetle Lodge in Skaddria Naddheim",
			"The Bards Tale in Skaddria Naddheim",
			"The Meat Cleaver in Skaddria Naddheim",
			"The Needle's Thread in Skaddria Naddheim",
			"The Road Warrior in Skaddria Naddheim",
			"Treasured Tomes in Skaddria Naddheim"
		};

		private static string[] m_HouseResidenceRegion = new string[]
		{
			"Camp Maplethorne in the Zaythalor Forest",
                        "Ivanguard Residence in the Autumnwood", 
			"Lighthouse in the Zaythalor Forest",
			"Lorentius Manor in the Zaythalor Forest",  
                        "Maggie's Produce in the Autumnwood", 
			"Orwick Farmhouse in the Harashi Nabi", 
			"Popplewell Ranch in the Zaythalor Forest", 
			"Sabrina's Farm in the Zaythalor Forest",
			"Smith and Westerson in the Zaythalor Forest", 
                        "St. Abitha Monastery in the Harashi Nabi", 
                        "Thalidor Residence in the Harashi Nabi",
			"Theresa Residence in the Alytharr Region",
			"Thornforge Manor in the Zaythalor Forest", 
                        "Tiki Hut in the Oboru Jungle",
			"Zoo in the Zaythalor Forest"
		};

		private static string[] m_AreaInterestRegion = new string[]
		{
			"Harpy Nest in the Autumnwood" 
		};

		private static string[] m_LandscapeRegion = new string[]
		{
			"Autumnwood", "Dragon Storm Island", "Glimmerwood",
			"Island of Giants", "Jade Jungle", "Oboru Jungle",
			"Ponyo Plateau", "Samson Swamplands", "Star Lake",
                        "Zaythalor Graveyard"
		};

		private static string[] m_ZaythalorNPCRegion = new string[]
		{
			"go see Abigail the Weaponsmith over in Elmhaven",
			"go see Azuralin the Barber over in Elmhaven",
			"go see Bruce Darabont over at Old Plunderer's Haven",
			"go see Cottoneye Job over in Hammerhill",
			"go see David Cranshaw out over at his cabin in the Zaythalor Forest",
			"go see Denomorr the Scribe over in Elmhaven",
			"go see Feron the Blacksmith over in the Zaythalor Forest",
			"go see Fharlune Crimsonleaf over in Elmhaven",
			"go see Fluffy Snapper over in the Zaythalor Forest",
			"go see Gilmora the Tailor over in Elmhaven",
			"go see Grizelda over in the Zaythalor Forest",
			"go see Haldur over at the Zaythalor Tavern",
			"go see Imus Grant the Tinker over in Elmhaven",
			"go see Karina the Rancher over in Elmhaven",
			"go see Melananha the Sales Gal in Skaddria Naddheim",
			"go see Morokail the Provisioner over in Elmhaven",
			"go see Osprey the Reagent Seller over in Elmhaven",
			"go see Ozzy Mason over in Hammerhill",
			"go see Qualinn the Fletcher over in Elmhaven",
			"go see Ralph the Banner Crafter in Hammerhill",
			"go see Rholan the Alchemist over in Elmhaven",
			"go see Sabrina in the Zaythalor Forest",
			"go see Sierra the Flower Arranger over in Elmhaven",
			"go see Talitharr the Carpenter over in Elmhaven",
			"go see Travis Crabtree over in the Zaythalor Forest",
			"go see Umarelina the Baker over in Elmhaven",
			"go see Vairon the Glassblower over in Elmhaven",
			"go see Vargas over at Old Plunderer's Haven",
			"go see Vas Gharn over in the Zaythalor Forest",
			"go see Welklin the Butcher over in Elmhaven",
			"go see Wynslo the Farmer over in Elmhaven"
		};

		// ML quest system general list
		// Used when: MLQuestSystem.Enabled && !Region.IsPartOf( "Haven Island" )
		private static Type[] m_MLQuestTypes =
		{
			typeof( EscortToYew ),
			typeof( EscortToVesper ),
			typeof( EscortToTrinsic ),
			typeof( EscortToSkaraBrae ),
			typeof( EscortToSerpentsHold ),
			typeof( EscortToNujelm ),
			typeof( EscortToMoonglow ),
			typeof( EscortToMinoc ),
			typeof( EscortToMagincia ),
			typeof( EscortToJhelom ),
			typeof( EscortToCove ),
			typeof( EscortToBritain )
			// Ocllo was removed in pub 56
			//typeof( EscortToOcllo )
		};

		// ML quest system New Haven list
		// Used when: MLQuestSystem.Enabled && Region.IsPartOf( "Haven Island" )
		private static Type[] m_MLQuestTypesNH =
		{
			typeof( EscortToNHAlchemist ),
			typeof( EscortToNHBard ),
			typeof( EscortToNHWarrior ),
			typeof( EscortToNHTailor ),
			typeof( EscortToNHCarpenter ),
			typeof( EscortToNHMapmaker ),
			typeof( EscortToNHMage ),
			typeof( EscortToNHInn ),
			// Farm destination was removed
			//typeof( EscortToNHFarm ),
			typeof( EscortToNHDocks ),
			typeof( EscortToNHBowyer ),
			typeof( EscortToNHBank )
		};

		[Constructable]
		public BaseEscortable(): base(AIType.AI_Melee, FightMode.Aggressor, 22, 1, 0.2, 1.0)
		{
			InitBody();
			InitOutfit();
		}

		public virtual void InitBody()
		{
			SetStr(90, 100);
			SetDex(90, 100);
			SetInt(15, 25);

			Hue = Utility.RandomSkinHue();

			if (Female = Utility.RandomBool())
			{
				Body = 401;
				Name = NameList.RandomName("female");
			}
			else
			{
				Body = 400;
				Name = NameList.RandomName("male");
			}
		}

		public virtual void InitOutfit()
		{
			AddItem(new FancyShirt(Utility.RandomNeutralHue()));
			AddItem(new ShortPants(Utility.RandomNeutralHue()));
			AddItem(new Boots(Utility.RandomNeutralHue()));

			Utility.AssignRandomHair(this);

			PackGold(15, 25);
		}

////////////////////////////////////////////////////// Begin Added kill count if killed by players

                public override bool OnBeforeDeath()
                {
                    if (Combatant is PlayerMobile)
                    Combatant.Kills += 1;
        	
                return base.OnBeforeDeath();
                }

////////////////////////////////////////////////////// End Added kill count if killed by players

		public virtual bool SayDestinationTo(Mobile m)
		{
			EDI dest = GetDestination();

			if (dest == null || !m.Alive)
				return false;

			Mobile escorter = GetEscorter();

			if (escorter == null)
			{
				Say("I am looking to go to {0}, will you take me?", (dest.Name == "Hammerhill" && m.Map == Map.Malas) ? "Skaddria Naddheim" : dest.Name);
				return true;
			}
			else if (escorter == m)
			{
				Say("Lead on! Payment will be made when we arrive in {0}.", (dest.Name == "Hammerhill" && m.Map == Map.Malas) ? "Skaddria Naddheim" : dest.Name);
				return true;
			}

			return false;
		}

		private static Hashtable m_EscortTable = new Hashtable();

		public static Hashtable EscortTable
		{
			get { return m_EscortTable; }
		}

		private static TimeSpan m_EscortDelay = TimeSpan.FromMinutes(5.0);

		public virtual bool AcceptEscorter(Mobile m)
		{
			EDI dest = GetDestination();

			if (dest == null)
				return false;

			Mobile escorter = GetEscorter();

			if (escorter != null || !m.Alive)
				return false;

			BaseEscortable escortable = (BaseEscortable)m_EscortTable[m];

			if (escortable != null && !escortable.Deleted && escortable.GetEscorter() == m)
			{
				Say("I see you already have an escort.");
				return false;
			}
			else if (m is PlayerMobile && (((PlayerMobile)m).LastEscortTime + m_EscortDelay) >= DateTime.Now)
			{
				int minutes = (int)Math.Ceiling(((((PlayerMobile)m).LastEscortTime + m_EscortDelay) - DateTime.Now).TotalMinutes);

				Say("You must rest {0} minute{1} before we set out on this journey.", minutes, minutes == 1 ? "" : "s");
				return false;
			}
			else if (SetControlMaster(m))
			{
				m_LastSeenEscorter = DateTime.Now;

				if (m is PlayerMobile)
					((PlayerMobile)m).LastEscortTime = DateTime.Now;

				Say("Lead on! Payment will be made when we arrive in {0}.", (dest.Name == "Skaddria Naddheim" && m.Map == Map.Malas) ? "Skaddria Naddheim" : dest.Name);
				m_EscortTable[m] = this;
				StartFollow();
				return true;
			}

			return false;
		}

		public override bool HandlesOnSpeech(Mobile from)
		{
			if ( MLQuestSystem.Enabled )
				return false;

			if (from.InRange(this.Location, 3))
				return true;

			return base.HandlesOnSpeech(from);
		}

		public override void OnSpeech(SpeechEventArgs e)
		{
			base.OnSpeech(e);

			EDI dest = GetDestination();

			if (dest != null && !e.Handled && e.Mobile.InRange(this.Location, 3))
			{
				if (e.HasKeyword(0x1D)) // *destination*
					e.Handled = SayDestinationTo(e.Mobile);
				else if (e.HasKeyword(0x1E)) // *i will take thee*
					e.Handled = AcceptEscorter(e.Mobile);
			}
		}

		public override void OnAfterDelete()
		{
			if (m_DeleteTimer != null)
				m_DeleteTimer.Stop();

			m_DeleteTimer = null;

			base.OnAfterDelete();
		}

		public override void OnThink()
		{
			base.OnThink();
			CheckAtDestination();
		}

		protected override bool OnMove(Direction d)
		{
			if (!base.OnMove(d))
				return false;

			CheckAtDestination();

			return true;
		}

		public virtual void StartFollow()
		{
			StartFollow(GetEscorter());
		}

		public virtual void StartFollow(Mobile escorter)
		{
			if (escorter == null)
				return;

			ActiveSpeed = 0.1;
			PassiveSpeed = 0.2;

			ControlOrder = OrderType.Follow;
			ControlTarget = escorter;

			if ((IsPrisoner == true) && (CantWalk == true))
			{
				CantWalk = false;
			}
			CurrentSpeed = 0.1;
		}

		public virtual void StopFollow()
		{
			ActiveSpeed = 0.2;
			PassiveSpeed = 1.0;

			ControlOrder = OrderType.None;
			ControlTarget = null;

			CurrentSpeed = 1.0;
		}

		private DateTime m_LastSeenEscorter;

		public virtual Mobile GetEscorter()
		{
			if (!Controlled)
				return null;

			Mobile master = ControlMaster;

			if ( MLQuestSystem.Enabled || master == null )
				return master;

			if (master.Deleted || master.Map != this.Map || !master.InRange(Location, 30) || !master.Alive)
			{
				StopFollow();

				TimeSpan lastSeenDelay = DateTime.Now - m_LastSeenEscorter;

				if (lastSeenDelay >= TimeSpan.FromMinutes(2.0))
				{
					master.SendLocalizedMessage(1042473); // You have lost the person you were escorting.
					Say(1005653); // Hmmm. I seem to have lost my master.

					SetControlMaster(null);
					m_EscortTable.Remove(master);

					Timer.DelayCall(TimeSpan.FromSeconds(5.0), new TimerCallback(Delete));
					return null;
				}
				else
				{
					ControlOrder = OrderType.Stay;
					return master;
				}
			}

			if (ControlOrder != OrderType.Follow)
				StartFollow(master);

			m_LastSeenEscorter = DateTime.Now;
			return master;
		}

		public virtual void BeginDelete()
		{
			if (m_DeleteTimer != null)
				m_DeleteTimer.Stop();

			m_DeleteTime = DateTime.Now + TimeSpan.FromSeconds(30.0);

			m_DeleteTimer = new DeleteTimer(this, m_DeleteTime - DateTime.Now);
			m_DeleteTimer.Start();
		}

		public virtual bool CheckAtDestination()
		{
			if ( MLQuestSystem.Enabled )
				return false;

			EDI dest = GetDestination();

			if (dest == null)
				return false;

			Mobile escorter = GetEscorter();

			if (escorter == null)
				return false;

			if (dest.Contains(Location))
			{
				Say(1042809, escorter.Name); // We have arrived! I thank thee, ~1_PLAYER_NAME~! I have no further need of thy services. Here is thy pay.

				// not going anywhere
				m_Destination = null;
				m_DestinationString = null;

				Container cont = escorter.Backpack;

				if (cont == null)
					cont = escorter.BankBox;

				Gold gold = new Gold(500, 1000);

				if (!cont.TryDropItem(escorter, gold, false))
					gold.MoveToWorld(escorter.Location, escorter.Map);

				StopFollow();
				SetControlMaster(null);
				m_EscortTable.Remove(escorter);
				BeginDelete();

				Misc.Titles.AwardFame(escorter, 10, true);

				bool gainedPath = false;

				PlayerMobile pm = escorter as PlayerMobile;

				if (pm != null)
				{
					if (pm.CompassionGains > 0 && DateTime.Now > pm.NextCompassionDay)
					{
						pm.NextCompassionDay = DateTime.MinValue;
						pm.CompassionGains = 0;
					}

					if (pm.CompassionGains >= 5) // have already gained 5 times in one day, can gain no more
					{
						pm.SendLocalizedMessage(1053004); // You must wait about a day before you can gain in compassion again.
					}
					else if (VirtueHelper.Award(pm, VirtueName.Compassion, this.IsPrisoner ? 400 : 200, ref gainedPath))
					{
						if (gainedPath)
							pm.SendLocalizedMessage(1053005); // You have achieved a path in compassion!
						else
							pm.SendLocalizedMessage(1053002); // You have gained in compassion.

						pm.NextCompassionDay = DateTime.Now + TimeSpan.FromDays(1.0); // in one day CompassionGains gets reset to 0
						++pm.CompassionGains;
					}
					else
					{
						pm.SendLocalizedMessage(1053003); // You have achieved the highest path of compassion and can no longer gain any further.
					}
				}

				return true;
			}

			return false;
		}

		public BaseEscortable(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version

			EDI dest = GetDestination();

			writer.Write(dest != null);

			if (dest != null)
				writer.Write(dest.Name);

			writer.Write(m_DeleteTimer != null);

			if (m_DeleteTimer != null)
				writer.WriteDeltaTime(m_DeleteTime);

			MLQuestSystem.WriteQuestRef( writer, StaticMLQuester ? null : m_MLQuest );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			if (reader.ReadBool())
				m_DestinationString = reader.ReadString(); // NOTE: We cannot EDI.Find here, regions have not yet been loaded :-(

			if (reader.ReadBool())
			{
				m_DeleteTime = reader.ReadDeltaTime();
				m_DeleteTimer = new DeleteTimer(this, m_DeleteTime - DateTime.Now);
				m_DeleteTimer.Start();
			}

			if ( version >= 1 )
			{
				MLQuest quest = MLQuestSystem.ReadQuestRef( reader );

				if ( MLQuestSystem.Enabled && quest != null && !StaticMLQuester )
					m_MLQuest = quest;
			}
		}

		public override bool CanBeRenamedBy(Mobile from)
		{
			return (from.AccessLevel >= AccessLevel.GameMaster);
		}

		public override void AddCustomContextEntries(Mobile from, List<ContextMenuEntry> list)
		{
			if ( from.Alive )
			{
				Mobile escorter = GetEscorter();

				if ( !MLQuestSystem.Enabled && GetDestination() != null )
				{
					if ( escorter == null || escorter == from )
						list.Add( new AskDestinationEntry( this, from ) );

					if ( escorter == null )
						list.Add( new AcceptEscortEntry( this, from ) );
				}

				if ( escorter == from )
					list.Add( new AbandonEscortEntry( this, from ) );
			}

			base.AddCustomContextEntries( from, list );
		}

		public virtual string[] GetPossibleDestinations()
		{
			if (!Core.ML)
				return m_TownNames;
			else
				return m_MLTownNames;
		}

		public virtual string PickRandomDestination()
		{
			if (Map.Malas.Regions.Count == 0 || Map == null || Map == Map.Internal || Location == Point3D.Zero)
				return null; // Not yet fully initialized

			string[] possible = GetPossibleDestinations();
			string picked = null;

			while (picked == null)
			{
				picked = possible[Utility.Random(possible.Length)];
				EDI test = EDI.Find(picked);

				if (test != null && test.Contains(Location))
					picked = null;
			}

			return picked;
		}

		public EDI GetDestination()
		{
			if ( MLQuestSystem.Enabled )
				return null;

			if (m_DestinationString == null && m_DeleteTimer == null)
				m_DestinationString = PickRandomDestination();

			if (m_Destination != null && m_Destination.Name == m_DestinationString)
				return m_Destination;

			if (Map.Malas.Regions.Count > 0)
				return (m_Destination = EDI.Find(m_DestinationString));

			return (m_Destination = null);
		}

		private class DeleteTimer : Timer
		{
			private Mobile m_Mobile;

			public DeleteTimer(Mobile m, TimeSpan delay)
				: base(delay)
			{
				m_Mobile = m;

				Priority = TimerPriority.OneSecond;
			}

			protected override void OnTick()
			{
				m_Mobile.Delete();
			}
		}
	}

	public class EscortDestinationInfo
	{
		private string m_Name;
		private Region m_Region;
		//private Rectangle2D[] m_Bounds;

		public string Name
		{
			get { return m_Name; }
		}

		public Region Region
		{
			get { return m_Region; }
		}

		/*public Rectangle2D[] Bounds
		{
			get{ return m_Bounds; }
		}*/

		public bool Contains(Point3D p)
		{
			return m_Region.Contains(p);
		}

		public EscortDestinationInfo(string name, Region region)
		{
			m_Name = name;
			m_Region = region;
		}

		private static Hashtable m_Table;

		public static void LoadTable()
		{
			ICollection list = Map.Malas.Regions.Values;

			if (list.Count == 0)
				return;

			m_Table = new Hashtable();

			foreach (Region r in list)
			{
				if (r.Name == null)
					continue;

				if (r is Regions.DungeonRegion || r is Regions.DungeonEntranceRegion || r is Regions.SkaddriaTownShopRegion || r is Regions.HouseResidenceRegion || r is Regions.AreaInterestRegion || r is Regions.LandscapeRegion || r is Regions.ZaythalorNPCRegion || r is Regions.TownRegion)
					m_Table[r.Name] = new EscortDestinationInfo(r.Name, r);
			}
		}

		public static EDI Find(string name)
		{
			if (m_Table == null)
				LoadTable();

			if (name == null || m_Table == null)
				return null;

			return (EscortDestinationInfo)m_Table[name];
		}
	}

	public class AskDestinationEntry : ContextMenuEntry
	{
		private BaseEscortable m_Mobile;
		private Mobile m_From;

		public AskDestinationEntry(BaseEscortable m, Mobile from): base(6100, 3)
		{
			m_Mobile = m;
			m_From = from;
		}

		public override void OnClick()
		{
			m_Mobile.SayDestinationTo(m_From);
		}
	}

	public class AcceptEscortEntry : ContextMenuEntry
	{
		private BaseEscortable m_Mobile;
		private Mobile m_From;

		public AcceptEscortEntry(BaseEscortable m, Mobile from): base(6101, 3)
		{
			m_Mobile = m;
			m_From = from;
		}

		public override void OnClick()
		{
			m_Mobile.AcceptEscorter(m_From);
		}
	}

	public class AbandonEscortEntry : ContextMenuEntry
	{
		private BaseEscortable m_Mobile;
		private Mobile m_From;

		public AbandonEscortEntry(BaseEscortable m, Mobile from): base(6102, 3)
		{
			m_Mobile = m;
			m_From = from;
		}

		public override void OnClick()
		{
			m_Mobile.Delete(); // OSI just seems to delete instantly
		}
	}
}