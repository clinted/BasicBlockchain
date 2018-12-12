using System;
using System.Collections.Generic;

namespace BasicBlockchain
{
	public class Transaction
	{
		public string Activity { get; set; }
		public string FromAddress { get; set; }
		public string ToAddress { get; set; }
		public int Amount { get; set; }

		public Transaction()
		{

		}
		public Transaction(string activity, string fromAddress, string toAddress, int amount)
		{
			Activity = activity;
			FromAddress = fromAddress;
			ToAddress = toAddress;
			Amount = amount;
		}
	}

	public class TransactionList : List<Transaction>
	{
	}
}
