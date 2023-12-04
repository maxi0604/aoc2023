open Angstrom
open Core
let word = take 4 >>= (function | "Card" -> return () | _ -> fail "Expected 'card'")
let spaces = take_while (function | ' ' -> true | _ -> false)
let digits = take_while (function | '0'..'9' -> true | _ -> false)
let colon = take 1 >>| (function | ":" -> fun () -> return () | _ -> fun () -> fail "Expected ':'")
let pipe = take 1 >>| (function | "|" -> fun () -> return () | _ -> fun () -> fail "Expected '|'")
let number = digits >>= fun k -> (Stdlib.int_of_string_opt k) |> (function | Some i -> return i | None -> fail "Expected number")

let game_line = (word *> spaces *> number <* colon <* spaces) >>= fun num -> (many (number <* spaces)) <* pipe <* spaces >>= fun card -> many (number <* spaces) >>= fun draw -> return (num, card, draw)

let () =
    Stdlib.print_string "ocaml my caml";;
