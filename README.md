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
