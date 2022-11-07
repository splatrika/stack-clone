using NUnit.Framework;
using Splatrika.StackClone.Model;
using UnityEngine;

namespace Splatrika.StackClone.UnitTests
{
    public class BlockTests
    {
        [Test]
        public void ShouldCut()
        {
            var valuesSets = new CutTestValues[]
            {
                new CutTestValues
                {
                    Rect1 = new Rect(0, 0, 3, 3),
                    Rect2 = new Rect(0, 1, 3, 3),
                    ExceptedResult = new Rect(0, 1, 3, 2)
                },
                new CutTestValues
                {
                    Rect1 = new Rect(0, 1, 2, 2),
                    Rect2 = new Rect(1, 1, 2, 2),
                    ExceptedResult = new Rect(1, 1, 1, 2)
                }
            };

            foreach (var values in valuesSets)
            {
                var block = new Block(values.Rect1, Color.red);
                var cutted = block.Cut(values.Rect2);

                Assert.AreEqual(values.ExceptedResult, cutted.Rect);
            }
        }


        public struct CutTestValues
        {
            public Rect Rect1;
            public Rect Rect2;
            public Rect ExceptedResult;
        }
    }
}
