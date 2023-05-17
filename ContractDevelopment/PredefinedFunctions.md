# Predefined Functions and Methods

This section will be replaced by automatically generated technical docs in future.

For now this serves as quick checklist of what functions and properties are available
on the supported types.

### Ulong
- Pow
- BigPow
- Sqrt
- BitLen
- ToTealBytes

### BigInteger

- BitLen

### Byte[]

- Length
- IsFixedSize
- IsReadOnly
- IsSynchronized
- LongLength
- MaxLength
- Rank
- Concat
- Part
- Replace
- Init
- ToString
- ToTealUlong
- BitLen
- GetBit

### Integers

- Pow
- BigPow
- Sqrt
- BitLen
- ToTealBytes

### String

- ToByteArray

### Operators

Most operators are implemented. >> Right Shift is not implemented on BigInteger types.

### AccountRef

- Address
- Balance
- AssetBalance
- IsAssetFrozen
- OptedIn
- SetAsLocalAccountContext
- Freeze
- UnFreeze
- Clawback

### AssetRef

- Id
- Total
- Decimals
- DefaultFrozen
- UnitName
- Name
- URL
- MetadataHash
- Manager
- Reserve
- Freeze
- Clawback
- Creator
- Transfer
- Update

### SmartContractRef

- ApprovalProgram
- ClearStateProgram
- GlobalNumUint
- GlobalNumByteSlice
- LocalNumUint
- LocalNumByteSlice
- ExtraProgramPages
- Creator
- Address
- Balance

### TransactionRef

- Fee
- FirstValid
- LastValid
- Sender
- TxType
- Lease
- Note
- RekeyTo
- GroupIndex
- TxID
- NumAppArgs
- NumAccounts
- NumAssets
- NumApplications
- GlobalNumUint
- GlobalNumByteSlice
- LocalNumUint
- LocalNumByteSlice
- ApplicationID
- OnCompletion
- ApprovalProgram
- ClearStateProgram
- ExtraProgramPages
- XferAsset
- AssetAmount
- AssetSender
- AssetReceiver
- AssetCloseTo
- FreezeAssetAccount
- FreezeAsset
- FreezeAssetFrozen
- Receiver
- Amount
- CloseRemainderTo
- VotePK
- SelectionPK
- StateProofPK
- VoteFirst
- VoteLast
- VoteKeyDilution
- Nonparticipation
- ConfigAsset
- ConfigAssetTotal
- ConfigAssetDecimals
- ConfigAssetDefaultFrozen
- ConfigAssetUnitName
- ConfigAssetName
- ConfigAssetURL
- ConfigAssetMetadataHash
- ConfigAssetManager
- ConfigAssetReserve
- ConfigAssetFreeze
- ConfigAssetClawback


### SmartContract


- Balance
- MinTransactionFee
- MinBalance
- MaxTransactionLife
- ZeroAddress
- GroupSize
- LogicSigVersion
- Round
- LatestTimeStamp
- CurrentApplicationID
- CreatorAddress
- CurrentApplicationAddress
- GroupId
- OpcodeBudget
- CallerApplicationID
- CallerApplicationAddress
- Ecdsa_pk_decompress_secp256k1(byte[] pk, out byte[] pubkey_X, out byte[] pubkey_Y)
- Ecdsa_pk_recover_secp256k1(byte[] data, ulong recover_id, byte[] signature_R, byte[] signature_S, out byte[] pubkey_X, out byte[] pubkey_Y)
- Ecdsa_verify_secp256k1(byte[] data, byte[] signature_R, byte[] signature_S, byte[] pubkey_X, byte[] pubkey_Y)
- Ed25519verify(byte[] data, byte[] signature, byte[] publicKey)
- InvokeSmartContractMethod()
- InvokeSmartSignatureMethod()
- Keccak256(byte[] toHash)
- Sha256(byte[] toHash)
- Sha512_256(byte[] toHash)
- SwitchLocalAccountContext(int acccountIndex)
- BigIntegerToByteArray(BigInteger bytes)
- UBigIntegerToByteArray(UBigInteger bytes)
- BigIntegerFromByteArray(byte[] bytes)
- UBigIntegerFromByteArray(byte[] bytes)
- LogBytes(byte[] bytes)
- LogUInt(ulong integral)
- LogInt(long integral)
- BigIntegerFromIntegral(long integral)
- UBigIntegerFromIntegral(ulong integral)
- CreateAsset




