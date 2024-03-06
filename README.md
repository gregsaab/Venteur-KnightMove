First off, thank you for the consideration!

# Venteur-KnightMove

## Description
The solution provides a means of solving for the shortest path for knight movements in the context of a valid chess game.
This codebase contains projects for three different servers - `RequestAPI`, `ResultAPI`, and `ComputeWorker`. Additionally, there is `Common` project which contains types shared betwixt the aforementioned projects and a `Specs` project with a smattering of nUnit tests.

## Usage
Here is a [postman collection](https://api.postman.com/collections/20359850-fb3db60f-773e-40d8-b191-45c1c61c530a?access_key=PMAT-01HR9AQ7T43SZAX3P7R4YRS3GH) to interface with the deployed solution.

Otherwise, the url for the request is 
`https://knight-move-request.azurewebsites.net/KnightMove?source=<source position>&target=<target position>`
and the url for the result is
`https://knight-move-result.azurewebsites.net/KnightMove?operationId=<operation id>`

Also, I added an optional `callback` property to the request, so you can setup a [https://webhook.site/](https://webhook.site/) webhook, then pass that url into the request like:
`https://knight-move-request.azurewebsites.net/KnightMove?source=C1&target=F7&callback=https://webhook.site/034b41c1-b327-48b6-be95-15e0e0ac5afb`
and the `ComputeWorker` will post the result to that webhook.


## High Level Overview
### Architecture 
A web api (`RequestAPI`) takes in `Request` payloads, performs basic validation to verify that the `source` and `target` properties are valid positions within a chess game, then either returns a validation error or enqueues the request onto the `knightmoverequests` queue. A function app (`ComputeWorker`) is in place that is triggered by new items on the `knightmoverequests` queue. This function app perfoms the computation to find the shortest path, then when it does it will insert a new `ResultsData` object into a storage table (nosql) with the `operationId` as the `rowKey`.

Using a queue in between the request api and function app made sense to me as Azure sdk provides an easy way of setting up triggers. To me, that is a simple way to create an asynchronous background worker for this workflow. I added an optional `callback` since that provides a bit better UX than having the user constantly poll the results api -- pushed based rather than poll.

### Algorithm
The logic written to find the shortest path is as follows:
1. Enqueue starting position to a `Queue<Position>`
2. Start looping with condition that queue is not empty
3. Dequeue the queue and store as `currentNode`
4. If the `currentNode`'s position is the end position (target), then return that position
5. Otherwise, enqueue all of the current position's valid moves
6. Loop until we find the target position

## Possible optimizations
The `RequestAPI` could do a query in the storage table for results that have the same start & end positions, then just add another entry into that table with the same results, but just with a new `rowKey`/`operationId`
