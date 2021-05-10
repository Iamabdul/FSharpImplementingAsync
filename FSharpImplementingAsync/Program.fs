open System
open System.Threading

//here is a function that takes a unit parameter
//it also contains an async block that returns a string
//hence the output type being a string wrapped in an async
let marco () =
     async {
        printfn "doing something.. "
        Thread.Sleep 3000
        return "marco"
     }

let polo =
    async {
        printfn "Initial call .. "
        let! marco = marco()

        printfn "%s polo!" marco
    }

//Async.RunSynchronously polo //RunSynchronously returns something for us to work with
Async.Start polo //Sart returns a unit which we wont be doing anything with
