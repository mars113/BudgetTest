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
            var amount    = repo.ReturnAll().FirstOrDefault(b => b.YearMonth == yearMonth)?.Amount ?? 0m;

            return amount * days / totalDays;
        }

    }
}