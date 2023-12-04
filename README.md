# AOC 2023
## The plan
With descending priority:
- Solve the challenges
- Have solutions run in reasonable time
- Use a different language for each day
- If the above fails, use a different presentation for the language from before (e. g. local HTML site vs. console output)
- If applicable, get the code running in an environment similar to a real device/deployment environment (e. g. QEMU, after the code is done in ol' reliable [RARS](https://github.com/TheThirdOne/rars))
- Document the process used to get the code running if it's interesting.
- Come up with nice algorithms
- Come up with optimal (with respect to space/time complexity) algorithms

Ideas for languages, in no particular order:
- [x] C
- [x] Haskell
- [x] OCaml
- [x] Rust
- [x] x86-64 Assembly
- [ ] ARM Assembly
- [ ] Ada
- [ ] C#
- [ ] C++
- [ ] COBOL
- [ ] D
- [ ] Elixir
- [ ] Erlang
- [ ] F#
- [ ] Go
- [ ] Java
- [ ] Julia
- [ ] Kotlin
- [ ] Lua
- [ ] One of the Lisps (Probably Common Lisp)
- [ ] PHP
- [ ] Pascal
- [ ] Python
- [ ] R
- [ ] RISC-V Assembly
- [ ] Scala
- [ ] TypeScript
- [ ] Uiua
- [ ] VHDL
- [ ] Zig
- [ ] Zsh

Note that there are more languages than days here. This is deliberate.

## Resources
- https://wiki.freepascal.org/Basic_Pascal_Tutorial/Contents
- https://theintobooks.wordpress.com/2019/12/28/hello-world-on-risc-v-with-qemu/
- https://uclibc.org/docs/psABI-x86_64.pdf
- http://ocamlverse.net/content/monadic-parsers-angstrom.html
## License
I don't know what you'd be using this for, but if you must: AGPLv3 or later.
## Log
- d1: Finished part 1 with **x86-64 Assembly**, part 2 was becoming too depressing, so I wrote **C** instead. (Wow, wouldn't have expected to ever consider C as an upgrade...)
- d2: Finished part 1 and 2 with **Haskell**. After wasting way too much time on unsuccessfully trying to understand parser combinators, I just wrote a minor abomination using `splitOn`. The actual tasks were quite fun to implement (It's always fun to get to use `transpose`), however.
- d3: Wrote both parts in **Rust**. For part 1, I spent way too much time trying to debug some off-by-one error at the end of a line. Fixed it by just padding each line with dots internally. Part 2 went over easily. Using multiple binaries in the same crate made it really pleasant to split the code for both parts. The fact that rust considers a `String` to be valid UTF-8 could be worked around easily by just storing the input as bytes and converting to `&str` where needed. Hit the rate limit, after which it also doesn't ever tell you whether your attempts are too high or too low again (probably to prevent binary searching for the answer).
- d4: Wrote both parts in **OCaml**. This took longer than it would if I hadn't also stubbornly decided that today is the day on which I finally grok parser combinators (See above). Wrote a parser combinator and fought with a weird "Why is that of that type" bug and finished part one on the first submission. For part 2, I don't really see the functional solution, so I wrote a `for` loop that mutates an `int array`. I still don't know the difference between `in`, `;` and `;;` precisely but it works. Also finished part 2 on first submission. I definitely want to combine more parsers in the future, maybe I'll try `Parsec` again now.
