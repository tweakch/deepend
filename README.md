# deepend

A very basic keyword correlator.

## Settings

```json
{
  "RootDirectory": "C:\\SOME\\PATH\\TO\\A\\DIRECTORY",
  "FilesToInclude": "*.md",
  "OutputFilePath": "analysis.txt",
  "FileAnalysis": {
    "IncludePattern": "[a-zA-Z]+",
    "IgnoreCase": true,
    "IgnoreSpecialCharacters": true,
    "WordsToIgnore": "ignore.txt",
    "SequencesToInclude": "",
    "Lemmas": {
      "ambassador": [ "ambassadors" ],
      "have": [ "has", "had", "having" ],
      "be": [ "being", "am", "are", "been", "were" ],
      "pool": [ "pools" ],
      "privilege": [ "privileged" ],
      "aim": [ "aims" ]
    }
  }
}
```

## Output

```txt
C:\dev\cardano-foundation\docs-cardano-org\components\ouroboros.md (35 words)
Top 10 (count, score)
	ouroboros (1, 2,86%)
		ouroboros network
	network (1, 2,86%)
		ouroboros network component
C:\dev\cardano-foundation\docs-cardano-org\explore-cardano\addresses.md (709 words)
Top 10 (count, score)
	address (43, 6,06%)
		address
		base address
		pointer address
		enterprise address
		account address
		new address shelley
		bootstrap address
		script address
		pointer address carry
		address consist
		utxo address
		data address contains
		base address directly
		pointer address indirectly
		pointer address before
		enterprise address carry
		enterprise address exchanges
		reward address
		address reward
		address furthermore
		address contributes
		reward address does
	stake (30, 4,23%)
		carry stake right
		stake key
		stake
		stake right
		stake pool
		active stake key
		no stake right
		stake protocol
		exercising stake right
		public stake key
		registered stake key
		furthermore stake associated
		stake object
C:\dev\cardano-foundation\docs-cardano-org\explore-cardano\cardano-fee-structure.md (411 words)
Top 10 (count, score)
	transaction (13, 3,16%)
		cardano transaction fee
		transaction fee
		transaction
		transaction tx
		transaction size
		transaction volume
		transaction cost
		small transaction
	fee (10, 2,43%)
		transaction fee structure
		transaction fee system
		handles fee
		fee
		no fee
		cardano fee structure
		minimal fee
		payable fee regardless
C:\dev\cardano-foundation\docs-cardano-org\explore-cardano\cardano-monetary-policy.md (861 words)
Top 10 (count, score)
	cardano (13, 1,51%)
		cardano monetary
		cardano aim
		influence cardano evolution
		transform cardano
		drive cardano development
		development cardano wants
		cardano incentive
		cardano blockchain
		cardano development
		develop cardano activities
		prevent cardano reserve
	reward (13, 1,51%)
		reward
		offer reward
		through reward
		stake reward
		epoch reward
		reward taken
		initial reward over
		available reward
		instead reward change
		higher reward
		high reward
		unclaimed reward automatically
```
