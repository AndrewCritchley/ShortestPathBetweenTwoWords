# ShortestPathBetweenTwoWords

## Quick Overview

* I first of all turn the dictionary words into a graph. Every single word is bundled with any letters with only a single letter difference (as a recursive structure).
* I then get the graph item for the start word and then start a breadth first search of the graph by building up the path I have taken so far and then I get the child nodes which I haven't yet visited within that path (Please Note: The graph will contain circular references).

## Notes on approach

* My first thought was an A* like approach (which can be seen where I discard any potential paths to search which are of the same length)

## Assumptions

* The spec made it clear that the dictionary will contain four letter words, however the sample file has other lengths. I covered both bases by using the decorator pattern without polluting the code within the dictionary provider(s).
* I assumed the start and end words will always be present in the dictionary.
* You could end up with multiple paths of the same length between the start and end words (depending on the dictionary provided). This should be handled in a consistent way by ordering the dictionary alphabetically.
* The spec specifies that this is a command line interface. I assume this means that there is no requirement to accept command line arguments.

## Implementation Notes

* I tried to keep each class as simple as possible. For example it doesn't matter to the actual distance calculator how the dictionary is provided - it just wants that list of words.
* The ProcessRunner class composes multiple other classes with no logic required. The aim was to keep cyclomatic complexity here, and coupling as low as possible between the key classes within the application.
* I've gotten into the habit of designing my classes in such a way that feature toggles can be implemented within the wireup of the DI container. This is particularly useful when feature toggles are temporary and you want to minimise the points you need to change within the codebase.
* I tried a few ways of managing the graph. I tried eagerly creating the graph between all words but decided to make it lazy instead (using the deferred execution provided by linq2objects). It really depends on the intended usage: If this was something that could tollerate a longer warmup time for a shorter search it would make sense to do it this way.


## Potential Improvements

* An easy way to maximise throughput would be to use Parallel LINQ over each branch in the graph
* The file I/O is synchronous. It could use async/await but async/await has the potential to pollute large parts of the codebase and really it's just not needed unless there's a definite need (such as thread pool contention or having multiple tasks that can run concurrently).
* Logging is always useful. I'd include relevant debug/info/warn/error log messages in a production application.
* Metrics are useful - time taken / dictionary size / etc. These can be useful to see issues which grow over time.
* There could be more tests, however the parts with the largest cyclomatic complexity are covered which is the important thing for now.
* Naming things is hard. That is reflected in the code. These days I prefer to establish a Ubiquitous Language with any stakeholders during the discovery phase of a feature. This makes requirements gathering, development and feedback easier. It also helps the codebase maintain consistent naming within itself, as well as consitency with the actual business domain it's involved in.
* There's a lot of use in integration tests in code like this. The interaction between the components is very important to test. Tests such as ensuring the file can be read from disk, it will create the full path to the output file, etc are all useful for long term code bases.
