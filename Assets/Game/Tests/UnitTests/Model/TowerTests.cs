using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Splatrika.StackClone.Model;
using UnityEngine;
using UnityEngine.TestTools;

namespace Splatrika.StackClone.UnitTests
{
    public class TowerTests
    {
        [Test]
        public void ShouldStartsWithStartBlock()
        {
            var startBlock = new Block(
                new Rect(-1, -1, 3, 3),
                Color.yellow);

            var configuration = new TowerConfiguration(
                startBlock.Rect,
                startBlock.Color,
                0);

            var tower = new Tower(configuration);

            Assert.AreEqual(startBlock, tower.Last);
        }



        [Test]
        public void ShouldCutAddingBlock() 
        {
            var lastRect = new Rect(-1, -1, 2, 2);
            var addingRect = new Rect(-0.5f, -1.5f, 2, 2);
            var lastBlock = new Block(lastRect, Color.red);
            var addingBlock = new Block(addingRect, Color.blue);

            var exceptedLastBlock = lastBlock.Cut(addingBlock.Rect);

            var configuration = new TowerConfiguration(
                lastBlock.Rect,
                lastBlock.Color,
                0);

            var tower = new Tower(configuration);

            Block? added = null;
            tower.BlockAdded += (block, _) => added = block;
            tower.AddBlock(addingBlock, out _, out _);

            Assert.NotNull(added);
            Assert.AreEqual(exceptedLastBlock.Rect, added.Value.Rect);
            Assert.AreEqual(exceptedLastBlock.Rect, tower.Last.Rect);
        }


        [Test]
        public void ShouldFinishWhenAddingBlockIsNotOverlapLastBlock()
        {
            var startRect = new Rect(-2, 0, 0.5f, 0.5f);
            var addingRect = new Rect(2, 0, 0.5f, 0.5f);
            var color = Color.gray;
            var addingBlock = new Block(addingRect, color);

            var configuration = new TowerConfiguration(
                startRect,
                color,
                0);

            var tower = new Tower(configuration);
            var finished = false;
            tower.Finished += () => finished = true;
            tower.AddBlock(addingBlock, out _, out bool wasFinished);

            Assert.True(finished);
            Assert.True(tower.IsFinished);
            Assert.True(wasFinished);
        }


        [Test]
        public void AddingShouldBePerfectWhenDistanceIsPerfect()
        {
            CheckPerfect(
                start: new Rect(-2, 0, 2, 2),
                adding: new Rect(-1.5f, 0, 2, 2),
                color: Color.cyan,
                perfectDistance: 1,
                perfect: true);
        }


        [Test]
        public void AddingShouldNotBePerfectWhenDistanceIsNotPerfect()
        {
            CheckPerfect(
                start: new Rect(-2, 0, 2, 2),
                adding: new Rect(-2.2f, 0, 2, 2),
                color: Color.cyan,
                perfectDistance: 0.05f,
                perfect: false);
        }


        [Test]
        public void ShouldNotAllowAddingWhenTowerIsFinished()
        {
            var tower = GetFinished();
            Assert.Throws<InvalidOperationException>(
                () => tower.AddBlock(new Block(), out _, out _));
        }


        private Tower GetFinished()
        {
            var lastRect = new Rect(-1, -1, 2, 2);
            var addingRect = new Rect(100, 100, 2, 2);

            var configuration = new TowerConfiguration(
                lastRect, Color.cyan, 0);
            var tower = new Tower(configuration);
            tower.AddBlock(new Block(addingRect, Color.red), out _, out _);

            return tower;
        }


        private void CheckPerfect(Rect start, Rect adding, Color color,
            float perfectDistance, bool perfect)
        {
            var startRect = start;
            var addingRect = adding;
            var addingBlock = new Block(addingRect, color);

            var configuration = new TowerConfiguration(
                startRect,
                color,
                perfectDistance);

            var tower = new Tower(configuration);
            bool? isPerfect = null;
            tower.BlockAdded += (_, perfect) => isPerfect = perfect;
            tower.AddBlock(addingBlock, out bool wasPerfect, out _);

            Assert.NotNull(isPerfect);
            Assert.AreEqual(perfect, isPerfect);
            Assert.AreEqual(perfect, wasPerfect);
        }
    }
}
