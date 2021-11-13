namespace BudgetTest {
    public class BudgetService {

        private readonly IBudgetRepo repo;

        public BudgetService(IBudgetRepo repo) {
            this.repo = repo;
        }

    }
}