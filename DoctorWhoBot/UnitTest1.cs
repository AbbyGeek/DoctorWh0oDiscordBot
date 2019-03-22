using NUnit.Framework;
using DoctorWh0oDiscordBot.Core;
using DoctorWh0oDiscordBot.Core.Commands;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        private DndSpell dndSpell;
      
        [SetUp]
        public void Setup()
        {
            // setting variables that will be true universially
            dndSpell = new DndSpell();
        }

        [Test]
        public void Test1()
        {
            //Arrange
            var message = "darkness";
            //Act
            var SpellName = dndSpell.produceURL(message);
            //Assert
       
        }
        [Test]
        public void TestSpellCreateArray()
        {
            //Arrange
            //Act
            //Assert
        }
    }
}