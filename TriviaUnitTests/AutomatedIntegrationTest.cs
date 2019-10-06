using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Trivia.UnitTests
{
    [TestClass]
    public class AutomatedIntegrationTest
    {
        [DataTestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        [DataRow(6)]
        [DataRow(7)]
        [DataRow(8)]
        [DataRow(9)]
        public void TestGameRunsSameAsOriginal_00_09(int seed) => TestGameRunsSameAsOriginal(seed);

        [DataTestMethod]
        [DataRow(10)]
        [DataRow(11)]
        [DataRow(12)]
        [DataRow(13)]
        [DataRow(14)]
        [DataRow(15)]
        [DataRow(16)]
        [DataRow(17)]
        [DataRow(18)]
        [DataRow(19)]
        public void TestGameRunsSameAsOriginal_10_19(int seed) => TestGameRunsSameAsOriginal(seed);

        [DataTestMethod]
        [DataRow(20)]
        [DataRow(21)]
        [DataRow(22)]
        [DataRow(23)]
        [DataRow(24)]
        [DataRow(25)]
        [DataRow(26)]
        [DataRow(27)]
        [DataRow(28)]
        [DataRow(29)]
        public void TestGameRunsSameAsOriginal_20_29(int seed) => TestGameRunsSameAsOriginal(seed);

        [DataTestMethod]
        [DataRow(30)]
        [DataRow(31)]
        [DataRow(32)]
        [DataRow(33)]
        [DataRow(34)]
        [DataRow(35)]
        [DataRow(36)]
        [DataRow(37)]
        [DataRow(38)]
        [DataRow(39)]
        public void TestGameRunsSameAsOriginal_30_39(int seed) => TestGameRunsSameAsOriginal(seed);

        [DataTestMethod]
        [DataRow(40)]
        [DataRow(41)]
        [DataRow(42)]
        [DataRow(43)]
        [DataRow(44)]
        [DataRow(45)]
        [DataRow(46)]
        [DataRow(47)]
        [DataRow(48)]
        [DataRow(49)]
        public void TestGameRunsSameAsOriginal_40_49(int seed) => TestGameRunsSameAsOriginal(seed);

        private void TestGameRunsSameAsOriginal(int seed)
        {
            using var writer = new StringWriter();

            var oldOut = Console.Out;
            Console.SetOut(writer);

            GameRunner.RunGame(new Random(seed));

            writer.Flush();

            var originalLines = GameData.ResourceManager.GetString($"Game{seed:00}").Split("\r\n".ToCharArray());
            var newLines = writer.ToString().Split("\r\n".ToCharArray());

            for (int i = 0; i < originalLines.Length && i < newLines.Length; i++)
            {
                Assert.AreEqual(originalLines[i], newLines[i], true, "Line number " + i + " is different.");
            }

            CollectionAssert.AreEqual(originalLines, newLines);

            Console.SetOut(oldOut);
        }
    }
}
