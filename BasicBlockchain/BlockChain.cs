using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BasicBlockchain
{
    public class BlockChain<T> where T : new()
    {
        public int              Difficulty { get; private set; }
        public List<Block<T>>   Chain      { get; private set; }

        public BlockChain( int difficulty )
        {
            // Save the required difficulty
            this.Difficulty = difficulty;

            // Initialize the chain
            this.Chain = new List<Block<T>>();
            this.MineGenesisBlock();
        }

        private void MineGenesisBlock()
        {
            // Add a genesis block to start the chain
            Console.Write( "Mining genesis block..." );

            var genesisBlock = new Block<T>( this.Difficulty, 0, null, new T() );
            this.Chain.Add( genesisBlock );

            Console.WriteLine( $"done ({genesisBlock.Nonce} guesses)." );
        }

        public Block<T> MineBlock( T data )
        {
            var latestBlock = GetLatestBlock();
            var prevHash = latestBlock.Hash;

            Console.Write( $"Mining block {latestBlock.Index + 1}..." );

            var newBlock = new Block<T>( this.Difficulty, latestBlock.Index + 1, prevHash, data );
            this.Chain.Add( newBlock );

            Console.WriteLine( $"done ({newBlock.Nonce} guesses)." );
            return newBlock;
        }

        private Block<T> GetLatestBlock()
        {
            return this.Chain[this.Chain.Count - 1];
        }

        public bool IsValid()
        {
            for( int i = 1; i < this.Chain.Count; i++ )
            {
                var currentBlock = this.Chain[i];
                var previousBlock = this.Chain[i - 1];

                // Is the hash of the current block valid?
                if( currentBlock.Hash != currentBlock.CalculateHash() )
                {
                    return false;
                }

                // Does the previous hash match that of the previous block?
                // If not, the chain has been tampered with
                if( currentBlock.PreviousHash != previousBlock.Hash )
                {
                    return false;
                }
            }
            return true;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject( this, Formatting.Indented );
        }
    }
}
