using System;
using System.Linq;

namespace BudgetTest {
    public class BudgetService {

        private readonly IBudgetRepo repo;

        public BudgetService(IBudgetRepo repo) {
            this.repo = repo;
        }

        public decimal Query(DateTime start, DateTime end) {
            if (start > end)
                return 0m;

            if (IsDifferentMonths(start, end))
                return QueryMultiMonths(start, end);

            return QuerySingleMonth(start, end);
        }

        private decimal QueryMultiMonths(DateTime start, DateTime end) {
            return QueryFirstMonth() + QueryMiddleMonths() + QueryLastMonth();

            decimal QueryFirstMonth() {
                var lastDate = GetLastDate(start);

                return QuerySingleMonth(start, lastDate);
            }

            decimal QueryMiddleMonths() {
                var sum = 0m;

                var pointer = new DateTime(start.Year, start.Month, 1).AddMonths(1);

                while (IsDifferentMonths(pointer, end)) {
                    var lastDate = GetLastDate(pointer);
                    sum += QuerySingleMonth(pointer, lastDate);

                    pointer = pointer.AddMonths(1);
                }

                return sum;
            }

            decimal QueryLastMonth() {
                var firstDate = new DateTime(end.Year, end.Month, 1);
                return QuerySingleMonth(firstDate, end);
            }
        }

        private bool IsDifferentMonths(DateTime start, DateTime end) {
            return start.Year != end.Year || start.Month != end.Month;
        }

        private DateTime GetLastDate(DateTime start) {
            return new DateTime(
                start.Year,
                start.Month,
                DateTime.DaysInMonth(start.Year, start.Month)
            );
        }

        private decimal QuerySingleMonth(DateTime start, DateTime end) {
            var days      = (end - start).Days + 1;
            var totalDays = (decimal)DateTime.DaysInMonth(start.Year, start.Month);

            var yearMonth = start.ToString("yyyyMM");
            var amount    = repo.ReturnAll().FirstOrDefault(b => b.YearMonth == yearMonth)?.Amount ?? 0m;

            return amount * days / totalDays;
        }

    }
}