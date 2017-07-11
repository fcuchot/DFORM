using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DForm.Tests
{
    [TestFixture]
    public class FormTests
    {

        [Test]
        public void stupid_test()
        {
            Assert.That(true);
        }
        [Test]
        public void Create_Form_Should_Not_Be_Null()
        {
            Form f = new Form();
            //f.Should().NotBeNull();
            Assert.IsNotNull( f );

            //f.Title.Should().BeNull();
            Assert.IsNull( f.Title );
            f.Title = "testTitle";
            //f.Title.Should().NotBeNull();
            Assert.IsNotNull( f.Title );
            //f.Title.Should().Be( "testTitle" );
            Assert.AreEqual( f.Title, "testTitle" );
        }


        [Test]
        public void Check_if_FormAnswer_Creation_Works()
        {
            Form f = new Form();

            FormAnswer fa = f.FindOrCreateAnswer( "Emilie" );
            //fa.Should().NotBeNull();
            Assert.IsNotNull( fa );

            FormAnswer fa2 = f.FindOrCreateAnswer( "Emilie" );
            //fa2.Should().Be( fa );
            Assert.AreEqual( fa2, fa );

            FormAnswer fa21 = f.FindOrCreateAnswer( "John Doe" );
            //fa21.Should().NotBe( fa2 );
            Assert.AreNotEqual( fa21, fa2 );

            FormAnswer fa3 = f.Create( "Georges" );
           // fa3.Should().NotBeNull();
            Assert.IsNotNull( fa3 );

            
        
                Action a1 = () => { var b = f.Create( "Georges" ); };
                var ex = Assert.Throws<ArgumentException>( () => f.Create( "Georges" ) );
               // Assert.IsTrue( ex.TargetSite.Name.Contains( "ThrowArgumentException" ) );
                
          
            //a1.ShouldThrow<ArgumentException>();
            //Assert.Throws<ArgumentException>
            FormAnswer fa4 = f.Find( "Emilie" );
            //fa4.Should().Be( fa2 );
            Assert.AreEqual( fa4, fa2 );
            FormAnswer fa5 = f.Find( "FloLeBGdu69" );
           // fa5.Should().BeNull();
            Assert.IsNull( fa5 );


        }
    }
}
