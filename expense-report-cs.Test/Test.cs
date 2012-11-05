using NUnit.Framework;
using System;

namespace expensereport.Test
{
	[TestFixture()]
	public class Tests {
		private ExpenseReport report = new ExpenseReport();
		private MockReportPrinter printer = new MockReportPrinter();
		
		[SetUp()]
		public void SetUp() {
			report = new ExpenseReport();
			printer = new MockReportPrinter();
		}
		
		[Test()]
		public void PrintEmpty() {

			report.PrintReport(printer);
			
			Assert.AreEqual(
				"Expenses 9/12/2002\n" +
				"\n" +
				"Meal expenses $0.00\n" +
				"Total $0.00",
				printer.GetText());
		}
		
		[Test()]
		public void PrintOneDinner() {
			report.AddExpense(new Expense(Expense.Type.DINNER, 1678));
			report.PrintReport(printer);
			
			Assert.AreEqual(
				"Expenses 9/12/2002\n" +
				" \tDinner\t$16.78\n" +
				"\n" +
				"Meal expenses $16.78\n" +
				"Total $16.78",
				printer.GetText());
		}

		[Test()]
		public void TwoMeals() {
			report.AddExpense(new Expense(Expense.Type.DINNER, 1000));
			report.AddExpense(new Expense(Expense.Type.BREAKFAST, 500));
			report.PrintReport(printer);
			
			Assert.AreEqual(
				"Expenses 9/12/2002\n" +
				" \tDinner\t$10.00\n" +
				" \tBreakfast\t$5.00\n" +
				
				"\n" +
				"Meal expenses $15.00\n" +
				"Total $15.00",
				printer.GetText());
		}
		
		[Test()]
		public void TwoMealsAndCarRental() {
			report.AddExpense(new Expense(Expense.Type.DINNER, 1000));
			report.AddExpense(new Expense(Expense.Type.BREAKFAST, 500));
			report.AddExpense(new Expense(Expense.Type.CAR_RENTAL, 50000));
			report.PrintReport(printer);
			
			Assert.AreEqual(
				"Expenses 9/12/2002\n" +
				" \tDinner\t$10.00\n" +
				" \tBreakfast\t$5.00\n" +
				" \tCar Rental\t$500.00\n" +
				"\n" +
				"Meal expenses $15.00\n" +
				"Total $515.00",
				printer.GetText());
		}
		
		[Test()]
		public void Overages() {
			report.AddExpense(new Expense(Expense.Type.BREAKFAST, 1000));
			report.AddExpense(new Expense(Expense.Type.BREAKFAST, 1001));
			report.AddExpense(new Expense(Expense.Type.DINNER, 5000));
			report.AddExpense(new Expense(Expense.Type.DINNER, 5001));
			report.PrintReport(printer);
			
			Assert.AreEqual(
				"Expenses 9/12/2002\n" +
				" \tBreakfast\t$10.00\n" +
				"X\tBreakfast\t$10.01\n" +
				" \tDinner\t$50.00\n" +
				"X\tDinner\t$50.01\n" +
				"\n" +
				"Meal expenses $120.02\n" +
				"Total $120.02",
				printer.GetText());
		}
	}
}

