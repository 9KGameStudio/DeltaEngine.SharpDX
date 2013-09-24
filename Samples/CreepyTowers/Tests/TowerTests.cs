﻿using System;
using System.Collections.Generic;
using CreepyTowers.Creeps;
using CreepyTowers.Levels;
using CreepyTowers.Towers;
using DeltaEngine.Core;
using DeltaEngine.Datatypes;
using DeltaEngine.Graphics;
using DeltaEngine.Platforms;
using NUnit.Framework;

namespace CreepyTowers.Tests
{
	public class TowerTests : TestWithMocksOrVisually
	{
		[SetUp]
		public void Initialize()
		{
			new Game(Resolve<Window>(), Resolve<Device>());
			grid = Game.CameraAndGrid.Grid;
		}

		private LevelGrid grid;

		[Test]
		public void CreateWaterTower()
		{
			new Tower(Vector3D.Zero, Tower.TowerType.Water, Names.TowerWaterRangedWaterspray);
			//var tower = CreateDefaultTower();
			//Assert.AreEqual(0.6f, tower.Get<TowerProperties>().Range);
			//Assert.AreEqual(Tower.TowerType.Water, tower.Get<TowerProperties>().TowerType);
			//Assert.AreEqual(0.5f, tower.Get<TowerProperties>().AttackFrequency);
			//Assert.AreEqual(20.0f, tower.Get<TowerProperties>().AttackDamage);
			//Assert.AreEqual(Tower.AttackType.DirectShot, tower.Get<TowerProperties>().AttackType);
		}

		private Tower CreateDefaultTower()
		{
			var position = grid.PropertyMatrix[2, 3].MidPoint;
			return new Tower(position, Tower.TowerType.Water, Names.TowerWaterRangedWaterspray);
		}

		[Test]
		public void CreateFireTower()
		{
			//var position = grid.PropertyMatrix[5, 5].MidPoint;
			var tower = new Tower(Vector3D.Zero, Tower.TowerType.Fire, Names.TowerFireCandlehula);
			//Assert.AreEqual(0.6f, tower.Get<TowerProperties>().Range);
			//Assert.AreEqual(Tower.TowerType.Fire, tower.Get<TowerProperties>().TowerType);
			//Assert.AreEqual(0.5f, tower.Get<TowerProperties>().AttackFrequency);
			//Assert.AreEqual(40.0f, tower.Get<TowerProperties>().AttackDamage);
			//Assert.AreEqual(Tower.AttackType.RadiusFull, tower.Get<TowerProperties>().AttackType);
		}

		[Test]
		public void CreateSliceTower()
		{
			new Tower(Vector3D.Zero, Tower.TowerType.Acid, Names.TowerSliceConeKnifeblock);
		}

		[Test]
		public void CreateImapctTower()
		{
			new Tower(Vector3D.Zero, Tower.TowerType.Acid, Names.TowerImpactRangedKnightscales);
		}

		[Test]
		public void CreateAcidTower()
		{
			new Tower(Vector3D.Zero, Tower.TowerType.Acid, Names.TowerAcidConeJanitor);
		}

		[Test]
		public void CreateIceTower()
		{
			new Tower(Vector3D.Zero, Tower.TowerType.Acid, Names.TowerIceConeIcelady);
		}

		//[Test]
		//public void CreateTowerAtClickedPosition()
		//{
		//	var floor = new Plane(Vector3D.UnitY, 0.0f);
		//	new Command(pos =>
		//	{
		//		var ray = camera.ScreenPointToRay(ScreenSpace.Current.ToPixelSpace(pos));
		//		var tower = new Model3D(Names.TowerFireCandlehula);
		//		var position = floor.Intersect(ray);
		//		tower.Position = grid.ComputeGridCoordinates(grid, position);
		//	}).Add(new MouseButtonTrigger(MouseButton.Left, State.Releasing));
		//}

		[Test]
		public void DisposingTowerRemovesTowerEntity()
		{
			var tower = CreateDefaultTower();
			tower.Dispose();
			Assert.IsFalse(tower.IsActive);
		}

		//[Test]
		//public void TowerCannotAttackAgainBeforeTimeHasPassed()
		//{
		//	tower = new Tower(new Vector2D(0.3f, 0.4f), Tower.TowerType.Water);
		//	creep = new Creep(Vector2D.Half, Creep.CreepType.Sand);
		//	int numberOfAttacksRecieved = 0;
		//	creep.GotAttacked += () => { numberOfAttacksRecieved++; };
		//	AdvanceTimeAndUpdateEntities(1 / tower.Get<Tower.Properties>().AttackFrequency);
		//	tower.FireAtCreep(creep);
		//	tower.FireAtCreep(creep);
		//	AdvanceTimeAndUpdateEntities(1 / tower.Get<Tower.Properties>().AttackFrequency);
		//	tower.FireAtCreep(creep);
		//	Assert.AreEqual(2, numberOfAttacksRecieved);
		//}

		//[Test]
		//public void CheckForCreepUnderAttack()
		//{
		//	var tower = CreateDefaultTower();
		//	var creep = CreateDefaultCreep();
		//	var creepHpBeforeHit = creep.Get<CreepProperties>().CurrentHp;
		//	AdvanceTimeAndUpdateEntities(1.5f);
		//	tower.FireAtCreep(creep);
		//	var creepHpAfterHit = creep.Get<CreepProperties>().CurrentHp;
		//	Assert.Less(creepHpAfterHit, creepHpBeforeHit);
		//}

		//private Creep CreateDefaultCreep()
		//{
		//	var position = grid.PropertyMatrix[2, 2].MidPoint;
		//	var creep = new Creep(position, Creep.CreepType.Cloth, Names.CreepCottonMummy);
		//	creep.Add(new MovementInGrid.MovementData
		//	{
		//		Velocity = new Vector3D(0.0f, 0.0f, 0.0f),
		//		StartGridPos = new Tuple<int, int>(4, 0),
		//		Waypoints = new List<Tuple<int, int>> { new Tuple<int, int>(1, 0) }
		//	});
		//	creep.Remove<MovementInGrid>();

		//	return creep;
		//}

		//[Test]
		//public void CheckForCreepDead()
		//{
		//	var tower = CreateDefaultTower();
		//	var creep = CreateDefaultCreep();
		//	creep.Get<CreepProperties>().CurrentHp = 2.0f;
		//	AdvanceTimeAndUpdateEntities(1.5f);
		//	tower.FireAtCreep(creep);
		//	Assert.IsFalse(creep.IsActive);
		//}

		//[Test]
		//public void CheckIfCreepIsHit()
		//{
		//	var manager = new Manager(Resolve<ScreenSpace>());
		//	CreateTower(manager);
		//	CreateCreep(manager);
		//	CreateFireTower(manager);
		//	AddSingleCreepAndTowerToManager(manager);
		//	AdvanceTimeAndUpdateEntities(1);
		//}
	}
}