using System;
using System.Linq;

namespace BudgetTest {
    public class BudgetService {

        private readonly IBudgetRepo repo;

        public BudgetService(IBudgetRepo repo) {
            this.repo = repo;
        }

        public decimal Query(DateTime start, DateTime end) {
            var yearMonth = start.ToString("yyyyMM");
            return repo.ReturnAll().First(b => b.YearMonth == yearMonth).Amount;
        }

    }
}