using System;

namespace BudgetTest {
    public class BudgetService {

        private readonly IBudgetRepo repo;

        public BudgetService(IBudgetRepo repo) {
            this.repo = repo;
        }

        public object Query(DateTime start, DateTime end) {
            throw new NotImplementedException();
        }

    }
}