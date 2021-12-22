open System
open IsEven

let main =
    task {
        let random = Random()

        let! result = random.Next 100 |> isEvenAsync

        printfn "%s" result.Ad
    }

main.Wait()

// HELP WANTED: Child Care provider. Apply in person, Jack & Kill Childcare, 1905 NW Smith. NO PHONE CALLS