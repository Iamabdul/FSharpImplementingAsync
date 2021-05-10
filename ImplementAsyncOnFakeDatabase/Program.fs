// Learn more about F# at http://fsharp.org

open System


type User = User of Guid

type Post = {
    Content: string
    Number_of_views: int
}

//this takes a username and returns a new guid, the user guid or none
let user userName : User option = 
    System.Guid.NewGuid()
    |> User
    |> Some

let postsOfUser (user: User option) : Post list = 
    match user with 
    | Some user -> 
        [
            {Number_of_views = 10; Content = "content 1"}
            {Number_of_views = 20; Content = "content 2"}
            {Number_of_views = 30; Content = "content 3"}
            {Number_of_views = 40; Content = "content 4"}
            {Number_of_views = 50; Content = "content 5"}
        ]
    | None -> []

let top3 posts =
    posts
    |> List.sortByDescending(fun post -> post.Number_of_views)
    |> List.take 3

let contents posts =
    posts
    |> List.map(fun post -> post.Content)


let userAsync userName : Async<User option> = 
    async{ 
        return System.Guid.NewGuid()
        |> User
        |> Some
       }

let postsOfUserAsync (user: User option) : Async<Post list> = 
    async { 
        match user with 
            | Some user -> 
            return 
                [
                    {Number_of_views = 10; Content = "content 1"}
                    {Number_of_views = 20; Content = "content 2"}
                    {Number_of_views = 30; Content = "content 3"}
                    {Number_of_views = 40; Content = "content 4"}
                    {Number_of_views = 50; Content = "content 5"}
                ]
            | None -> return []
    }

let postsOfAsyncUserAsync (userAsync: Async<User option>) : Async<Post list> =
    async {
        let! user = userAsync
        return! postsOfUserAsync user
    }


//we ideally would not want this to happen like this, spreading the Async.RunSynchronously
//userAsync "this user 2"
//|> Async.RunSynchronously
//|> postsOfUserAsync
//|> Async.RunSynchronously
//|> top3
//|> contents

userAsync "this user 2"
|> postsOfAsyncUserAsync
|> Async.RunSynchronously
|> top3
|> contents
