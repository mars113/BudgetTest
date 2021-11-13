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

            if (AreDifferentMonths(start, end))
                return QueryMultiMonths(start, end);

            return QuerySingleMonth(start, end);
        }

        private bool AreDifferentMonths(DateTime start, DateTime end) {
            return start.Year != end.Year || start.Month != end.Month;
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

                while (AreDifferentMonths(pointer, end)) {
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

        private DateTime GetLastDate(DateTime start) {
            return new DateTime(
                start.Year,
                start.Month,
                DateTime.DaysInMonth(start.Year, start.Month)
            );
        }


        private decimal QuerySingleMonth(DateTime sourceStart, DateTime sourceEnd) {
            var start = new DateTime(sourceStart.Year, sourceStart.Month, sourceStart.Day);
            var end   = new DateTime(sourceEnd.Year,   sourceEnd.Month,   sourceEnd.Day);

            var days      = (end - start).Days + 1;
            var totalDays = (decimal)DateTime.DaysInMonth(start.Year, start.Month);

            var yearMonth = start.ToString("yyyyMM");
            var amount    = repo.ReturnAll().FirstOrDefault(b => b.YearMonth == yearMonth)?.Amount ?? 0;

            return amount * days / totalDays;
        }

    }
}