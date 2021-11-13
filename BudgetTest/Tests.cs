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
                new Budget("202106", 3000),
                new Budget("202107", 310)
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

        [Test]
        public void NoData() {
            var start = new DateTime(2021, 1, 1);
            var end   = new DateTime(2021, 1, 2);
            TestQuery(start, end, 0);
        }

        [Test]
        public void TwoMonths() {
            var start = new DateTime(2021, 6, 30);
            var end   = new DateTime(2021, 7, 1);
            TestQuery(start, end, 110m);
        }


        private void TestQuery(DateTime start, DateTime end, decimal expected) {
            var amount = budgetService.Query(start, end);
            Assert.AreEqual(expected, amount);
        }

    }
}