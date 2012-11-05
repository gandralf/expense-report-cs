using System;

namespace expensereport.Test
{
	public class MockReportPrinter: ReportPrinter
	{
		private String printedText = "";
		
		public void Print(String text) {
			printedText += text;
		}
		
		public String GetText() {
			return printedText;
		}
	}
}

