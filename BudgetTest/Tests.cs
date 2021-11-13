using System;
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
        public void SingleMonth() {
            var start = new DateTime(2021, 6, 1);
            var end   = new DateTime(2021, 6, 30);
            TestQuery(start, end, 3000m);
        }
        
        [Test]
        public void SingleDay() {
            var start = new DateTime(2021, 6, 1);
            var end   = new DateTime(2021, 6, 1);
            TestQuery(start, end, 100m);
        }

        private void TestQuery(DateTime start, DateTime end, decimal expected) {
            var amount = budgetService.Query(start, end);
            Assert.AreEqual(expected, amount);
        }

    }
}