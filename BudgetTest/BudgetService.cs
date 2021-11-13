using System;
using System.Linq;

namespace BudgetTest {
    public class BudgetService {

        private readonly IBudgetRepo repo;

        public BudgetService(IBudgetRepo repo) {
            this.repo = repo;
        }

        public decimal Query(DateTime start, DateTime end) {
            var days      = (end - start).Days + 1;
            var totalDays = (decimal)DateTime.DaysInMonth(start.Year, start.Month);

            var yearMonth = start.ToString("yyyyMM");
            var amount    = repo.ReturnAll().First(b => b.YearMonth == yearMonth).Amount;

            return amount * days / totalDays;
        }

    }
}