open Angstrom
open Angstrom.Consume
open Core

(**Parser**)
let word = take 4 >>= (function | "Card" -> return () | _ -> fail "Expected 'card'")
let spaces = take_while (function | ' ' -> true | _ -> false)
let digits = take_while (function | '0'..'9' -> true | _ -> false)
let colon = take 1 >>| (function | ":" -> fun () -> return () | _ -> fun () -> fail "Expected ':'")
let pipe = take 1 >>| (function | "|" -> fun () -> return () | _ -> fun () -> fail "Expected '|'")
let number = digits >>= fun k -> (Stdlib.int_of_string_opt k) |> (function | Some i -> return i | None -> fail "Expected number")

let game_line = (word *> spaces *> number <* colon <* spaces) >>= fun num -> many (number <* spaces) <* pipe <* spaces >>= fun card -> many (number <* spaces) >>= fun draw -> return (num, card, draw)

let hits (_, card, draw) = List.count draw ~f:(fun x -> (List.mem card x ~equal:Int.equal))
let score = function
| 0 -> 0
| x -> Int.pow 2 (x - 1)

let main =
    let lines = In_channel.input_lines In_channel.stdin in
    let parsed = List.map lines ~f:(Angstrom.parse_string ~consume:Prefix game_line) in
    let filtered = List.filter_map parsed ~f:Result.ok in
    let hit_list = List.map filtered ~f:hits in
    List.sum (module Int) hit_list ~f:score;;

let () =
    Stdlib.print_int main
