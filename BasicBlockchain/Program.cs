using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace BasicBlockchain
{
	class Program
	{
		static void Main( string[] args )
		{
			Integer_Chain_Demo();

			//Transaction_Chain_Demo();

			// Coin_Chain_Demo();

			Console.ReadLine();
		}

		static void Integer_Chain_Demo()
		{
			var stopWatch = new Stopwatch();
			stopWatch.Start();

			var intChain = new BlockChain<int>( difficulty: 4 );
			intChain.MineBlock( 25 );
			intChain.MineBlock( 33 );
			intChain.MineBlock( 22 );

			stopWatch.Stop();
			Console.WriteLine( $"Duration: {stopWatch.Elapsed}" );

			Console.WriteLine( "\n=========================" );
			Console.WriteLine( intChain.ToJson() );

			Console.WriteLine( "\n=========================" );
			Console.WriteLine( $"Chain Validation: {intChain.IsValid()}" );
		}

		static void Transaction_Chain_Demo()
		{
			var stopWatch = new Stopwatch();
			stopWatch.Start();

			var transactionChain = new BlockChain<Transaction>( difficulty: 2 );

			transactionChain.MineBlock( new Transaction( "Deposit", "Cash", "Clint", 100 ) );

			transactionChain.MineBlock( new Transaction( "Transfer", "Clint", "Chris", 10 ) );

			transactionChain.MineBlock( new Transaction( "Withdraw", "Clint", "Cash", 5 ) );

			transactionChain.MineBlock( new Transaction( "Withdraw", "Clint", "Cash", 12 ) );

			transactionChain.MineBlock( new Transaction( "Deposit", "Cash", "Chris", 25 ) );

			stopWatch.Stop();
			Console.WriteLine( $"Duration: {stopWatch.Elapsed}" );

			Console.WriteLine( "\n=========================" );
			Console.WriteLine( transactionChain.ToJson() );

			Console.WriteLine( "\n=========================" );
			Console.WriteLine( $"Chain Validation: {transactionChain.IsValid()}" );
		}

		static void Coin_Chain_Demo()
		{
			var stopWatch = new Stopwatch();
			stopWatch.Start();

			var coinChain = new CoinChain();

			coinChain.CreateTransaction( new Transaction( "Deposit", null, "Henry", 100 ) );
			coinChain.CreateTransaction( new Transaction( "Transfer", "Henry", "Clint", 10 ) );
			coinChain.ProcessPendingTransactions( "Bill" );

			coinChain.CreateTransaction( new Transaction( "Transfer", "Clint", "Henry", 3 ) );
			coinChain.CreateTransaction( new Transaction( "Transfer", "Clint", "Henry", 1 ) );
			coinChain.ProcessPendingTransactions( "Bill" );

			stopWatch.Stop();
			Console.WriteLine( $"Duration: {stopWatch.Elapsed}" );

			Console.WriteLine( "\n=========================" );
			Console.WriteLine( coinChain.ToJson() );

			Console.WriteLine( "\n=========================" );
			Console.WriteLine( $"Chain Validation: {coinChain.IsValid()}" );

			Console.WriteLine( "\n=========================" );
			Console.WriteLine( $"Henry's balance: {coinChain.GetBalance( "Henry" )}" );
			Console.WriteLine( $"Clint's balance: {coinChain.GetBalance( "Clint" )}" );
			Console.WriteLine( $"Bill's  balance: {coinChain.GetBalance( "Bill" )}" );

		}
	}
}
