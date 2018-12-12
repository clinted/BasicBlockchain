using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;

namespace BasicBlockchain
{
	public class Block<D>
	{
		// Header elements
		public string	Hash		 { get; private set; }
		public uint     Index		 { get; private set; }
		public DateTime CreationDate { get; private set; }
		public string   PreviousHash { get; private set; }
		public uint     Nonce		 { get; private set; }

		// Data element
		public D Data { get; private set; }


		// Constructor
		public Block( int difficulty, uint index, string previousHash, D data )
		{
			// Set basic header & data values
			this.Index = index;
			this.CreationDate = DateTime.Now;
			this.PreviousHash = previousHash;
			this.Data = data;

			// Mine the block's Hash based on difficulty and data members
			this.MineBlock( difficulty );
		}

		private void MineBlock( int difficulty )
		{
			// Keep computing hash until it begins with the specified number of zeros
			var leadingZeros = new string( '0', difficulty );
			while (this.Hash == null || this.Hash.Substring(0, difficulty) != leadingZeros )
			{
				this.Nonce++;
				this.Hash = this.CalculateHash();
			}
		}

		public string CalculateHash()
		{
			using( var sha256 = SHA256.Create() )
			{
				string dataToHash = $"{this.Index}-{this.CreationDate}-{this.PreviousHash ?? ""}-{this.Nonce}-{JsonConvert.SerializeObject(this.Data)}";
				byte[] inputBytes = Encoding.ASCII.GetBytes(dataToHash);
				byte[] outputBytes = sha256.ComputeHash(inputBytes);

				return Convert.ToBase64String(outputBytes);
			}
		}
	}
}
