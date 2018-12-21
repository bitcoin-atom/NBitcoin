using NBitcoin.Crypto;
using NBitcoin.DataEncoders;
using NBitcoin.Protocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace NBitcoin.Altcoins
{
	public class BitcoinAtom : NetworkSetBase
	{
		public static BitcoinAtom Instance { get; } = new BitcoinAtom();

		public override string CryptoCode => "BCA";

		private BitcoinAtom()
		{
		}

#pragma warning disable CS0618 // Type or member is obsolete
		public class BitcoinAtomConsensusFactory : ConsensusFactory
		{
			private BitcoinAtomConsensusFactory()
			{
			}
			public static BitcoinAtomConsensusFactory Instance { get; } = new BitcoinAtomConsensusFactory();

			public override BlockHeader CreateBlockHeader()
			{
				return new BitcoinAtomBlockHeader();
			}
			public override Block CreateBlock()
			{
				return new BitcoinAtomBlock(new BitcoinAtomBlockHeader());
			}
		}

		public class BitcoinAtomBlockHeader : BlockHeader
		{
			public override uint256 GetPoWHash()
			{
				var headerBytes = this.ToBytes();
				var h = SCrypt.ComputeDerivedKey(headerBytes, headerBytes, 1024, 1, 1, null, 32);
				return new uint256(h);
			}
		}

		public class BitcoinAtomBlock : Block
		{
			public BitcoinAtomBlock(BitcoinAtomBlockHeader bitcoinAtomBlockHeader) : base(bitcoinAtomBlockHeader)
			{

			}

			public override ConsensusFactory GetConsensusFactory()
			{
				return BitcoinAtomConsensusFactory.Instance;
			}
		}

		protected override NetworkBuilder CreateMainnet()
		{
			var builder = new NetworkBuilder();
			builder.SetConsensus(new Consensus()
			{
				SubsidyHalvingInterval = 210000,
				MajorityEnforceBlockUpgrade = 750,
				MajorityRejectBlockOutdated = 950,
				MajorityWindow = 1000,
				BIP34Hash = new uint256("0x000000000000024b89b42a942fe0d9fea3bb44ab7bd1b19115dd6a759c0808b8"),
				PowLimit = new Target(new uint256("00000000ffffffffffffffffffffffffffffffffffffffffffffffffffffffff")),
				PowTargetTimespan = TimeSpan.FromSeconds(14 * 24 * 60 * 60),
				PowTargetSpacing = TimeSpan.FromSeconds(10 * 60),
				PowNoRetargeting = false,
				PowAllowMinDifficultyBlocks = false,
				RuleChangeActivationThreshold = 1916,
				MinerConfirmationWindow = 2016,
				MinimumChainWork = new uint256("0x000000000000000000000000000000000000000000f7a10d870760a5efb2aef8"),
				ConsensusFactory = BitcoinAtomConsensusFactory.Instance,
				SupportSegwit = true,
				CoinbaseMaturity = 100
			})
			.SetBase58Bytes(Base58Type.PUBKEY_ADDRESS, new byte[] { 23 })
			.SetBase58Bytes(Base58Type.SCRIPT_ADDRESS, new byte[] { 10 })
			.SetBase58Bytes(Base58Type.SECRET_KEY, new byte[] { 128 })
			.SetBase58Bytes(Base58Type.EXT_PUBLIC_KEY, new byte[] { 0x04, 0x88, 0xB2, 0x1E })
			.SetBase58Bytes(Base58Type.EXT_SECRET_KEY, new byte[] { 0x04, 0x88, 0xAD, 0xE4 })
			.SetBech32(Bech32Type.WITNESS_PUBKEY_ADDRESS, Encoders.Bech32("bca"))
			.SetBech32(Bech32Type.WITNESS_SCRIPT_ADDRESS, Encoders.Bech32("bca"))
			.SetPort(7333)
			.SetRPCPort(7332)
			.SetName("bca-main")
			.AddDNSSeeds(new DNSSeedData[]
			{
				new DNSSeedData("bitcoinatom.io", "seed.bitcoinatom.io"),
				new DNSSeedData("bitcoinatom.org", "seed.bitcoinatom.org"),
				new DNSSeedData("bitcoinatom.net", "seed.bitcoinatom.net"),
			})
			.SetMagic(0x4fc11de8)
			.AddSeeds(new NetworkAddress[0])
			.SetGenesis("0100000000000000000000000000000000000000000000000000000000000000000000003ba3edfd7a7b12b27ac72c3e67768f617fc81bc3888a51323a9fb8aa4b1e5e4a29ab5f49ffff001d1dac2b7c0101000000010000000000000000000000000000000000000000000000000000000000000000ffffffff4d04ffff001d0104455468652054696d65732030332f4a616e2f32303039204368616e63656c6c6f72206f6e206272696e6b206f66207365636f6e64206261696c6f757420666f722062616e6b73ffffffff0100f2052a01000000434104678afdb0fe5548271967f1a67130b7105cd6a828e03909a67962e0ea1f61deb649f6bc3f4cef38c4f35504e51ec112de5c384df7ba0b8d578a4c702b6bf11d5fac00000000");

			return builder;
		}

		protected override NetworkBuilder CreateTestnet()
		{
			var builder = new NetworkBuilder();
			builder.SetConsensus(new Consensus()
			{
				SubsidyHalvingInterval = 210000,
				MajorityEnforceBlockUpgrade = 75,
				MajorityRejectBlockOutdated = 51,
				MajorityWindow = 100,
				BIP34Hash = new uint256("0x0000000023b3a96d3484e5abb3755c413e7d41500f8e2a5c3f0dd01299cd8ef8"),
				PowLimit = new Target(new uint256("00000000ffffffffffffffffffffffffffffffffffffffffffffffffffffffff")),
				PowTargetTimespan = TimeSpan.FromSeconds(14 * 24 * 60 * 60),
				PowTargetSpacing = TimeSpan.FromSeconds(10 * 60),
				PowNoRetargeting = false,
				PowAllowMinDifficultyBlocks = true,
				RuleChangeActivationThreshold = 108,
				MinerConfirmationWindow = 144,				
				MinimumChainWork = new uint256("0x00000000000000000000000000000000000000000000003480f4fb0959dfdff3"),
				ConsensusFactory = BitcoinAtomConsensusFactory.Instance,
				SupportSegwit = true,
				CoinbaseMaturity = 100
			})
			.SetBase58Bytes(Base58Type.PUBKEY_ADDRESS, new byte[] { 111 })
			.SetBase58Bytes(Base58Type.SCRIPT_ADDRESS, new byte[] { 196 })
			.SetBase58Bytes(Base58Type.SECRET_KEY, new byte[] { 239 })
			.SetBase58Bytes(Base58Type.EXT_PUBLIC_KEY, new byte[] { 0x04, 0x35, 0x87, 0xCF })
			.SetBase58Bytes(Base58Type.EXT_SECRET_KEY, new byte[] { 0x04, 0x35, 0x83, 0x94 })
			.SetBech32(Bech32Type.WITNESS_PUBKEY_ADDRESS, Encoders.Bech32("tbca"))
			.SetBech32(Bech32Type.WITNESS_SCRIPT_ADDRESS, Encoders.Bech32("tbca"))
			.SetPort(17333)
			.SetRPCPort(17332)
			.SetMagic(0xa68e3fd6)
			.AddDNSSeeds(new DNSSeedData[]
			{
				new DNSSeedData("bitcoinatom.io", "testnet-seed.bitcoinatom.io"),
				new DNSSeedData("bitcoinatom.org", "testnet-seed.bitcoinatom.org"),
				new DNSSeedData("bitcoinatom.net", "testnet-seed.bitcoinatom.net"),
			})
			.SetName("bca-test")
			.AddSeeds(new NetworkAddress[0])
			.SetGenesis("0100000000000000000000000000000000000000000000000000000000000000000000003ba3edfd7a7b12b27ac72c3e67768f617fc81bc3888a51323a9fb8aa4b1e5e4adae5494dffff001d1aa4ae180101000000010000000000000000000000000000000000000000000000000000000000000000ffffffff4d04ffff001d0104455468652054696d65732030332f4a616e2f32303039204368616e63656c6c6f72206f6e206272696e6b206f66207365636f6e64206261696c6f757420666f722062616e6b73ffffffff0100f2052a01000000434104678afdb0fe5548271967f1a67130b7105cd6a828e03909a67962e0ea1f61deb649f6bc3f4cef38c4f35504e51ec112de5c384df7ba0b8d578a4c702b6bf11d5fac00000000");

			return builder;
		}

		protected override NetworkBuilder CreateRegtest()
		{
			var builder = new NetworkBuilder();
			builder.SetConsensus(new Consensus()
			{
				SubsidyHalvingInterval = 150,
				MajorityEnforceBlockUpgrade = 750,
				MajorityRejectBlockOutdated = 950,
				MajorityWindow = 1000,
				BIP34Hash = new uint256(),
				PowLimit = new Target(new uint256("00000000ffffffffffffffffffffffffffffffffffffffffffffffffffffffff")),
				PowTargetTimespan = TimeSpan.FromSeconds(14 * 24 * 60 * 60),
				PowTargetSpacing = TimeSpan.FromSeconds(10 * 60),
				PowNoRetargeting = false,
				PowAllowMinDifficultyBlocks = true,
				RuleChangeActivationThreshold = 108,
				MinerConfirmationWindow = 144,
				MinimumChainWork = new uint256("0000000000000000000000000000000000000000000000000000000000000000"),
				ConsensusFactory = BitcoinAtomConsensusFactory.Instance,
				SupportSegwit = true,
				CoinbaseMaturity = 100
			})
			.SetBase58Bytes(Base58Type.PUBKEY_ADDRESS, new byte[] { 111 })
			.SetBase58Bytes(Base58Type.SCRIPT_ADDRESS, new byte[] { 196 })
			.SetBase58Bytes(Base58Type.SECRET_KEY, new byte[] { 239 })
			.SetBase58Bytes(Base58Type.EXT_PUBLIC_KEY, new byte[] { 0x04, 0x35, 0x87, 0xCF })
			.SetBase58Bytes(Base58Type.EXT_SECRET_KEY, new byte[] { 0x04, 0x35, 0x83, 0x94 })
			.SetBech32(Bech32Type.WITNESS_PUBKEY_ADDRESS, Encoders.Bech32("bcart"))
			.SetBech32(Bech32Type.WITNESS_SCRIPT_ADDRESS, Encoders.Bech32("bcart"))
			.SetPort(18444)
			.SetRPCPort(18443)
	    	.SetMagic(0xcad71f4a)
			.SetName("bca-reg")
			.AddSeeds(new NetworkAddress[0])
			.SetGenesis("0100000000000000000000000000000000000000000000000000000000000000000000003ba3edfd7a7b12b27ac72c3e67768f617fc81bc3888a51323a9fb8aa4b1e5e4adae5494dffff7f20020000000101000000010000000000000000000000000000000000000000000000000000000000000000ffffffff4d04ffff001d0104455468652054696d65732030332f4a616e2f32303039204368616e63656c6c6f72206f6e206272696e6b206f66207365636f6e64206261696c6f757420666f722062616e6b73ffffffff0100f2052a01000000434104678afdb0fe5548271967f1a67130b7105cd6a828e03909a67962e0ea1f61deb649f6bc3f4cef38c4f35504e51ec112de5c384df7ba0b8d578a4c702b6bf11d5fac00000000");

			return builder;
		}
	}
}
