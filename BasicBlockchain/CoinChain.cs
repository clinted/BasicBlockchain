using System;
using System.Collections.Generic;

namespace BasicBlockchain
{
    public class CoinChain : BlockChain<TransactionList>
    {
        TransactionList PendingTransactions = new TransactionList();
        private readonly int Reward = 1; // 1 currency

        public CoinChain()
            : base( 2 )
        {
        }

        public void CreateTransaction( Transaction transaction )
        {
            this.PendingTransactions.Add( transaction );
        }

        public void ProcessPendingTransactions( string minerAddress )
        {
            // Add a reward transaction for the miner who is mining this block
            this.CreateTransaction( new Transaction( "Reward", "_System_", minerAddress, Reward ) );

            // Mine a new the block containing the pending transactions
            this.MineBlock( this.PendingTransactions );

            // Clear the transaction list queue for the next block
            this.PendingTransactions = new TransactionList();
        }

        public int GetBalance( string address )
        {
            int balance = 0;

            for( int i = 0; i < Chain.Count; i++ )
            {
                for( int j = 0; j < Chain[i].Data.Count; j++ )
                {
                    var transaction = Chain[i].Data[j];

                    if( transaction.FromAddress == address )
                    {
                        balance -= transaction.Amount;
                    }

                    if( transaction.ToAddress == address )
                    {
                        balance += transaction.Amount;
                    }
                }
            }

            return balance;
        }
    }
}
