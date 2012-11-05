using System;
using System.Collections.Generic;

namespace expensereport
{
	public class ExpenseReport {
		private List<Expense> expenses = new List<Expense>();

		public void PrintReport(ReportPrinter printer) {
			int total = 0;
			int mealExpenses = 0;
			
			printer.Print("Expenses " + GetDate() + "\n");

			foreach (Expense expense in expenses) {
				if (expense.type == Expense.Type.BREAKFAST || expense.type == Expense.Type.DINNER)
					mealExpenses += expense.amount;
				
				String name = "TILT";
				switch (expense.type) {
				case Expense.Type.DINNER: name = "Dinner"; break;
				case Expense.Type.BREAKFAST: name = "Breakfast"; break;
				case Expense.Type.CAR_RENTAL: name = "Car Rental"; break;
				}
				printer.Print(String.Format("{0}\t{1}\t${2:0.00}\n",
				                            (  (expense.type == Expense.Type.DINNER && expense.amount > 5000)
				 || (expense.type == Expense.Type.BREAKFAST && expense.amount > 1000)) ? "X" : " ",
				                            name, expense.amount / 100.0));
				
				total += expense.amount;
			}

			printer.Print(String.Format("\nMeal expenses ${0:0.00}",mealExpenses / 100.0 ));
			printer.Print(String.Format("\nTotal ${0:0.00}", total / 100.0));
		}
		
		public void AddExpense(Expense expense) {
			expenses.Add(expense);
		}
		
		private String GetDate() {
			return "9/12/2002";
		}		
	}
}

