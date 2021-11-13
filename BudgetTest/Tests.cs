using System.Linq;
using NSubstitute;
using NUnit.Framework;

namespace BudgetTest {
    [TestFixture]
    public class Tests {

        private BudgetService budgetService;

        [SetUp]
        public void Setup() {
            var budgets = new[] {
                new Budget("202106", 3000)
            };

            var repo = Substitute.For<IBudgetRepo>();
            repo.ReturnAll().Returns(budgets.ToList());

            budgetService = new BudgetService(repo);
        }


        [Test]
        public void Test1() {
            Assert.True(true);
        }

    }
}